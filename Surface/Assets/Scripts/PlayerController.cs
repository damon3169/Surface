using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

	public List<CellController> nearCellList;
	private int numberBattery = 0;
	public bool isMovingToCell = false;
	private Vector3 cellTargetPosition;
	public float timeTakenDuringLerp = 1f;
	public CellController currentCell;
	public CellController previousCell;

	/// <summary>
	/// How far the object should move when 'space' is pressed
	/// </summary>
	private float distanceToMove = 10;

	//Whether we are currently interpolating or not

	//The start and finish positions for the interpolation
	private Vector3 _startPosition;
	private Vector3 _endPosition;

	//The Time.time value when we started the interpolation
	private float timeStartedLerping;


	public bool _isMovingToCell
	{
		get { return isMovingToCell; }
		set
		{
			if (isMovingToCell != value)
			{
				isMovingToCell = value;
				// Le sous marin arrive sur la cells
				if (!isMovingToCell)
				{
					//si cell est reveler alors montrer popup
					if(currentCell.cellState == CellController.stateCell.hide)
					{
						currentCell.OpenMyPopup();
					}  
					currentCell.CellStateChange(CellController.stateCell.reveal);
					currentCell.EffectPlayerIn();
				}
			}
		}
	}

	public void MoveToCell(GameObject newCell)
	{
		cellTargetPosition = newCell.transform.position;
		_isMovingToCell = true;
		timeStartedLerping = Time.time;
		_startPosition = this.transform.position;
		distanceToMove = Mathf.Abs(Vector3.Distance(_startPosition, cellTargetPosition));
		if (currentCell!= null)
		{
			previousCell = currentCell;
		}
		currentCell = newCell.GetComponent<CellController>();
	}


	public void addBattery()
	{
		numberBattery += 1;
	}

	public int GetNumberBatteries()
	{
		return numberBattery;
	}


	void FixedUpdate()
	{
		if (_isMovingToCell)
		{
			//We want percentage = 0.0 when Time.time = _timeStartedLerping
			//and percentage = 1.0 when Time.time = _timeStartedLerping + timeTakenDuringLerp
			//In other words, we want to know what percentage of "timeTakenDuringLerp" the value
			//"Time.time - _timeStartedLerping" is.
			float timeSinceStarted = Time.time - timeStartedLerping;
			float percentageComplete = timeSinceStarted / timeTakenDuringLerp;

			//Perform the actual lerping.  Notice that the first two parameters will always be the same
			//throughout a single lerp-processs (ie. they won't change until we hit the space-bar again
			//to start another lerp)
			transform.position = Vector3.Lerp(_startPosition, cellTargetPosition, percentageComplete);

			//When we've completed the lerp, we set _isLerping to false
			if (percentageComplete >= 1.0f)
			{
				foundCloseCell();
				_isMovingToCell = false;
			}
		}
	}

	void foundCloseCell()
	{
		//fonction change les cells a proche contenu dans closeCellList
	}
}
