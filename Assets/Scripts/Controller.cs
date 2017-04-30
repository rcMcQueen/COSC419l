using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

	public GameObject invCanvas;
	public GameObject lootCanvas;
	public GameObject charSheet;
	public bool[] menusOpen;
	public int numMenus = 0;
	public bool inLootRange = false;
	public Slot[] lootSlots;
	public GameObject player;

	public AudioSource audio;

	public AudioClip crateOpen;
	public AudioClip menuOpen;
	public AudioClip clickEffect;

	public GameObject tooltip;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(player)
			this.transform.position = player.transform.position;//for audio purposes

		if (numMenus > 0)
		{
			if(player)
			{
				player.GetComponent<PlayerControl> ().paused = true;
				player.GetComponent<PlayerStats> ().paused = true;
			}
			Time.timeScale = 0;
		}
		else
			
		{
			if(player)
			{
				player.GetComponent<PlayerControl> ().paused = false;
				player.GetComponent<PlayerStats> ().paused = false;
			}
			Time.timeScale = 1;
		}

		if(Input.GetKeyDown("i"))
		{
			tooltip.SetActive (false);
			audio.clip = menuOpen;
			audio.Play ();
			invCanvas.SetActive (!invCanvas.activeSelf);
			menusOpen [0] = !menusOpen [0];
			if(menusOpen[0])
				numMenus++;
			else
				numMenus--;
		}

		if(Input.GetKeyDown("l") && inLootRange)
		{
			tooltip.SetActive (false);
			audio.clip = crateOpen;
			audio.Play ();
			lootCanvas.SetActive (!lootCanvas.activeSelf);
			menusOpen [1] = !menusOpen [1];
			if(menusOpen[1])
				numMenus++;
			else
				numMenus--;
		}

		if(Input.GetKeyDown("c"))
		{
			tooltip.SetActive (false);
			audio.clip = menuOpen;
			audio.Play ();
			charSheet.SetActive (!charSheet.activeSelf);
			menusOpen [2] = !menusOpen [2];
			if(menusOpen[2])
				numMenus++;
			else
				numMenus--;
		}
	}

	public void lootUpdate()
	{
		for(int x = 0;x<lootSlots.Length;x++)
		{
			if (lootSlots [x].inventory.itemSlots [x] == null)
			{
				lootSlots [x].pic.sprite = null;
				lootSlots [x].amount = 0;
				lootSlots [x].amountText.text = "";
				lootSlots[x].pic.gameObject.SetActive (false);
				continue;
			}
			lootSlots [x].pic.sprite = lootSlots [x].inventory.itemSlots [x].icon;
			lootSlots[x].amount = lootSlots [x].inventory.slotAmnts[x];
			lootSlots [x].amountText.text = "" + lootSlots[x].amount;
			lootSlots[x].pic.gameObject.SetActive (true);
		}
	}

	public void playClick()
	{
		audio.clip = clickEffect;
		audio.Play ();
	}
}
