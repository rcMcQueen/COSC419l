using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * parent class for all player items
 */
public class Item : MonoBehaviour {

	 int ID;
	Sprite icon;
	public int ammoType;//0 = not ammo

	public Item(int ID,Sprite icon)
	{
		this.ID = ID;
		this.icon = icon;
	}

	public Item(int ID)//set icon based on ID?
	{
		this.ID = ID;
		//icon stuff here
	}

	public Item()
	{
		
	}
}
