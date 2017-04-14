using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class baseController : MonoBehaviour {

	public timeTracker lootTimer;
	int numWorkers = 4;
	public Item[] workerLootTable;
	public Text lowerText;
	Inventory storageInv;
	int numLootDrops;


	// Use this for initialization
	void Start () {
		numLootDrops = 0;
		storageInv = GameObject.FindGameObjectWithTag ("lootInv").GetComponent<Inventory> ();
	}
	
	// Update is called once per frame
	void Update () {
		lowerText.text = getLootTime ();
		workerTimer ();
	}

	public String getLootTime()
	{
		return "Number of worker loot drops: " + numLootDrops + "\nTime till next drop: " + (int)(lootTimer.getTimeDiffrence () % 10) + " mins";
	}

	public void workerTimer()
	{
		if(lootTimer.getTimeDiffrence() > 10)
		{
			numLootDrops = (int)lootTimer.getTimeDiffrence () / 10;
			Debug.Log (lootTimer.getTimeDiffrence ());
			float workerTime = lootTimer.getTimeDiffrence ();
			while (workerTime > 10)
			{
				workerTime -= 10;
				for(int x = 0;x<numWorkers;x++)
				{
					addToStorage ();
				}
			}
			lootTimer.updateTime (workerTime);
		}
	}

	public void addToStorage()
	{
		Item item = workerLootTable [(int)(UnityEngine.Random.Range (0, workerLootTable.Length-1))];

		for(int x = 0;x<storageInv.itemSlots.Length;x++)//check for item first
		{
			if(storageInv.itemSlots[x] == item)
			{
				storageInv.slotAmnts [x]++;
				return;
			}
		}

		for(int x = 0;x<storageInv.itemSlots.Length;x++)//item not found, place it
		{
			if(storageInv.itemSlots[x] == null)
			{
				storageInv.itemSlots [x] = item;
				storageInv.slotAmnts [x] = 1;
				return;
			}
		}
	}
}
