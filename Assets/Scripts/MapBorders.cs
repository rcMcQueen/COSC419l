using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapBorders : MonoBehaviour {

	//stops player from leaving demo map

	public float xLimit = 100;
	public float zLimit = 100;
	public GameObject character;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (character.transform.position.x > xLimit)
			character.transform.position = new Vector3 (xLimit, character.transform.position.y, character.transform.position.z);
		else if (character.transform.position.x < xLimit*-1)
			character.transform.position = new Vector3 (-1*xLimit, character.transform.position.y, character.transform.position.z);
		else if (character.transform.position.z < zLimit*-1)
			character.transform.position = new Vector3 (character.transform.position.x, character.transform.position.y, -1*zLimit);
		else if (character.transform.position.z > zLimit)
			character.transform.position = new Vector3 (character.transform.position.x, character.transform.position.y, zLimit);
	}
}
