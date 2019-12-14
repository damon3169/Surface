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
	private PortionDisplay portionDisplay;
	public bool isGoingUp = true;
	public DonjonGenerator donjon;
	public int notMoving = 5;

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
		portionDisplay = GameObject.Find("EventSystem").GetComponent<PortionDisplay>();
		if (this.name == "ChangeLevelDrawer(Clone)")
		{
			Target = GameObject.Find("TargetBackToSelection");
			isGoingUp = false;
			notMoving = 0;
		}
		else
			Target = GameObject.FindGameObjectWithTag("targetRandomEvent");
		rt = this.GetComponent<RectTransform>();
		hidingPosition = rt.localPosition;
		DrawerOut(Target.GetComponent<RectTransform>().localPosition, false, true);
	}

	public void DrawerOut(Vector3 TargetPosition, bool isClosed, bool isMovingDungeon)
	{
		Debug.Log(isGoingUp);
		this.TargetPosition = TargetPosition;
		_isDraweMoving = true;
		timeStartedLerping = Time.time;
		_startPosition = hidingPosition;
		distanceToMove = Mathf.Abs(Vector3.Distance(_startPosition, TargetPosition));
		float nb;
		if (isGoingUp)
		{
			nb = 1;
		}
		else
		{
			nb = -1;
		}
		if (isMovingDungeon)
		{
			if (GameObject.Find("Donjon").transform.GetChild(0).gameObject.activeInHierarchy == false)
			{
				for (int i = 0; i < portionDisplay.portionSelected.transform.childCount; i++)
				{
					if (portionDisplay.portionSelected.transform.GetChild(i).GetComponent<CellController>().cellRow == notMoving)
					{
						portionDisplay.portionSelected.transform.GetChild(i).GetComponent<LerpController>().Lerp(portionDisplay.portionSelected.transform.GetChild(i).position + new Vector3(0, nb, 0));
					}
					else
					{
						portionDisplay.portionSelected.transform.GetChild(i).GetComponent<LerpController>().Lerp
							(portionDisplay.portionSelected.transform.GetChild(i).position + new Vector3
							(0, nb + (nb * (2f * (1 - (0.2f * (5 - portionDisplay.portionSelected.transform.GetChild(i).GetComponent<CellController>().cellRow))))), 0));
					}
				}
			}
			else
			{
				donjon = GameObject.Find("EventSystem").GetComponent<DonjonGenerator>();
				donjon.pStage1.GetComponent<LerpController>().Lerp(donjon.pStage1.transform.position + new Vector3(0, 3 * nb, 0));
				foreach (GameObject pstage3 in donjon.pStage3)
				{
					pstage3.GetComponent<LerpController>().Lerp(pstage3.transform.position + new Vector3(0, 2.7f * nb, 0));
				}
				foreach (GameObject pstage2 in donjon.pStage2)
				{
					pstage2.GetComponent<LerpController>().Lerp(pstage2.transform.position + new Vector3(0, 3 * nb, 0));
				}


			}
		}
		this.isClosed = isClosed;
		isGoingUp = !isGoingUp;
		Debug.Log(isGoingUp);
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
