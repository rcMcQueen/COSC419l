using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour {

	public playerStats stats;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "enemy")
		{
			other.gameObject.GetComponent<enemy>().dealDmg (stats.getAttack());
		}
	}
}
