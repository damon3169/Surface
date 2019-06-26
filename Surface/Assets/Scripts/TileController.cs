using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]

public class TileController : MonoBehaviour
{
	public Vector2 positionInMap = Vector2.zero;

	public bool isBaseTile = false;

	public enum TileType
	{
		Corridor, Pilot, Teleport, Generator, Oxygen
	}

	public TileType myType;

	private TileType oldType;


	void Update()
	{
		if (oldType != myType)
		{
			switch (myType)
			{
				case TileType.Corridor:
					{
						this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Corridor");
						break;
					}
				case TileType.Pilot:
					{
						this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Pilot");
						break;
					}
				case TileType.Teleport:
					{
						this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Teleport");
						break;
					}
				case TileType.Generator:
					{
						this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Generator");
						break;
					}
				case TileType.Oxygen:
					{
						this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Oxygen");
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
		RaycastHit2D hit = Physics2D.Raycast(new Vector3(this.transform.position.x + this.transform.localScale.x / 2.5f, this.transform.position.y - this.transform.localScale.y / 2 - 0.01f, this.transform.position.z), direction);
		RaycastHit2D hitRight = Physics2D.Raycast(new Vector3(this.transform.position.x - this.transform.localScale.x / 2.5f, this.transform.position.y - this.transform.localScale.y / 2 - 0.01f, this.transform.position.z), direction);

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

			//2 up raycast
			direction = Vector3.up;
			hit = Physics2D.Raycast(new Vector3(this.transform.position.x + this.transform.localScale.x / 2.5f, this.transform.position.y + this.transform.localScale.y / 2 + 0.01f, this.transform.position.z), direction);
			hitRight = Physics2D.Raycast(new Vector3(this.transform.position.x - this.transform.localScale.x / 2.5f, this.transform.position.y + this.transform.localScale.y / 2 + 0.01f, this.transform.position.z), direction);
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
		}
	}
}
