using UnityEngine;
using System.Collections;
using System;

//class handling the player's inventory

public class Inventory : MonoBehaviour
{
	public GameObject[] slots;
	public bool isOpen;
	public GameObject canvas;
	public static int numSlots = 24;
	private static int maxAmnt = 99;// max amount of item that can be in a single slot
	public Item[] itemSlots = new Item[numSlots];
	public int[] slotAmnts = new int[numSlots];

	// Use this for initialization
	void Start ()
	{
		isOpen = false;
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
			itemSlots [index] = null;
			slotAmnts [index] = 0;
			return false;
		}
		return true;
	}

	public Item ammoSearch(int ammoType)//search inventory for ammo of type ammo type, return it if found, else null
	{
		for(int x = 0;x<numSlots;x++)
		{
			if (itemSlots [x].ammoType == ammoType)
				return itemSlots [x];
		}
		return null;
	}
}

