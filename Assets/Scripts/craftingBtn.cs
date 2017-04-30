using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class craftingBtn : MonoBehaviour {

	public Text lowerText;
	public craftingSlot[] slots;
	public Text[] amountTexts;
	public Item[] ingrediants;
	public int[] itemAmnts;
	public Item resultItem;
	public Inventory playerInv;
	public Inventory storageInv;
	public bool isSubmit = false;
	bool[] foundItem = {false,false,false,false}; 
	int[] itemIndex = {-1,-1,-1,-1};
	bool canCraft;
	int craftItemIndex = -1;
	public craftingBtn submitBtn;

	// Use this for initialization
	void Start () {
		playerInv = GameObject.FindGameObjectWithTag("playerInv").GetComponent<Inventory>();
		storageInv = GameObject.FindGameObjectWithTag("lootInv").GetComponent<Inventory>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setSubmitBtn()//loads submit button with info on currently selected crafting recipe
	{
		submitBtn.ingrediants = ingrediants;
		submitBtn.itemAmnts = itemAmnts;
		submitBtn.resultItem = resultItem;
		submitBtn.canCraft = canCraft;
		submitBtn.itemIndex = itemIndex;
		submitBtn.craftItemIndex = craftItemIndex;
	}

	public void onClick()
	{
		if(isSubmit)
		{
			makeItem ();
			loadCraft ();
		}
		else
		{
			loadCraft ();
			setSubmitBtn ();
		}
	}

	public void loadCraft()
	{
		for(int x = 0;x<amountTexts.Length;x++)
		{
			amountTexts [x].gameObject.SetActive (true);
			slots [x].pic.gameObject.SetActive (true);
			if (ingrediants [x] == null)
			{
				slots [x].emptySlot ();
				continue;
			}
			slots [x].setItem (ingrediants [x], ingrediants [x].icon, ingrediants [x].name, ingrediants [x].desc);
			amountTexts [x].text = "x" + itemAmnts [x];
		}
		checkForItems ();
		lowerText.text = resultItem.name;
	}

	public void makeItem()
	{
		if (!canCraft)
			return;
		
		for(int x = 0;x<ingrediants.Length;x++)//remove consumed items
		{
			if (ingrediants [x] == null)
				break;
			if(itemIndex[x] < storageInv.itemSlots.Length)//index from storage
			{
				storageInv.slotAmnts [itemIndex [x]] -= itemAmnts [x];
				if(storageInv.slotAmnts [itemIndex [x]] <= 0)//if no more of item left, remove it
				{
					storageInv.itemSlots [itemIndex [x]] = null;
					storageInv.slotAmnts [itemIndex [x]] = 0;
					storageInv.slots [itemIndex [x]].GetComponent<Slot> ().pic.gameObject.SetActive (false);
					storageInv.slots[itemIndex [x]].GetComponent<Slot>().amount = 0;
					storageInv.slots [itemIndex [x]].GetComponent<Slot> ().amountText.text = "";
				}
			}

			else
			{
				Debug.Log ("player: "+ (itemIndex [x]-storageInv.itemSlots.Length));
				playerInv.slotAmnts [itemIndex [x]-storageInv.itemSlots.Length] -= itemAmnts [x];
				if(playerInv.slotAmnts [itemIndex [x]-storageInv.itemSlots.Length] <= 0)//if no more of item left, remove it
				{
					playerInv.itemSlots [itemIndex [x]-storageInv.itemSlots.Length] = null;
					playerInv.slotAmnts [itemIndex [x] - storageInv.itemSlots.Length] = 0;
					playerInv.slots [itemIndex [x] - storageInv.itemSlots.Length].GetComponent<Slot> ().pic.gameObject.SetActive (false);
					playerInv.slots[itemIndex [x] - storageInv.itemSlots.Length].GetComponent<Slot>().amount = 0;
					playerInv.slots [itemIndex [x] - storageInv.itemSlots.Length].GetComponent<Slot> ().amountText.text = "";
				}
			}
		}

		if(craftItemIndex >= 0)//found craft item already exists, add 1 to its amount wherever it is
		{
			if(craftItemIndex < storageInv.itemSlots.Length)//previous item found in storage
			{
				storageInv.slotAmnts [craftItemIndex]++;
			}
			else
			{
				playerInv.slotAmnts [craftItemIndex-storageInv.itemSlots.Length]++;
			}
		}
		else//item doesnt exist yet, make it and add to player inventory
		{
			for(int x = 0;x<playerInv.itemSlots.Length;x++)
			{
				if(playerInv.itemSlots[x] == null)
				{
					playerInv.itemSlots [x] = resultItem;
					playerInv.slotAmnts [x] = 1;
					//playerInv.slots [itemIndex [x]].GetComponent<Slot> ().pic.gameObject.SetActive (true);
					playerInv.slots [x].GetComponent<Slot> ().pic.gameObject.SetActive (true);
					playerInv.slots[x].GetComponent<Slot>().pic.sprite = resultItem.icon;
					playerInv.slots[x].GetComponent<Slot>().amount = 1;
					playerInv.slots [x].GetComponent<Slot> ().amountText.text = "1";
					loadCraft ();//update menus
					return;
				}
			}

			for(int x = 0;x<storageInv.itemSlots.Length;x++)
			{
				if(storageInv.itemSlots[x] == null)
				{
					storageInv.itemSlots [x] = resultItem;
					storageInv.slotAmnts [x] = 1;
					storageInv.slots [itemIndex [x]].GetComponent<Slot> ().pic.gameObject.SetActive (true);
					storageInv.slots[itemIndex [x]].GetComponent<Slot>().pic.sprite = resultItem.icon;
					storageInv.slots[itemIndex [x]].GetComponent<Slot>().amount = 1;
					storageInv.slots [itemIndex [x]].GetComponent<Slot> ().amountText.text = "1";
					loadCraft ();
					return;
				}
			}

			//TODO go back and verify there is room to place item beforehand
			Debug.Log("no room to place item!");
		}
	}

	public void checkForItems()
	{
		itemIndex = new int[]{-1,-1,-1,-1};
		craftItemIndex = -1;
		//check storage first
		for(int x = 0;x<storageInv.itemSlots.Length-1;x++)
		{
			for (int y = 0; y < ingrediants.Length; y++) 
			{
				if(ingrediants[y] == null)
				{
					itemIndex [y] = -2;
					continue;
				}
				if (storageInv.itemSlots [x] == ingrediants [y]) {//one of the ingrediants found in storage
					if (storageInv.slotAmnts [x] >= itemAmnts [y]) {//there is enough of the item to consume
						itemIndex [y] = x;
					}
				} else if (storageInv.itemSlots [x] == resultItem)
					craftItemIndex = x;
			}
		}

		//check player inventory
		for(int x = 0;x<playerInv.itemSlots.Length;x++)
		{
			for (int y = 0; y < ingrediants.Length; y++) 
			{
				if(ingrediants[y] == null)
				{
					itemIndex [y] = -2;
					continue;
				}

				if(playerInv.itemSlots[x] == ingrediants[y])//one of the ingrediants found in storage
				{
					if(playerInv.slotAmnts[x] >= itemAmnts[y])//there is enough of the item to consume
					{
						itemIndex [y] = x + storageInv.itemSlots.Length; //add storage length for check later to differentate storage/player indexs
					}
				}
				else if (playerInv.itemSlots [x] == resultItem)
					craftItemIndex = x+storageInv.itemSlots.Length;
			}
		}
		canCraft = true;
		for(int x = 0;x<itemIndex.Length;x++)//if all indexs are zero or greater, we have enough of each ingrediant found to make item 
		{
			if (itemIndex [x] == -1)
			{
				amountTexts [x].color = Color.red;
				canCraft = false;
			}
			else if (itemIndex[x] == -2)//don't need ingrediant
			{
				amountTexts [x].gameObject.SetActive (false);
				slots [x].pic.gameObject.SetActive (false);
				//images [x].gameObject.SetActive (false);
			}
			else
				amountTexts [x].color = Color.green;
		}
	}
}
