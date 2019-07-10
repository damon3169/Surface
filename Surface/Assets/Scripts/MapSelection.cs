using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MapSelection : MonoBehaviour
{
	public List<GameObject> Maps;
	List<GameObject> SelectedMaps;
	int numPlayerSelected = 2;

	Vector2 StartPos;
	int SwipeID = -1;
	float minMovement = 20.0f;
	Rigidbody2D rigidbody2d;
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

	void selectMaps()
	{
		foreach (GameObject map in Maps)
		{
			if (numPlayerSelected >= map.transform.GetChild(0).GetComponent<MapManager>().minPlayer && numPlayerSelected <= map.transform.GetChild(0).GetComponent<MapManager>().maxPlayer)
			{
				SelectedMaps.Add(map);
			}
		}
	}
	private void Awake()
	{
		widthCollider = this.transform.GetChild(0).GetComponent<BoxCollider2D>().size.x;

	}

	private void Start()
	{
		SelectedMaps = new List<GameObject>();
		rigidbody2d = this.GetComponent<Rigidbody2D>();
		for (int i = 0; i < transform.childCount; i++)
		{
			scaleX += widthCollider;
		}
		selectMaps();
		mapMaxPositionX = this.transform.position.x - widthCollider;
		mapMinPositionX = this.transform.position.x;
		SortList();

	}

	public void lerpMap()
	{
		_startPosition = this.transform.position;
		timeStartedLerping = Time.time;
		isLerping = true;

	}

	public void lerpMapStop()
	{
		isLerping = false;
	}

	void FixedUpdate()
	{

		if (this.transform.position.x > mapMinPositionX && mapFocused > 0)
		{

			ChangeFocusedMap(mapFocused - 1);

		}

		else if (this.transform.position.x < mapMaxPositionX && mapFocused < SelectedMaps.Count - 1)
		{
			ChangeFocusedMap(mapFocused + 1);
		}

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
				SwipeID = -1;
				if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
				{
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
			if (percentageComplete >= 1.0f)
			{
				isLerping = false;
			}
		}
	}

	void ChangeFocusedMap(int newMapIndex)
	{
		mapFocused = newMapIndex;
		mapMinPositionX = -widthCollider * mapFocused;

		targetPostion = new Vector3(mapMinPositionX, 0, 0);
		mapMaxPositionX = mapMinPositionX - widthCollider;
	}

	void OnGUI()
	{
		Event m_Event = Event.current;

		if (m_Event.type == EventType.MouseDown)
		{
			StartPos = m_Event.mousePosition;
		}

		if (m_Event.type == EventType.MouseDrag)
		{
			var delta = m_Event.mousePosition - StartPos;
			if (delta.magnitude > minMovement)
			{
				if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
				{
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
						}
					}
				}
			}
		}

		if (m_Event.type == EventType.MouseUp)
		{
			lerpMap();
		}
	}

	public void SortList()
	{
		foreach (GameObject map in Maps)
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


			float widthTotalx = (Tiles[0].transform.position.x - Tiles[0].transform.localScale.x ) - (Tiles[Tiles.Count - 1].transform.position.x + Tiles[0].transform.localScale.x );
			float widthTotaly = (Tiles[Tiles.Count - 1].transform.position.y + Tiles[0].transform.localScale.y) -(Tiles[0].transform.position.y - Tiles[0].transform.localScale.y ) ;
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



