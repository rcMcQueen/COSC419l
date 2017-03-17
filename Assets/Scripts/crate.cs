using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crate : MonoBehaviour {

	bool beenOpened=false;
	int lootLevel;
	Item[] loot;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			//TODO have popup with icon to show player button to press to open
		}
	}
	void OnTriggerExit(Collider other)
	{
		if(other.tag == "Player")
		{
			//TODO remove popup when player leaves
		}
	}

}
