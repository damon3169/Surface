using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CellController : MonoBehaviour
{

	float timerForDoubleClick = 0.0f;
	float delay = 0.3f;
	bool isDoubleClick = false;
	public enum stateCell { reveal, hide};
	stateCell cellState = stateCell.hide;
	public Sprite hideCellSprite;
	public Sprite revealCellSprite;
	public PlayerController submarine;


	private void Start()
	{
		cellState = stateCell.hide;
	}

	void Update()
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


	void OnMouseOver()
	{
		if (Input.GetButtonDown("Fire1") && isDoubleClick == false)
		{
			Debug.Log("Mouse clicked once");
			isDoubleClick = true;
		}
	}

	void OnMouseDown()
	{
		// si player a cote de this

		if (isDoubleClick == true && timerForDoubleClick < delay && submarine.nearCellList.Contains(this) && !submarine.isMovingToCell)
		{
			//Faire se deplacer le player et ouvrir popup
			
			

			submarine.MoveToCell(this.gameObject);
		}

		// si player PAS a cote de this mais deja passer par la
		else if (isDoubleClick == true && timerForDoubleClick < delay && cellState == stateCell.reveal)
		{
			//Faire revenir popup
		}
	}

	public void CellStateChange(stateCell newState)
	{
		SpriteRenderer cellSpriteRenderer =  this.GetComponent<SpriteRenderer>();
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

}
