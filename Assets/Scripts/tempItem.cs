using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/*
 * parent class for all player items
 */
public class tempItem : MonoBehaviour {

	public Item item;
	public int amount;
	public Image icon;

	void Update()
	{

		this.gameObject.transform.position = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 1);
		if (item == null)
			icon.enabled = false;
	}
}
