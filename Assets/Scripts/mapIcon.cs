using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class mapIcon : MonoBehaviour {

	public Sprite baseImg;
	public Sprite hoverImg;
	public Image pic;
	public int areaID;
	public playerIcon player;
	bool playerMoving;

	// Use this for initialization
	void Start () {
		playerMoving = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (playerMoving && !player.playerGoing ())//player arrived at this location, load scene now
			onClick ();
	}

	public void onHover()
	{
		pic.sprite = hoverImg;
	}

	public void onExit()
	{
		pic.sprite = baseImg;
	}

	public void onClick()
	{
		if(new Vector3(player.transform.position.x,player.getYPos()-30,player.transform.position.z) == transform.position)//player already here
		{
			loadScene ();
		}
		else
		{
			player.moveToPos (new Vector3 (transform.position.x, transform.position.y + 30, transform.position.z));
		}
	}

	public void loadScene()
	{
		if (areaID == 1)//load base 
		{
			SceneManager.LoadScene ("homebaseScreen");
		}
			
		else if (areaID == 2)//load new level
		{
			SceneManager.LoadScene ("NatureStarterKit2/Scene/Demo");
		}

		else if (areaID == 3)//start button
		{
			SceneManager.LoadScene ("initScene");
		}
			
	}

}
