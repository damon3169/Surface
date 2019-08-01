using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[ExecuteInEditMode]

public class TileController : MonoBehaviour
{
	public Vector2 positionInMap = Vector2.zero;

	public bool isBaseTile = false;

	public enum TileType
	{
		Corridor, Pilot, Teleport, Generator, Oxygen, Robot, Emergency, Spawn
	}

	public TileType myType;

	private TileType oldType;

	private void Start()
	{
		switch (myType)
		{
			case TileType.Corridor:
				{
					this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Rooms/Corridor");
					break;
				}
			case TileType.Pilot:
				{
					this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Rooms/Pilot");
					break;
				}
			case TileType.Teleport:
				{
					this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Rooms/Teleport");
					break;
				}
			case TileType.Generator:
				{
					this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Rooms/Generator");
					break;
				}
			case TileType.Oxygen:
				{
					this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Rooms/Oxygen");
					break;
				}
			case TileType.Emergency:
				{
					this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Rooms/Emergency");
					break;
				}
			case TileType.Robot:
				{
					this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Rooms/Robot");
					break;
				}
			case TileType.Spawn:
				{
					this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Rooms/Spawn");
					break;
				}
		}
	}
	void Update()
	{
		if (oldType != myType)
		{
			switch (myType)
			{
				case TileType.Corridor:
					{
						this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Rooms/Corridor");
						break;
					}
				case TileType.Pilot:
					{
						this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Rooms/Pilot");
						break;
					}
				case TileType.Teleport:
					{
						this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Rooms/Teleport");
						break;
					}
				case TileType.Generator:
					{
						this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Rooms/Generator");
						break;
					}
				case TileType.Oxygen:
					{
						this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Rooms/Oxygen");
						break;
					}
				case TileType.Emergency:
					{
						this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Rooms/Emergency");
						break;
					}
				case TileType.Robot:
					{
						this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Rooms/Robot");
						break;
					}
				case TileType.Spawn:
					{
						this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Rooms/Spawn");
						break;
					}
			}
			oldType = myType;
		}
	}

