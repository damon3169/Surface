using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : CellController
{
	public override void EffectPlayerIn()
	{
		submarine.MoveToCell(submarine.previousCell.gameObject);
		//ajouter call screenshake
	}

}
