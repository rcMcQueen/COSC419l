using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy : MonoBehaviour {

	public GameObject player;

	int hp;
	int attack;
	int defense;

	bool trackingPlayer;
	bool stopMoving;
	Transform tempPos;


	// Use this for initialization
	void Start () {
		hp = 10;
		attack = 4;
		defense = 0;
		stopMoving = false;
		trackingPlayer = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (trackingPlayer && !stopMoving)
			GetComponent<NavMeshAgent>().destination = player.transform.position;
		else if (trackingPlayer && stopMoving)
		{
			this.gameObject.transform.position = tempPos.position;
			GetComponent<NavMeshAgent> ().Warp (tempPos.position);
		}
	}

	public void dealDmg(int dmg)
	{
		Debug.Log ("enemy dmg taken, HP: " + hp);
		hp = hp -(dmg - defense);
		if(hp <= 0)//TODO do better death stuff
		{
			Debug.Log ("Enemy dead");
			Destroy (this.gameObject);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			trackingPlayer = true;
			GetComponent<NavMeshAgent> ().destination = player.transform.position;
			//resize box collider to just around model
			GetComponent<BoxCollider> ().size = new Vector3 (15, 30, 15);
			GetComponent<BoxCollider> ().center = new Vector3 (0, 7, 0);
		}

		if(other.tag == "Player" && trackingPlayer) //stops AI from literally spinning around player
		{
			stopMoving = true;
			tempPos = this.gameObject.transform;
		}
	}

	void OnTriggerExit(Collider other)
	{
		stopMoving = false;
	}
}
