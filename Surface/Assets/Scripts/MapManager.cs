using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class MapManager : MonoBehaviour
{
	public bool isEnable = false;

	public GameObject LastSelectedGameObject;

	public List<GameObject> Tiles;

	public bool isEditEnable = true;

	// Update is called once per frame

	void Start()
	{

		/*foreach (GameObject tile in Tiles)
		{
			tile.GetComponent<TileController>().GetHits();
		}*/
		
	}
#if (UNITY_EDITOR)
    public void CreateTile()
	{
		isEditEnable = false;
		GameObject LastTile = Tiles[0];
		foreach (GameObject tile in Tiles)
		{
			if (tile.GetComponent<TileController>().positionInMap.y> LastTile.GetComponent<TileController>().positionInMap.y)
			{
				LastTile = tile;
			}
		}
		Vector3 lastTilePos = LastTile.transform.position;
		GameObject newTile = Instantiate(Resources.Load<GameObject>("Prefabs/Tile"), lastTilePos, Quaternion.identity);
		newTile.transform.parent = this.transform;
		newTile.transform.position = new Vector3(lastTilePos.x + LastTile.transform.localScale.x / 2, lastTilePos.y - LastTile.transform.localScale.y, lastTilePos.z);
		newTile.GetComponent<TileController>().positionInMap = new Vector2(LastTile.transform.GetComponent<TileController>().positionInMap.x + 1, LastTile.transform.GetComponent<TileController>().positionInMap.y + 1);
		Tiles.Add(newTile);
		isEditEnable = true;
	}

	public void inEditor()
	{
		if (Application.isEditor && isEditEnable && !EditorApplication.isPlayingOrWillChangePlaymode)
		{

			if (LastSelectedGameObject && Selection.activeGameObject != LastSelectedGameObject && LastSelectedGameObject.tag == "Tile")
			{
				LastSelectedGameObject.GetComponent<TileController>().GetHits();
			}
			LastSelectedGameObject = Selection.activeGameObject;
		}
	}
#endif

}
