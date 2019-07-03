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
	bool isDraweMoving = false;
	public Vector3 hidingPosition;
	private bool isClosed = false;

	public bool _isDraweMoving
	{
		get { return isDraweMoving; }
		set
		{
			if (isDraweMoving != value)
			{
				isDraweMoving = value;
				//drawer sortie
				if (!isClosed && !isDraweMoving)
				{
					RandomEventEffect();
				}
				else if (isClosed && !isDraweMoving)
				{
					GameObject.Destroy(this.gameObject);
				}
			}
		}
	}


	protected virtual void Awake()
	{
		Target = GameObject.FindGameObjectWithTag("targetRandomEvent");
		rt = this.GetComponent<RectTransform>();
		DrawerOut(Target.GetComponent<RectTransform>().localPosition, false);
		hidingPosition = rt.localPosition;
	}

	public void DrawerOut(Vector3 TargetPosition, bool isClosed)
	{
		this.TargetPosition = TargetPosition;
		_isDraweMoving = true;
		timeStartedLerping = Time.time;
		_startPosition = rt.localPosition;
		distanceToMove = Mathf.Abs(Vector3.Distance(_startPosition, TargetPosition));
		this.isClosed = isClosed;
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
			}
		}
	}

	protected virtual void RandomEventEffect()
	{

	}

}
