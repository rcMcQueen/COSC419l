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
	bool attacked;
	Transform tempPos;

	public Animation anim;
	public Animation[] clips;


	// Use this for initialization
	void Start () {
		hp = 10;
		attack = 4;
		defense = 0;
		stopMoving = false;
		trackingPlayer = false;
		attacked = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(attacked)
		{
			if (!anim.isPlaying)
				attacked = false;
		}
		if (trackingPlayer && !stopMoving)
		{
			anim.clip = clips[5].GetClip("run");
			anim.Play ();
			GetComponent<NavMeshAgent>().destination = player.transform.position;
		}
		else if (trackingPlayer && stopMoving)
		{
			this.gameObject.transform.position = tempPos.position;
			GetComponent<NavMeshAgent> ().Warp (tempPos.position);
			if(anim.clip == clips[5].GetClip("run"))
			{
				anim.Stop();
			}
			if(!attacked)
			{
				anim.clip = clips[0].GetClip("attack01");
				anim.Play ();
			}
		}
	}

	public void dealDmg(int dmg)
	{
		Debug.Log ("enemy dmg taken, HP: " + hp);
		hp = hp -(dmg - defense);
		anim.Stop ();
		anim.clip = clips[2].GetClip("damage");
		anim.Play ();
		attacked = true;
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
