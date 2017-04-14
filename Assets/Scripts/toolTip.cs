using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class toolTip : MonoBehaviour {

	public Text nameText;
	public Text descText;

	public int xOffset = 70;
	public int yOffset = -55;

	static int panelHeight = 422;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.mousePosition.y + yOffset < 0)//flip x and/or y offset to put window in view when it would be outside screen
		{
			if (Input.mousePosition.x+xOffset > Screen.width)
				this.gameObject.transform.position = new Vector3 (Input.mousePosition.x-xOffset, Input.mousePosition.y-yOffset, 1);
			else
				this.gameObject.transform.position = new Vector3 (Input.mousePosition.x+xOffset, Input.mousePosition.y-yOffset, 1);
		}
			
		else
		{
			if (Input.mousePosition.x+xOffset > Screen.width)
				this.gameObject.transform.position = new Vector3 (Input.mousePosition.x-xOffset, Input.mousePosition.y+yOffset, 1);
			else
				this.gameObject.transform.position = new Vector3 (Input.mousePosition.x+xOffset, Input.mousePosition.y+yOffset, 1);
		}
			
	}

	public void updateText(string name,string desc)
	{
		nameText.text = name;
		descText.text = desc;
	}
}
