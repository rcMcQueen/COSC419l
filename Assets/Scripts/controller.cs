using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controller : MonoBehaviour {

	public GameObject invCanvas;
	public GameObject lootCanvas;
	public GameObject charSheet;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("i"))
		{
			invCanvas.SetActive (!invCanvas.activeSelf);
		}

		if(Input.GetKeyDown("l"))//TODO change from get key to loot box specific
		{
			lootCanvas.SetActive (!lootCanvas.activeSelf);
		}

		if(Input.GetKeyDown("c"))
		{
			charSheet.SetActive (!charSheet.activeSelf);
		}
	}
}
