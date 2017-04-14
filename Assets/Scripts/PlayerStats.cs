using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStats : Character {


	int food;
	int water;
	public Text[] canvasText;
	//TODO add other stuff like status effects when relevant
	float waterTimer;
	static float waterTimerLimit = 9;
	float foodTimer;
	static float foodTimerLimit = 12;// lose 1 food every 12 secs, 5 food per min

	public Image hpBar;
	public Image foodBar;
	public Image waterBar;

	public GameObject canvas;
	public Weapon[] weapons = new Weapon[2];
	public Armor[] armor = new Armor[4]; //0 = helmet, 1 = body, 2 = legs, 3 = boots

	public bool paused;

	// Use this for initialization
	void Start () {
		attack = 1;
		hp = 30;
		maxHp = 30;
		defense = 2;
		food = 100;
		water = 100;
		canvasText [0].text = "" + hp;
		canvasText [1].text = "" + attack;
		canvasText [2].text = "" + defense;
		paused = false;

	}
	
	// Update is called once per frame
	void Update () {
		if(hp <= 0)
		{
			SceneManager.LoadScene ("startScreen");
		}

		if (paused)//don't increment water/food timers while paused
			return;
		waterTimer += Time.deltaTime;
		foodTimer += Time.deltaTime;
		if(waterTimer >= waterTimerLimit)
		{
			waterTimer = 0;
			water--;
			waterBar.transform.localScale = new Vector3 (water * 0.01F, 1, 1);
			if(water <= 0)
			{
				water = 0;
				hp--;
			}
		}
		if(foodTimer >= foodTimerLimit)
		{
			foodTimer = 0;
			food--;
			foodBar.transform.localScale = new Vector3 (food * 0.01F, 1, 1);
			if(food <= 0)
			{
				food = 0;
				hp--;
			}
		}
	}

	public void updateText()
	{
		canvasText [0].text = "" + hp;
		canvasText [1].text = "" + attack;
		canvasText [2].text = "" + defense;
	}

	public void takeDmg(int dmg)
	{
		hp = hp - (dmg - defense);
		updateHPBar ();
	}

	public int getFood()
	{
		return food;
	}

	public int getWater()
	{
		return water;
	}

	public void setFood(int amnt)
	{
		food = amnt;
		foodBar.transform.localScale = new Vector3 (food * 0.01F, 1, 1);
	}

	public void setWater(int amnt)
	{
		water = amnt;
		waterBar.transform.localScale = new Vector3 (water * 0.01F, 1, 1);
	}

	public void updateHPBar()
	{
		hpBar.transform.localScale = new Vector3 (((float)hp/(float)maxHp), 1, 1);
	}
}
