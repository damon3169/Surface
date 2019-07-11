using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MapSelection : MonoBehaviour
{
	public List<GameObject> Maps;
	List<GameObject> SelectedMaps;
	int numPlayerSelected = 2;

	public Vector2 StartPos;
	int SwipeID = -1;
	float minMovement = 20.0f;
	bool goingRight;
	float scaleX;
	public float floatHeight;     // Desired floating height.
	public float liftForce;       // Force to apply when lifting the rigidbody.
	public float damping;
	public bool isLerping;
	private Vector3 _startPosition;
	private float timeStartedLerping;
	public float timeTakenDuringLerp = 1f;
	public Vector3 targetPostion;
	private float mapMinPositionX;
	private float mapMaxPositionX;
	public float widthCollider;
	private int mapFocused = 0;
	private Vector3 buttonMapTargetPosition;
	public List<GameObject> mapButtonList;
	private Vector3 buttonMapStartPosition;
	public GameObject containersMapButton;
	public float ratio;
	public GameObject mapContainerPrefab;
	public List<GameObject> mapOriginalSelected;
	public GameObject mapButtonPrefab;
	private float timeMouseDown;
	public bool isPopupOpen = false;
	public Event m_Event;

	void selectMaps()
	{
		widthCollider = mapContainerPrefab.GetComponent<BoxCollider2D>().size.x;
		foreach (GameObject map in Maps)
		{
			if (PlayerPrefs.GetInt("numberPlayer") >= map.transform.GetComponent<MapManager>().minPlayer && PlayerPrefs.GetInt("numberPlayer") <= map.transform.GetComponent<MapManager>().maxPlayer)
			{
				mapOriginalSelected.Add(map);
			}
		}
		int i = 0;
		foreach (GameObject map in mapOriginalSelected)
		{
			GameObject parent = Instantiate(mapContainerPrefab, this.transform);

			parent.transform.position = new Vector3(this.transform.position.x + widthCollider * i, this.transform.position.y, this.transform.position.z);
			Instantiate(map, parent.transform);
			SelectedMaps.Add(parent);
			parent = Instantiate(mapButtonPrefab, containersMapButton.transform);
			parent.transform.position = new Vector3(containersMapButton.transform.position.x + 1.5f * i, containersMapButton.transform.position.y, containersMapButton.transform.position.z);
			parent.GetComponent<MapButtonController>().MapIndex = i;
			mapButtonList.Add(parent);
			i++;
		}
	}

	void createSelectionMapButton()
	{

	}

	private void Start()
	{
		SelectedMaps = new List<GameObject>();
		selectMaps();
		for (int i = 0; i < SelectedMaps.Count; i++)
		{
			scaleX += widthCollider;
		}

		mapMaxPositionX = this.transform.position.x - widthCollider;
		mapMinPositionX = this.transform.position.x;
		SortList();
		ratio = (SelectedMaps[0].transform.position.x - SelectedMaps[1].transform.position.x) / (mapButtonList[0].transform.position.x - mapButtonList[1].transform.position.x);
		ChangeFocusedMap(0);
	}

	public void lerpMap()
	{
		_startPosition = this.transform.position;
		timeStartedLerping = Time.time;
		isLerping = true;
		buttonMapStartPosition = containersMapButton.transform.position;
	}

	public void lerpMapStop()
	{
		isLerping = false;
	}

	void FixedUpdate()
	{
		foreach (var T in Input.touches)
		{
			var P = T.position;
			if (T.phase == TouchPhase.Began && SwipeID == -1)
			{
				SwipeID = T.fingerId;
				StartPos = P;
			}
			/*else if (T.fingerId == SwipeID)
			{*/

			var delta = P - StartPos;
			if (T.phase == TouchPhase.Moved && delta.magnitude > minMovement)
			{
				if (this.transform.position.x > mapMinPositionX && mapFocused > 0)
				{

					ChangeFocusedMap(mapFocused - 1);
				}

				else if (this.transform.position.x < mapMaxPositionX && mapFocused < SelectedMaps.Count - 1)
				{
					ChangeFocusedMap(mapFocused + 1);
				}
				SwipeID = -1;
				if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
				{
					lerpMapStop();
					if (delta.x > 0)
					{

						if (goingRight == false)
						{
							lerpMapStop();
							StartPos = T.position;
							delta = P - StartPos;
							goingRight = true;
						}
						if (this.transform.position.x < 0)
						{
							transform.position += new Vector3(0.7f, 0, 0);
						}

					}
					else
					{
						if (goingRight == true)
						{
							lerpMapStop();
							StartPos = T.position;
							delta = P - StartPos;
							goingRight = false;
						}

						if (this.transform.position.x > -(scaleX / 2 + widthCollider / 2))
						{
							Debug.Log(-(scaleX / 2 + widthCollider / 2));

							transform.position += new Vector3(-0.7f, 0, 0);
						}
					}
				}
			}
			else if (T.phase == TouchPhase.Canceled || T.phase == TouchPhase.Ended)
			{
				SwipeID = -1;
				Debug.Log("passe la");
				lerpMap();
			}

		}



		if (isLerping)
		{

			float timeSinceStarted = Time.time - timeStartedLerping;
			float percentageComplete = timeSinceStarted / timeTakenDuringLerp;

			transform.position = Vector3.Lerp(_startPosition, targetPostion, percentageComplete);
			containersMapButton.transform.position = Vector3.Lerp(buttonMapStartPosition, buttonMapTargetPosition, percentageComplete);
			if (percentageComplete >= 1.0f)
			{
				isLerping = false;
			}

		}
	}

	public void ChangeFocusedMap(int newMapIndex)
	{
		mapFocused = newMapIndex;
		mapMinPositionX = -widthCollider * mapFocused;
		targetPostion = new Vector3(mapMinPositionX, 0, 0);
		mapMaxPositionX = mapMinPositionX - widthCollider;
		buttonMapTargetPosition = new Vector3(-mapButtonList[mapFocused].transform.localPosition.x, containersMapButton.transform.position.y, containersMapButton.transform.position.z);
		Debug.Log(buttonMapTargetPosition);
	}

	void OnGUI()
	{
		if (!isPopupOpen)
		{
			m_Event = Event.current;

			if (m_Event.type == EventType.MouseDown)
			{
				StartPos = m_Event.mousePosition;
				timeMouseDown = Time.time;
			}

			if (m_Event.type == EventType.MouseDrag)
			{
				var delta = m_Event.mousePosition - StartPos;
				if (delta.magnitude > minMovement)
				{
					if (this.transform.position.x > mapMinPositionX && mapFocused > 0)
					{
						ChangeFocusedMap(mapFocused - 1);
					}

					else if (this.transform.position.x < mapMaxPositionX && mapFocused < SelectedMaps.Count - 1)
					{
						ChangeFocusedMap(mapFocused + 1);
					}
					if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
					{
						lerpMapStop();
						if (delta.x > 0)
						{

							if (goingRight == false)
							{
								lerpMapStop();
								StartPos = m_Event.mousePosition;
								delta = m_Event.mousePosition - StartPos;
								goingRight = true;
							}
							if (this.transform.position.x < 0)
							{
								transform.position += new Vector3(0.2f, 0, 0);
								containersMapButton.transform.position += new Vector3(0.2f / ratio, 0, 0);
							}

						}
						else
						{
							if (goingRight == true)
							{
								lerpMapStop();
								StartPos = m_Event.mousePosition;
								delta = m_Event.mousePosition - StartPos;
								goingRight = false;
							}

							if (this.transform.position.x > -(scaleX / 2 + widthCollider / 2))
							{
								transform.position += new Vector3(-0.2f, 0, 0);
								containersMapButton.transform.position += new Vector3(-0.2f / ratio, 0, 0);
							}
						}
					}
				}
			}

			if (m_Event.type == EventType.MouseUp)
			{
				if (Time.time - timeMouseDown < 0.2f && StartPos == m_Event.mousePosition)
				{

				}
				lerpMap();
			}
		}
	}

	public void SortList()
	{
		foreach (GameObject map in SelectedMaps)
		{
			MapManager MapManager = map.transform.GetChild(0).GetComponent<MapManager>();
			List<GameObject> Tiles = MapManager.Tiles;
			Tiles = Tiles.OrderBy(x => x.transform.position.x).ToList();
			Vector3 middle = (Tiles[0].transform.position + Tiles[Tiles.Count - 1].transform.position) / 2;
			Vector3 distanceMiddle = MapManager.transform.position - middle;

			foreach (GameObject tile in Tiles)
			{
				tile.transform.position += distanceMiddle;
			}

			Tiles = Tiles.OrderBy(y => y.transform.position.y).ToList();
			middle = (Tiles[0].transform.position + Tiles[Tiles.Count - 1].transform.position) / 2;
			distanceMiddle = MapManager.transform.position - middle;

			foreach (GameObject tile in Tiles)
			{
				tile.transform.position += distanceMiddle;
			}


			float widthTotalx = (Tiles[0].transform.position.x - Tiles[0].transform.localScale.x) - (Tiles[Tiles.Count - 1].transform.position.x + Tiles[0].transform.localScale.x);
			float widthTotaly = (Tiles[Tiles.Count - 1].transform.position.y + Tiles[0].transform.localScale.y) - (Tiles[0].transform.position.y - Tiles[0].transform.localScale.y);
			float newSize;
			if (widthTotalx > widthTotaly)
			{
				newSize = widthCollider / widthTotalx;
			}
			else
			{
				newSize = widthCollider / widthTotaly;
			}
			MapManager.transform.localScale = MapManager.transform.localScale * Mathf.Abs(newSize);
		}
	}
}



