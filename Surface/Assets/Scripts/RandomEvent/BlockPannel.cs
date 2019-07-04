using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BlockPannel : MutePanel
{

	public TextMeshProUGUI textDescription;
	protected override void Awake()
	{
		base.Awake();
		System.Random rand = new System.Random();
		int randomNum = rand.Next(1, PlayerPrefs.GetInt("numberCase") + 1);
		textDescription.text = "La salle " + randomNum + " est bloquer." ;
	}
}
