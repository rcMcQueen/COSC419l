using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class slot : MonoBehaviour, IPointerClickHandler {
	
	public Image pic;
	public Text amountText;
	Sprite icon = null;
	int amount=0;
	public int idNum;
	public GameObject tempitem;
	public Inventory inventory;

	// Use this for initialization
	void Start () {
		if (icon == null)
			pic.gameObject.SetActive (false);
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
			inventory.itemSlots [idNum] = tempitem.GetComponent<tempItem>().item;
			amount = tempitem.GetComponent<tempItem>().amount;
			tempitem.GetComponent<tempItem> ().item = null;
			tempitem.GetComponent<tempItem> ().amount = 0;
			pic.sprite = tempitem.GetComponent<Image> ().sprite;
			tempitem.GetComponent<Image> ().sprite = null;
		}

		else if (inventory.itemSlots[idNum] != null && tempitem.GetComponent<tempItem>().item != null)//if holding item and slot has item, switch em
		{
			//hold current inventory stuff in temp vars
			Item temp = inventory.itemSlots [idNum];
			int tempAmnt = amount;
			Sprite tempSprite = pic.sprite;

			inventory.itemSlots [idNum] = tempitem.GetComponent<tempItem>().item;
			amount = tempitem.GetComponent<tempItem>().amount;
			pic.sprite = tempitem.GetComponent<Image> ().sprite;

			tempitem.GetComponent<tempItem> ().item = temp;
			tempitem.GetComponent<tempItem> ().amount = tempAmnt;
			tempitem.GetComponent<Image> ().sprite = tempSprite;
		}

		else if (inventory.itemSlots[idNum] != null && tempitem.GetComponent<tempItem>().item == null)//dont have item, take it from slot
		{
			tempitem.GetComponent<tempItem> ().item = inventory.itemSlots [idNum];
			tempitem.GetComponent<tempItem> ().amount = amount;
			tempitem.GetComponent<Image> ().sprite = pic.sprite;

			inventory.itemSlots [idNum] = null;
			amount = 0;
			pic.sprite = null;
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
