using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class craftingSlot : MonoBehaviour {

	public Image pic;
	public toolTip tooltip;
	public string itemName;
	public string desc;
	public bool active = false;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {   

	}

	public void setItem(Item item, Sprite icon,string name, string desc)
	{
		pic.sprite = icon;
		itemName = name;
		this.desc = desc;
		active = true;
	}

	public void emptySlot()
	{
		pic.sprite = null;
		itemName = "";
		desc = "";
		active = false;
	}

	public void onEnter()
	{
		if (active == false)//dont display tooltip on empty slot
			return;
		tooltip.gameObject.SetActive (true);
		tooltip.updateText (itemName,desc);
	}

	public void onExit()
	{
		tooltip.gameObject.SetActive (false);
	}
}
