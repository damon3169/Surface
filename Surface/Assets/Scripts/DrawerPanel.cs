using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerPanel : MonoBehaviour
{
	RectTransform rt;
	float timeStartedLerping;
	public float timeTakenDuringLerp;
	Vector3 _startPosition;
	public GameObject Target;
	private Vector3 TargetPosition;
	float distanceToMove;
	bool isDraweMoving;

	public bool _isDraweMoving
	{
		get { return isDraweMoving; }
		set
		{
			if (_isDraweMoving != value)
			{
				isDraweMoving = value;
				//drawer sortie
				RandomEventEffect();
			}
		}
	}


	private void Awake()
	{
		Target = GameObject.FindGameObjectWithTag("targetRandomEvent");
		rt = this.GetComponent<RectTransform>();
		DrawerOut();
	}

	void DrawerOut()
	{
		TargetPosition = Target.GetComponent<RectTransform>().localPosition;
		_isDraweMoving = true;
		timeStartedLerping = Time.time;
		_startPosition = rt.localPosition;
		distanceToMove = Mathf.Abs(Vector3.Distance(_startPosition, TargetPosition));
		Debug.Log(distanceToMove);
		Debug.Log(TargetPosition);
	}

	protected virtual void FixedUpdate()
	{
		if (_isDraweMoving)
		{

			float timeSinceStarted = Time.time - timeStartedLerping;
			float percentageComplete = timeSinceStarted / timeTakenDuringLerp;

			rt.localPosition = Vector3.Lerp(_startPosition, TargetPosition, percentageComplete);

			if (percentageComplete >= 1.0f)
			{
				_isDraweMoving = false;
				Debug.Log(rt.transform.position);

			}
		}
	}

	protected virtual void RandomEventEffect()
	{

	}
}
