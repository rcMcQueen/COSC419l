using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class timeTracker : MonoBehaviour {

	public string oldTime;

	// Use this for initialization
	void Awake () {
		if(PlayerPrefs.HasKey("Quit Time"))
			oldTime = PlayerPrefs.GetString ("Quit Time");
		else
		{
			oldTime = DateTime.Now.ToString ();
			PlayerPrefs.SetString ("Quit Time", DateTime.Now.ToString());
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnApplicationQuit()//sets leaving time plus whatever was left over from the loot timer
	{
		//PlayerPrefs.SetString ("Quit Time", (DateTime.Now.Subtract (DateTime.Parse(oldTime))).ToString()); //was setting timespan, not time
		updateTime ((DateTime.Now.Subtract (DateTime.Parse (oldTime))));
	}

	public float getTimeDiffrence()//returns time passed since last leaving the game and when this is called in minutes
	{
		oldTime = PlayerPrefs.GetString ("Quit Time");
		TimeSpan newTime = DateTime.Now.Subtract (DateTime.Parse(oldTime));
		return (float)newTime.TotalMinutes;
	}

	public void updateTime(float leftOverTime)//called when getting worker loot
	{
		oldTime = (DateTime.Now.Subtract (TimeSpan.FromMinutes ((double)leftOverTime))).ToString ();
		PlayerPrefs.SetString ("Quit Time", DateTime.Parse(oldTime).ToString());
	}

	public void updateTime(TimeSpan leftOverTime)//called when getting worker loot
	{
		oldTime = (DateTime.Now.Subtract (leftOverTime)).ToString ();
		PlayerPrefs.SetString ("Quit Time", DateTime.Parse (oldTime).ToString ());
	}
}



