using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class slot : MonoBehaviour, IPointerClickHandler {
	
	public Image pic;
	public Text amountText;
	Sprite icon = null;
	public int amount=0;
	public int idNum;
	public GameObject tempitem;
	public Inventory inventory;
	public bool isChar = false;
	public playerStats player;
	public Transform gearPos;
	public bool isLoot;

	// Use this for initialization
	void Start () {
		if(inventory == null)
		{
			pic.gameObject.SetActive (false);
			return;
		}

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
		if(inventory.itemSlots[idNum] == null && tempitem.GetComponent<tempItem>().item != null)//have item and put it in empty slot
		{
			Debug.Log ("Trying to place item in empty slot");
			inventory.itemSlots [idNum] = tempitem.GetComponent<tempItem>().item;
			amount = tempitem.GetComponent<tempItem>().amount;
			if(!isChar)
				amountText.text = "" + amount;
			tempitem.GetComponent<tempItem> ().item = null;
			tempitem.GetComponent<tempItem> ().amount = 0;
			pic.sprite = tempitem.GetComponent<Image> ().sprite;
			tempitem.GetComponent<Image> ().sprite = null;
			pic.gameObject.SetActive (true);
			tempitem.GetComponent<Image> ().enabled = false;
			if(isChar)
			{
				GameObject clone = Instantiate (inventory.itemSlots [idNum].gameObject, gearPos.position, Quaternion.identity);
				clone.transform.parent = gearPos;
				clone.transform.localEulerAngles = new Vector3 (0, 0, 0);
				clone.transform.localScale = new Vector3 (0.5F, 0.5F, 0.75F);
				clone.transform.localPosition = new Vector3 (0, 0, 0);
			}
		}

		else if (inventory.itemSlots[idNum] != null && tempitem.GetComponent<tempItem>().item != null)//if holding item and slot has item, switch em
		{
			Debug.Log ("trying to switch items");
			//hold current inventory stuff in temp vars
			Item temp = inventory.itemSlots [idNum];
			int tempAmnt = amount;
			Sprite tempSprite = pic.sprite;

			inventory.itemSlots [idNum] = tempitem.GetComponent<tempItem>().item;
			amount = tempitem.GetComponent<tempItem>().amount;
			if(!isChar)
				amountText.text = "" + amount;
			pic.sprite = tempitem.GetComponent<Image> ().sprite;

			tempitem.GetComponent<tempItem> ().item = temp;
			tempitem.GetComponent<tempItem> ().amount = tempAmnt;
			tempitem.GetComponent<Image> ().sprite = tempSprite;

			if(isChar)
			{
				GameObject.Destroy (gearPos.GetChild(0));
				GameObject clone = Instantiate (inventory.itemSlots [idNum].gameObject, gearPos.position, Quaternion.identity);
				clone.transform.parent = gearPos;
				clone.transform.localEulerAngles = new Vector3 (0, 0, 0);
				clone.transform.localScale = new Vector3 (0.5F, 0.5F, 0.75F);
				clone.transform.localPosition = new Vector3 (0, 0, 0);
			}
		}

		else if (inventory.itemSlots[idNum] != null && tempitem.GetComponent<tempItem>().item == null)//dont have item, take it from slot
		{
			tempitem.GetComponent<tempItem> ().item = inventory.itemSlots [idNum];
			tempitem.GetComponent<tempItem> ().amount = amount;
			tempitem.GetComponent<Image> ().sprite = pic.sprite;

			inventory.itemSlots [idNum] = null;
			amount = 0;
			if(!isChar)
				amountText.text = "";
			pic.sprite = null;
			pic.gameObject.SetActive (false);
			tempitem.GetComponent<Image> ().enabled = true;

			if(isChar)
			{
				GameObject.Destroy (gearPos.GetChild(0).gameObject);
			}
		}
	}

	public void consumeItem()
	{
		Debug.Log ("Consume item");
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Left)
			moveItem ();
		else if (eventData.button == PointerEventData.InputButton.Right)
			consumeItem ();
	}
}
