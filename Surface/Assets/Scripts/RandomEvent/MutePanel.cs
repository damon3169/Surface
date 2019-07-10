using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MutePanel : DrawerPanel
{
	float ValueTimer = 5f;
	public TextMeshProUGUI textTimer;
	private bool timerStart = false;

	protected override void RandomEventEffect()
	{
		timerStart = true;
	}

	protected override void Awake()
	{
		base.Awake();
		if (ValueTimer > 10)
			textTimer.text = ValueTimer.ToString().Substring(0, 2);
		else
			textTimer.text = ValueTimer.ToString().Substring(0, 1);
	}

	protected override void FixedUpdate()
	{
		base.FixedUpdate();
		if (timerStart)
		{
			if (ValueTimer > 0.1f)
			{
				
				ValueTimer -= Time.deltaTime;
				if (ValueTimer> 10)
					textTimer.text = ValueTimer.ToString().Substring(0, 2);
				else
					textTimer.text = ValueTimer.ToString().Substring(0, 1);
			}
			else
			{
				timerStart = false;
				DrawerOut(this.hidingPosition+ new Vector3 (0,-50,0),true);
			}
		}
	}
}
