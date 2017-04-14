using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerClickHandler {
	
	public Image pic;
	public Text amountText;
	Sprite icon = null;
	public int amount=0;
	public int idNum;
	public GameObject MovingItem;
	public Inventory inventory;
	public bool isChar = false;
	public PlayerStats player;
	public Transform[] gearPos;
	public bool isLoot;
	public toolTip tooltip;
	public bool atHomeBase = false;
	public int invType = 0;// 0 = loot, 1 = playerInv, 2 = playerGear

	// Use this for initialization
	void Start () {

		if (invType == 0)
			inventory = GameObject.FindGameObjectWithTag ("lootInv").GetComponent<Inventory>();
		else if (invType == 1)
			inventory = GameObject.FindGameObjectWithTag ("playerInv").GetComponent<Inventory>();
		else if (invType == 2)
			inventory = GameObject.FindGameObjectWithTag ("playerGear").GetComponent<Inventory>();

		if(inventory.slots[idNum] == null)
			inventory.slots [idNum] = this.gameObject;

		if(idNum >= inventory.itemSlots.Length)
		{
			pic.gameObject.SetActive (false);
			return;
		}
		if(inventory.itemSlots[idNum] != null)
		{
			pic.sprite = inventory.itemSlots [idNum].icon;
			amount = inventory.slotAmnts [idNum];
			amountText.text = "" + amount;
		}
		else
		{
			pic.gameObject.SetActive (false);
		}
	}
	
	// Update is called once per frame
	void Update () {   
		
	}

	public void setItem(Item item, Sprite icon, int amount)
	{
		
		pic.sprite = icon;
		this.amount = amount;
		if (amount > 1)
			amountText.text = "" + amount;
		else
			amountText.text = "";
	}

	public void moveItem()
	{
		
		if(inventory.itemSlots[idNum] == null && MovingItem.GetComponent<MovingItem>().item != null)//have item and put it in empty slot
		{
			if(inventory.control != null)
				inventory.control.playClick ();
			Debug.Log ("Trying to place item in empty slot");
			if ((isChar && !(MovingItem.GetComponent<MovingItem> ().item is Weapon || MovingItem.GetComponent<MovingItem> ().item is Armor)))//not trying to equip non equipable item
				return;
			if (isChar && !MovingItem.GetComponent<MovingItem> ().item is Weapon && (idNum == 4 || idNum == 5))//trying to place non weapon in weapon space
				return;
			if (isChar && MovingItem.GetComponent<MovingItem> ().item is Weapon && !(idNum == 4 || idNum == 5))//trying to place non weapon in weapon space
				return;
			if (isChar && !MovingItem.GetComponent<MovingItem> ().item is Armor)//armor checks
			{
				if (idNum == 4 || idNum == 5)//trying to place armor in weapon slot
					return;
				if (((Armor)MovingItem.GetComponent<MovingItem> ().item).equipType != idNum) //trying to put wrong armor in slot, ex pants in helmet slot
					return;
			}
			inventory.itemSlots [idNum] = MovingItem.GetComponent<MovingItem>().item;
			amount = MovingItem.GetComponent<MovingItem>().amount;
			inventory.slotAmnts [idNum] = amount;
			if(!isChar)
				amountText.text = "" + amount;
			MovingItem.GetComponent<MovingItem> ().item = null;
			MovingItem.GetComponent<MovingItem> ().amount = 0;
			pic.sprite = MovingItem.GetComponent<Image> ().sprite;
			MovingItem.GetComponent<Image> ().sprite = null;
			pic.gameObject.SetActive (true);
			MovingItem.GetComponent<Image> ().enabled = false;
			if(isChar)
			{
				if(inventory.itemSlots [idNum] is Weapon)//update player attack with weapon attack
				{
					player.attack = (player.attack + ((Weapon)inventory.itemSlots [idNum]).atk);
					player.updateText ();
				}
				GameObject clone = null;
				if(idNum == 4)//right hand weapon
				{
					clone = Instantiate (inventory.itemSlots [idNum].gameObject, gearPos[0].position, Quaternion.identity);
					clone.transform.parent = gearPos[0];
				}
				else if (idNum == 5) //left hand weapon
				{
					clone = Instantiate (inventory.itemSlots [idNum].gameObject, gearPos[1].position, Quaternion.identity);
					clone.transform.parent = gearPos[1];
				}
				clone.transform.localEulerAngles = new Vector3 (0, 0, 0);
				clone.transform.localScale = new Vector3 (0.5F, 0.5F, 0.75F);
				clone.transform.localPosition = new Vector3 (0, 0, 0);
			}
		}

		else if (inventory.itemSlots[idNum] != null && MovingItem.GetComponent<MovingItem>().item != null)//if holding item and slot has item, switch em
		{
			if(inventory.control != null)
				inventory.control.playClick ();
			Debug.Log ("trying to switch items");
			if ((isChar && !(MovingItem.GetComponent<MovingItem> ().item is Weapon || MovingItem.GetComponent<MovingItem> ().item is Armor)))//not trying to equip non equipable item
				return;
			if (isChar && !MovingItem.GetComponent<MovingItem> ().item is Weapon && (idNum == 4 || idNum == 5))//trying to place non weapon in weapon space
				return;
			if (isChar && MovingItem.GetComponent<MovingItem> ().item is Weapon && !(idNum == 4 || idNum == 5))//trying to place non weapon in weapon space
				return;
			if (isChar && !MovingItem.GetComponent<MovingItem> ().item is Armor)//armor checks
			{
				if (idNum == 4 || idNum == 5)//trying to place armor in weapon slot
					return;
				if (((Armor)MovingItem.GetComponent<MovingItem> ().item).equipType != idNum) //trying to put wrong armor in slot, ex pants in helmet slot
					return;
			}
			//hold current inventory stuff in temp vars
			Item temp = inventory.itemSlots [idNum];
			int tempAmnt = amount;
			Sprite tempSprite = pic.sprite;

			inventory.itemSlots [idNum] = MovingItem.GetComponent<MovingItem>().item;
			amount = MovingItem.GetComponent<MovingItem>().amount;
			inventory.slotAmnts [idNum] = amount;
			if(!isChar)
				amountText.text = "" + amount;
			pic.sprite = MovingItem.GetComponent<Image> ().sprite;

			MovingItem.GetComponent<MovingItem> ().item = temp;
			MovingItem.GetComponent<MovingItem> ().amount = tempAmnt;
			MovingItem.GetComponent<Image> ().sprite = tempSprite;

			if(isChar)
			{
				if(inventory.itemSlots [idNum] is Weapon)//update player attack with weapon attack
				{
					
					player.attack = (player.attack + ((Weapon)inventory.itemSlots [idNum]).atk - ((Weapon)MovingItem.GetComponent<MovingItem> ().item).atk);
					player.updateText ();
				}
				GameObject clone = null;
				if(idNum == 4)//right hand weapon
				{
					GameObject.Destroy (gearPos[0].GetChild(0).gameObject);
					clone = Instantiate (inventory.itemSlots [idNum].gameObject, gearPos[0].position, Quaternion.identity);
					clone.transform.parent = gearPos[0];
				}
				else if (idNum == 5) //left hand weapon
				{
					GameObject.Destroy (gearPos[1].GetChild(0).gameObject);
					clone = Instantiate (inventory.itemSlots [idNum].gameObject, gearPos[1].position, Quaternion.identity);
					clone.transform.parent = gearPos[1];
				}
				clone.transform.localEulerAngles = new Vector3 (0, 0, 0);
				clone.transform.localScale = new Vector3 (0.5F, 0.5F, 0.75F);
				clone.transform.localPosition = new Vector3 (0, 0, 0);
			}
		}

		else if (inventory.itemSlots[idNum] != null && MovingItem.GetComponent<MovingItem>().item == null)//dont have item, take it from slot
		{
			if(inventory.control != null)
				inventory.control.playClick ();
			MovingItem.GetComponent<MovingItem> ().item = inventory.itemSlots [idNum];
			MovingItem.GetComponent<MovingItem> ().amount = amount;
			MovingItem.GetComponent<Image> ().sprite = pic.sprite;

			inventory.itemSlots [idNum] = null;
			inventory.slotAmnts [idNum] = 0;
			amount = 0;
			if(!isChar)
				amountText.text = "";
			pic.sprite = null;
			pic.gameObject.SetActive (false);
			MovingItem.GetComponent<Image> ().enabled = true;

			if(isChar)
			{
				if(MovingItem.GetComponent<MovingItem>().item is Weapon)//update player attack with weapon attack
				{
					player.attack = (player.attack - ((Weapon)MovingItem.GetComponent<MovingItem> ().item).atk);
					player.updateText ();
					if(idNum == 4)//right hand weapon
					{
						GameObject.Destroy (gearPos[0].GetChild(0).gameObject);
					}
					else if (idNum == 5) //left hand weapon
					{
						GameObject.Destroy (gearPos[1].GetChild(0).gameObject);
					}
				}
				else if (MovingItem.GetComponent<MovingItem>().item is Armor)
				{
					player.defense = (player.defense - ((Armor)MovingItem.GetComponent<MovingItem> ().item).defense);
					player.updateText ();
				}
			}
		}
	}

	public void consumeItem()
	{
		if (atHomeBase)//dont want to consume items at homebase
			return;

		Debug.Log ("Consume item");
		if(inventory.itemSlots[idNum] is ConsumableItem)
		{
			if (((ConsumableItem)inventory.itemSlots [idNum]).Consume ())
			{
				Debug.Log ("returned true, decrement item");
				inventory.slotAmnts [idNum]--;
				if(inventory.slotAmnts[idNum] <= 0)
				{
					inventory.itemSlots [idNum] = null;
					inventory.slotAmnts [idNum] = 0;
					amount = 0;
					if(!isChar)
						amountText.text = "";
					pic.sprite = null;
					pic.gameObject.SetActive (false);
					tooltip.gameObject.SetActive (false);
				}
				else
				{
					amount--;
					amountText.text = ""+amount;
				}
			}
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Left)
			moveItem ();
		else if (eventData.button == PointerEventData.InputButton.Right)
			consumeItem ();
	}

	public void onEnter()
	{
		if (inventory.itemSlots [idNum] != null) 
		{
			tooltip.gameObject.SetActive (true);
			tooltip.updateText (inventory.itemSlots [idNum].name, inventory.itemSlots [idNum].desc);
		}
	}

	public void onExit()
	{
		tooltip.gameObject.SetActive (false);
	}
}
