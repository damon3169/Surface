using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpController : MonoBehaviour
{
	private Vector3 _startPosition;
	private Vector3 _endPosition;
	private Vector3 TargetPosition;

	public float timeTakenDuringLerp = 1f;
	private float timeStartedLerping;
	private bool isMoving = false;
	private int counterDrawLine= 0;
	private float distanceToMove;
	public bool _isMoving
	{
		get { return isMoving; }
		set
		{
			if (isMoving != value)
			{
				isMoving = value;
				if (!isMoving)
				{
					
				}
			}
		}
	}

	public void Lerp(Vector3 target)
	{
		TargetPosition = target;
		_isMoving = true;
		timeStartedLerping = Time.time;
		_startPosition = this.transform.position;
		distanceToMove = Mathf.Abs(Vector3.Distance(_startPosition, TargetPosition));
	}

    void FixedUpdate()
    {
		if (_isMoving)
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
			transform.position = Vector3.Lerp(_startPosition, TargetPosition, percentageComplete);

			//When we've completed the lerp, we set _isLerping to false
			if (percentageComplete >= 1.0f)
			{
				LerpFinished();
				_isMoving = false;
			}
			if (this.GetComponent<LineRenderer>()&& counterDrawLine==5)
			{
				GameObject.Find("EventSystem").GetComponent<DonjonGenerator>().redrawLine();
				counterDrawLine = 0;
			}
			else if (this.GetComponent<LineRenderer>())
			{
				counterDrawLine += 1;
			}
		}
	}

	protected virtual void LerpFinished()
	{

	}
}
