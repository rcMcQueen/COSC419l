using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttackHitbox : MonoBehaviour {

	public Character stats;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other)
	{
		if (this.gameObject.transform.parent.tag == other.tag)
			return;
		if(other.tag == "enemy")//player attacks enemy
		{
			other.gameObject.GetComponent<Enemy>().dealDmg (stats.attack);
		}

		if(other.tag == "Player")
		{
			if(other.gameObject.GetComponent<PlayerStats>() == null)
				other.gameObject.transform.parent.GetComponent<PlayerStats>().takeDmg (stats.attack);
			else
				other.gameObject.GetComponent<PlayerStats>().takeDmg (stats.attack);
		}
	}
}
