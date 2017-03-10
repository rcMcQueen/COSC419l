using UnityEngine;
using System.Collections;
using System;

//class handling the player's inventory

public class Inventory : MonoBehaviour
{
	Item[] slots;
	int[] slotAmnts;
	private static int numSlots = 30;//TODO temp value
	private static int maxAmnt = 99;// max amount of item that can be in a single slot

	// Use this for initialization
	void Start ()
	{
		slots = new Item[numSlots];
		slotAmnts = new int[numSlots];
		DontDestroyOnLoad (this.gameObject);//stops inventory from being destroyed between scenes
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public bool consumeBullet(Item bullet)//returns false if out of ammo, else true
	{
		int index = Array.FindIndex (slots, x => bullet);//predicate to find index of bullet
		slotAmnts [index]--;
		if(slotAmnts[index] <= 0)//run out of ammo, remove from inventory
		{
			slots [index] = null;
			slotAmnts [index] = 0;
			return false;
		}
		return true;
	}

	public Item ammoSearch(int ammoType)//search inventory for ammo of type ammo type, return it if found, else null
	{
		for(int x = 0;x<numSlots;x++)
		{
			if (slots [x].ammoType == ammoType)
				return slots [x];
		}
		return null;
	}
}

