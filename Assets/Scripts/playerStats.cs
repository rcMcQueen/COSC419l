using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerStats : MonoBehaviour {

	int hp;
	int attack; //based on equipped weapon (other modifiers?), attack range?
	int defense; //based on equipped armor
	int food;
	int water;
	//TODO add other stuff like status effects when relevant

	// Use this for initialization
	void Start () {
		attack = 5;
		hp = 30;
		defense = 2;
		food = 100;
		water = 100;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public int getAttack()
	{
		return attack;
	}
}
