using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatterieCellController : CellController
{
	public override void EffectPlayerIn()
	{
		submarine.AddBatterie();
		//ajouter call screenshake
	}
}
