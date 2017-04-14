using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class statBars : MonoBehaviour {

	public int bar; //0 = hp, 1 = food, 2 = water
	public PlayerStats player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void onEnter()
	{
		if(bar == 0)
			this.GetComponent<Text> ().text = "HP " + player.hp + "/" + player.maxHp;
		else if(bar == 1)
			this.GetComponent<Text> ().text = "Food " + player.getFood() + "/100";
		else if(bar == 2)
			this.GetComponent<Text> ().text = "Water " + player.getWater() + "/100";
	}

	public void onExit()
	{
		if (bar == 0)
			this.GetComponent<Text> ().text = "HP";
		else if (bar == 1)
			this.GetComponent<Text> ().text = "Food";
		else if (bar == 2)
			this.GetComponent<Text> ().text = "Water";
	}

}
