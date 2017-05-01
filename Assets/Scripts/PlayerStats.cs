using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerStats : MonoBehaviour {

	int hp;
	int attack; //based on equipped weapon (other modifiers?), attack range?
	int defense; //based on equipped armor
	int food;
	int water;
	public Text[] canvasText;
	//TODO add other stuff like status effects when relevant

	public GameObject canvas;
	public Weapon[] weapons = new Weapon[2];
	public Armor[] armor = new Armor[4]; //0 = helmet, 1 = body, 2 = legs, 3 = boots

	// Use this for initialization
	void Start () {
		attack = 1;
		hp = 30;
		defense = 2;
		food = 100;
		water = 100;
		canvasText [0].text = "" + hp;
		canvasText [1].text = "" + attack;
		canvasText [2].text = "" + defense;

	}
	
	// Update is called once per frame
	void Update () {

	}

	public int getAttack()
	{
		return attack;
	}

	public void setAttack(int attack)
	{
		this.attack = attack;
		canvasText [1].text = "" + attack;
	}

	public int getDefense()
	{
		return defense;
	}

	public int getHp()
	{
		return hp;
	}
}
