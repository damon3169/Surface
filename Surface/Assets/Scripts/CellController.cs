using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CellController : MonoBehaviour
{

	float timerForDoubleClick = 0.0f;
	float delay = 0.3f;
	bool isDoubleClick = false;
	public enum stateCell { reveal, hide };
	public stateCell cellState = stateCell.hide;
	public Sprite hideCellSprite;
	public Sprite revealCellSprite;
	public PlayerController submarine;
	public GameObject myPopup;
	public float timeHold;
	public float timeHoldRelease = 0.8f;

	protected virtual void Start()
	{
		cellState = stateCell.hide;
	}

	protected virtual void Update()
	{
		if (isDoubleClick == true)
		{
			timerForDoubleClick += Time.deltaTime;
		}

		if (timerForDoubleClick >= delay)
		{
			timerForDoubleClick = 0.0f;
			isDoubleClick = false;
		}
	}

	protected virtual void OnMouseOver()
	{
		if (Input.GetButtonDown("Fire1") && isDoubleClick == false)
		{
			Debug.Log("Mouse clicked once");
			isDoubleClick = true;
		}
	}

	protected virtual void OnMouseDown()
	{
		// si player a cote de this
		timeHold = Time.time;
		if (isDoubleClick == true && timerForDoubleClick < delay && submarine.nearCellList.Contains(this) && !submarine.isMovingToCell)
		{
			//Faire se deplacer le player si player pas encore sur cette cell
			if (submarine.currentCell != this)
			{
				submarine.MoveToCell(this.gameObject);
			}
		}

	}

	protected virtual void OnMouseUp()
	{

		if (Time.time - timeHold > timeHoldRelease)
		{
			OpenMyPopup();
		}
	}

	public virtual void CellStateChange(stateCell newState)
	{
		SpriteRenderer cellSpriteRenderer = this.GetComponent<SpriteRenderer>();
		cellState = newState;
		Debug.Log(cellState);
		switch (cellState)
		{
			case stateCell.reveal:
				{
					cellSpriteRenderer.sprite = revealCellSprite;
					break;
				}
			case stateCell.hide:
				{
					cellSpriteRenderer.sprite = hideCellSprite;
					break;
				}
			default: break;
		}
	}

	public virtual void OpenMyPopup()
	{
		if (GameObject.FindGameObjectWithTag("popup"))
		{
			Destroy(GameObject.FindGameObjectWithTag("popup"));
		}
		Instantiate(myPopup);
	}

	public virtual void EffectPlayerIn()
	{

	}

}
