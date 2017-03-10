using UnityEngine;
using System.Collections;

//child of item, one time use consumable items
public class ConsumableItem : Item
{

	public ConsumableItem()
	{
		
	}

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public bool Consume()//returns true if consumed, else not consumed
	{
		//TODO do stuff here
		return true;
	}
}