	public void GetHits()
	{

		//2 down raycast
		Vector3 direction = Vector3.down;
		List<RaycastHit2D> hits = new List<RaycastHit2D>();
		RaycastHit2D hit = Physics2D.Raycast(new Vector3(this.transform.position.x + this.transform.localScale.x / 2.5f, this.transform.position.y - this.transform.localScale.y / 2 - 0.01f, this.transform.position.z), direction);
		if (hit)
		{
			hits.Add(hit);
		}
		RaycastHit2D hitRight = Physics2D.Raycast(new Vector3(this.transform.position.x - this.transform.localScale.x / 2.5f, this.transform.position.y - this.transform.localScale.y / 2 - 0.01f, this.transform.position.z), direction);
		//2 up raycast
		if (hitRight)
		{
			hits.Add(hitRight);
		}
		direction = Vector3.up;
		RaycastHit2D hitupLeft = Physics2D.Raycast(new Vector3(this.transform.position.x + this.transform.localScale.x / 2.5f, this.transform.position.y + this.transform.localScale.y / 2 + 0.01f, this.transform.position.z), direction);
		if (hitupLeft)
		{
			hits.Add(hitupLeft);
		}
		RaycastHit2D hitupRight = Physics2D.Raycast(new Vector3(this.transform.position.x - this.transform.localScale.x / 2.5f, this.transform.position.y + this.transform.localScale.y / 2 + 0.01f, this.transform.position.z), direction);
		if (hitupRight)
		{
			hits.Add(hitupRight);
		}



		if (hits.Count > 0)
		{
			hits = hits.OrderBy(distance => distance.distance).ToList();
			if (hits.Count > 1)
			{
				int limit = hits.Count;
				for (int i = 1; i < limit; i++)
				{
					Debug.Log(i);
					if (hits[i].transform.position.y != hits[0].transform.position.y )
					{
						Debug.Log("test");
						hits.Remove(hits[i]);
						break;
					}
					if(hits[i].transform.position.x != hits[0].transform.position.x)
					{
						hits.Remove(hits[i]);
					}
				}
			}
			if (hits.Count > 1)
			{
				if (hits[0].transform.position.y > this.transform.position.y)
				{
					this.transform.position = new Vector3(hits[0].transform.position.x, hits[0].transform.position.y - hits[0].transform.localScale.y, this.transform.position.z);

				}
				else
				{
					this.transform.position = new Vector3(hits[0].transform.position.x, hits[0].transform.position.y + hits[0].transform.localScale.y, this.transform.position.z);
				}
			}
			else
			{

				//si en dessous
				if (hits[0].transform.position.y > this.transform.position.y)
				{
					//si a droite
					if (hits[0].transform.position.x > this.transform.position.x)
					{
						this.transform.position = new Vector3(hits[0].transform.position.x - hits[0].transform.localScale.x / 2, hits[0].transform.position.y - hits[0].transform.localScale.y, this.transform.position.z);
					}
					//si a gauche
					else
					{
						this.transform.position = new Vector3(hits[0].transform.position.x + hits[0].transform.localScale.x / 2, hits[0].transform.position.y - hits[0].transform.localScale.y, this.transform.position.z);
					}

				}
				//si au dessus
				else
				{
					if (hits[0].transform.position.x > this.transform.position.x)
					{
						this.transform.position = new Vector3(hits[0].transform.position.x - hits[0].transform.localScale.x / 2, hits[0].transform.position.y + hits[0].transform.localScale.y, this.transform.position.z);

					}
					//si a gauche
					else
					{
						this.transform.position = new Vector3(hits[0].transform.position.x + hits[0].transform.localScale.x / 2, hits[0].transform.position.y + hits[0].transform.localScale.y, this.transform.position.z);
					}
				}
			}
			if (!isBaseTile)
				this.positionInMap = new Vector2(hits[0].transform.GetComponent<TileController>().positionInMap.x, hits[0].transform.GetComponent<TileController>().positionInMap.y - 1);
		}
		/*
		if (hit || hitRight)
		{
			if (hit && hitRight && hitRight.transform.gameObject == hit.transform.gameObject)
			{
				if (hit.transform.tag == "Tile")
				{

					this.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y + hit.transform.localScale.y, this.transform.position.z);
					if (!isBaseTile)
						this.positionInMap = new Vector2(hit.transform.GetComponent<TileController>().positionInMap.x, hit.transform.GetComponent<TileController>().positionInMap.y - 1);
				}
			}
			else if (hit.distance <= hitRight.distance)
			{
				if (hit && hit.transform.tag == "Tile")
				{

					if (hit.transform.position.x > this.transform.position.x)
					{
						this.transform.position = new Vector3(hit.transform.position.x - hit.transform.localScale.x / 2, hit.transform.position.y + hit.transform.localScale.y, this.transform.position.z);
						if (!isBaseTile)
							this.positionInMap = new Vector2(hit.transform.GetComponent<TileController>().positionInMap.x, hit.transform.GetComponent<TileController>().positionInMap.y - 1);
					}
					else
					{
						this.transform.position = new Vector3(hit.transform.position.x + hit.transform.localScale.x / 2, hit.transform.position.y + hit.transform.localScale.y, this.transform.position.z);
						if (!isBaseTile)
							this.positionInMap = new Vector2(hit.transform.GetComponent<TileController>().positionInMap.x + 1, hit.transform.GetComponent<TileController>().positionInMap.y - 1);

					}

				}
				else if (hitRight && hitRight.transform.tag == "Tile")
				{
					if (hitRight.transform.position.x > this.transform.position.x)
					{
						this.transform.position = new Vector3(hitRight.transform.position.x - hitRight.transform.localScale.x / 2, hitRight.transform.position.y + hitRight.transform.localScale.y, this.transform.position.z);
						if (!isBaseTile)
							this.positionInMap = new Vector2(hitRight.transform.GetComponent<TileController>().positionInMap.x - 1, hitRight.transform.GetComponent<TileController>().positionInMap.y - 1);

					}
					else
					{
						this.transform.position = new Vector3(hitRight.transform.position.x + hitRight.transform.localScale.x / 2, hitRight.transform.position.y + hitRight.transform.localScale.y, this.transform.position.z);
						if (!isBaseTile)
							this.positionInMap = new Vector2(hitRight.transform.GetComponent<TileController>().positionInMap.x + 1, hitRight.transform.GetComponent<TileController>().positionInMap.y - 1);
					}
				}

			}
			else
			{
				if (hitRight && hitRight.transform.tag == "Tile")
				{
					if (hitRight.transform.position.x > this.transform.position.x)
					{
						this.transform.position = new Vector3(hitRight.transform.position.x - hitRight.transform.localScale.x / 2, hitRight.transform.position.y + hitRight.transform.localScale.y, this.transform.position.z);
						if (!isBaseTile)
							this.positionInMap = new Vector2(hitRight.transform.GetComponent<TileController>().positionInMap.x - 1, hitRight.transform.GetComponent<TileController>().positionInMap.y - 1);

					}
					else
					{
						this.transform.position = new Vector3(hitRight.transform.position.x + hitRight.transform.localScale.x / 2, hitRight.transform.position.y + hitRight.transform.localScale.y, this.transform.position.z);
						if (!isBaseTile)
							this.positionInMap = new Vector2(hitRight.transform.GetComponent<TileController>().positionInMap.x + 1, hitRight.transform.GetComponent<TileController>().positionInMap.y - 1);
					}
				}
				else if (hit && hit.transform.tag == "Tile")
				{
					if (hit.transform.position.x > this.transform.position.x)
					{
						this.transform.position = new Vector3(hit.transform.position.x - hit.transform.localScale.x / 2, hit.transform.position.y + hit.transform.localScale.y, this.transform.position.z);
						if (!isBaseTile)
							this.positionInMap = new Vector2(hit.transform.GetComponent<TileController>().positionInMap.x, hit.transform.GetComponent<TileController>().positionInMap.y - 1);
					}
					else
					{
						this.transform.position = new Vector3(hit.transform.position.x + hit.transform.localScale.x / 2, hit.transform.position.y + hit.transform.localScale.y, this.transform.position.z);
						if (!isBaseTile)
							this.positionInMap = new Vector2(hit.transform.GetComponent<TileController>().positionInMap.x + 1, hit.transform.GetComponent<TileController>().positionInMap.y - 1);

					}
				}

			}


		}
		else
		{

		
			if (hit && hitRight && hitRight.transform.gameObject == hit.transform.gameObject)
			{
				if (hit.transform.tag == "Tile")
				{
					Debug.Log("test");
					this.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y - hit.transform.localScale.y, this.transform.position.z);
					if (!isBaseTile)
						this.positionInMap = new Vector2(hit.transform.GetComponent<TileController>().positionInMap.x, hit.transform.GetComponent<TileController>().positionInMap.y + 1);
				}
			}
			else if (hit.distance <= hitRight.distance)
			{
				if (hit && hit.transform.tag == "Tile")
				{
					if (hit.transform.position.x > this.transform.position.x)
					{
						Debug.Log("test");
						this.transform.position = new Vector3(hit.transform.position.x - hit.transform.localScale.x / 2, hit.transform.position.y - hit.transform.localScale.y, this.transform.position.z);
						if (!isBaseTile)
							this.positionInMap = new Vector2(hit.transform.GetComponent<TileController>().positionInMap.x - 1, hit.transform.GetComponent<TileController>().positionInMap.y + 1);

					}
					else
					{
						this.transform.position = new Vector3(hit.transform.position.x + hit.transform.localScale.x / 2, hit.transform.position.y - hit.transform.localScale.y, this.transform.position.z);
						if (!isBaseTile)
							this.positionInMap = new Vector2(hit.transform.GetComponent<TileController>().positionInMap.x, hit.transform.GetComponent<TileController>().positionInMap.y + 1);
					}
				}
				else if (hitRight && hitRight.transform.tag == "Tile")
				{
					if (hitRight.transform.position.x > this.transform.position.x)
					{
						this.transform.position = new Vector3(hitRight.transform.position.x - hitRight.transform.localScale.x / 2, hitRight.transform.position.y - hitRight.transform.localScale.y, this.transform.position.z);
						if (!isBaseTile)
							this.positionInMap = new Vector2(hitRight.transform.GetComponent<TileController>().positionInMap.x - 1, hitRight.transform.GetComponent<TileController>().positionInMap.y + 1);
					}
					else
					{
						this.transform.position = new Vector3(hitRight.transform.position.x + hitRight.transform.localScale.x / 2, hitRight.transform.position.y - hitRight.transform.localScale.y, this.transform.position.z);
						if (!isBaseTile)
							this.positionInMap = new Vector2(hitRight.transform.GetComponent<TileController>().positionInMap.x, hitRight.transform.GetComponent<TileController>().positionInMap.y + 1);
					}
				}
			}
			else
			{
				if (hitRight && hitRight.transform.tag == "Tile")
				{
					if (hitRight.transform.position.x > this.transform.position.x)
					{
						this.transform.position = new Vector3(hitRight.transform.position.x - hitRight.transform.localScale.x / 2, hitRight.transform.position.y - hitRight.transform.localScale.y, this.transform.position.z);
						if (!isBaseTile)
							this.positionInMap = new Vector2(hitRight.transform.GetComponent<TileController>().positionInMap.x - 1, hitRight.transform.GetComponent<TileController>().positionInMap.y + 1);
					}
					else
					{
						this.transform.position = new Vector3(hitRight.transform.position.x + hitRight.transform.localScale.x / 2, hitRight.transform.position.y - hitRight.transform.localScale.y, this.transform.position.z);
						if (!isBaseTile)
							this.positionInMap = new Vector2(hitRight.transform.GetComponent<TileController>().positionInMap.x, hitRight.transform.GetComponent<TileController>().positionInMap.y + 1);
					}
				}
				else if (hit && hit.transform.tag == "Tile")
				{
					if (hit.transform.position.x > this.transform.position.x)
					{
						this.transform.position = new Vector3(hit.transform.position.x - hit.transform.localScale.x / 2, hit.transform.position.y - hit.transform.localScale.y, this.transform.position.z);
						if (!isBaseTile)
							this.positionInMap = new Vector2(hit.transform.GetComponent<TileController>().positionInMap.x - 1, hit.transform.GetComponent<TileController>().positionInMap.y + 1);

					}
					else
					{
						this.transform.position = new Vector3(hit.transform.position.x + hit.transform.localScale.x / 2, hit.transform.position.y - hit.transform.localScale.y, this.transform.position.z);
						if (!isBaseTile)
							this.positionInMap = new Vector2(hit.transform.GetComponent<TileController>().positionInMap.x, hit.transform.GetComponent<TileController>().positionInMap.y + 1);
					}
				}

			}
		}*/
	}
}
