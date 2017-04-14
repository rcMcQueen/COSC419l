using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour {

	bool beenOpened=false;
	int lootLevel;
	public Item[] loot;
	public int[] amount;
	public GameObject openText;
	public Controller controller;
	public Inventory lootInventory;

	public AudioSource audio;

	// Use this for initialization
	void Start () {
		if (lootInventory == null)
			lootInventory = GameObject.FindGameObjectWithTag ("lootInv").GetComponent<Inventory> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			openText.SetActive (true);
			controller.inLootRange = true;
			for(int x = 0;x<loot.Length;x++)
			{
				lootInventory.itemSlots [x] = loot [x];
				lootInventory.slotAmnts [x] = amount [x];
				loot [x] = null;
				amount [x] = 0;
			}
			controller.lootUpdate ();
		}

	}
	void OnTriggerExit(Collider other)
	{
		if(other.tag == "Player")
		{
			openText.SetActive (false);
			controller.inLootRange = false;
			int y = 0;
			for(int x = 0;x<lootInventory.itemSlots.Length;x++)
			{
				if(lootInventory.itemSlots[x] != null)
				{
					loot [y] = lootInventory.itemSlots [x];
					amount [y] = lootInventory.slotAmnts [x];
					y++;
				}
				lootInventory.itemSlots [x] = null;
				lootInventory.slotAmnts [x] = 0;
			}
			controller.lootUpdate ();
		}

	}

}
