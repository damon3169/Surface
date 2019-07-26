using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
	Vector3 startPosition;
	Vector3 targetPosition;
	private bool isMoving;
	float timeStartedLerping;
	float distanceToMove;
	public float timeTakenDuringLerp = 1f;
	public bool _isMoving
	{
		get { return isMoving; }
		set
		{
			if (isMoving != value)
			{
				isMoving = value;
				// Le sous marin arrive sur la cells
				if (!isMoving)
				{

				}
			}
		}
	}
	void FixedUpdate()
	{
		if (_isMoving)
		{

			float timeSinceStarted = Time.time - timeStartedLerping;
			float percentageComplete = timeSinceStarted / timeTakenDuringLerp;

			//Perform the actual lerping.  Notice that the first two parameters will always be the same
			//throughout a single lerp-processs (ie. they won't change until we hit the space-bar again
			//to start another lerp)
			transform.position = Vector3.Lerp(startPosition, targetPosition, percentageComplete);

			//When we've completed the lerp, we set _isLerping to false
			if (percentageComplete >= 1.0f)
			{
				_isMoving = false;
			}
		}
	}

	public void lerpBackground(bool up)
	{
		startPosition = this.transform.position;
		_isMoving = true;
		timeStartedLerping = Time.time;

		if (up)
		{
			targetPosition = this.transform.position + new Vector3(0, -1.5f, 0);
		}
		else
		{
			targetPosition = this.transform.position + new Vector3(0, 1.5f, 0);
		}
		distanceToMove = Mathf.Abs(Vector3.Distance(startPosition, targetPosition));

	}
}
