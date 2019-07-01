using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CellController : MonoBehaviour
{

	float timerForDoubleClick = 0.0f;
	float delay = 0.3f;
	bool isDoubleClick = false;
	enum stateCell { reveal, hide};
	stateCell cellState;
	public Sprite hideCellSprite;
	public Sprite revealCellSprite;
	public PlayerController submarine;


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
			cellState = stateCell.reveal;
			
			CellStateChange();

			submarine.MoveToCell(this.gameObject);
		}

		// si player PAS a cote de this mais deja passer par la
		else if (isDoubleClick == true && timerForDoubleClick < delay && cellState == stateCell.reveal)
		{
			//Faire revenir popup
		}
	}

	void CellStateChange()
	{
		Sprite cellSprite =  this.GetComponent<SpriteRenderer>().sprite;
		switch (cellState)
		{
			case stateCell.reveal:
				{
					cellSprite = revealCellSprite;
					break;
				}
			case stateCell.hide:
				{
					cellSprite = hideCellSprite;
					break;
				}
			default: break;
		}
	}

}
