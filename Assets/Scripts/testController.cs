using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testController : MonoBehaviour {

	int currentAnim;
	public AnimationClip[] clips;
	public Animator controller;

	// Use this for initialization
	void Start () {
		currentAnim = 0;
		Debug.Log (clips [currentAnim].name);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void playNext()
	{
		currentAnim++;
		if (currentAnim > clips.Length-1)
			currentAnim = 0;
		controller.Play (clips [currentAnim].name);
		Debug.Log (clips [currentAnim].name);
	}

	public void playPrev()
	{
		currentAnim--;
		if (currentAnim < 0)
			currentAnim = clips.Length-1;
		controller.Play (clips [currentAnim].name);
		Debug.Log (clips [currentAnim].name);
	}

	void OnGUI()
	{
		if (GUI.Button (new Rect (10, 10, 50, 50), "Next"))
			playNext ();
		
		if (GUI.Button (new Rect (10, 100, 50, 50), "Prev"))
			playPrev ();
	}
}
