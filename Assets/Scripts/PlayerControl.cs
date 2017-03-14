using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

	public Animator anim;
	float xVel;
	float zVel;
	static float Incrementor = 0.05F;
	bool Attacking;
	static int attackFrames = 67;
	int currAttackframes;

	// Use this for initialization
	void Start () {
		Attacking = false;
		currAttackframes = 0;
	}
	
	// Update is called once per frame
	void Update () {

		if(Attacking)
		{
			currAttackframes++;
			if(currAttackframes >= attackFrames)
			{
				currAttackframes = 0;
				Debug.Log("stopped Attack Anim");
				Attacking = false;
				anim.SetBool ("attack", false);
			}
			return;
		}
	

		if(Input.GetKey("w"))
		{
			this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x,this.gameObject.transform.position.y,this.gameObject.transform.position.z+0.1F);
			zVel += Incrementor;
			if (zVel >= 1)
				zVel = 1;
			anim.SetFloat ("z", zVel);
		}
		else
		{
			if (zVel > 0)
				zVel -= Incrementor;
			if (zVel < Incrementor && zVel > Incrementor * -1)
				zVel = 0;
			anim.SetFloat ("z", zVel);
		}
		if(Input.GetKey("a"))
		{
			this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x-0.1F,this.gameObject.transform.position.y,this.gameObject.transform.position.z);
			xVel -= Incrementor;
			if (xVel <= -1)
				xVel = -1;
			anim.SetFloat ("x", xVel);
		}
		else
		{
			if (xVel < 0)
				xVel += Incrementor;
			if (xVel < Incrementor && xVel > Incrementor * -1)
				xVel = 0;
			anim.SetFloat ("x", xVel);
		}
		if(Input.GetKey("s"))
		{
			this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x,this.gameObject.transform.position.y,this.gameObject.transform.position.z-0.1F);
			zVel -= Incrementor;
			if (zVel <= -1)
				zVel = -1;
			anim.SetFloat ("z", zVel);

		}
		else
		{
			if (zVel < 0)
				zVel += Incrementor;
			if (zVel < Incrementor && zVel > Incrementor * -1)
				zVel = 0;
			anim.SetFloat ("z", zVel);
		}
		if(Input.GetKey("d"))
		{
			this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x+0.1F,this.gameObject.transform.position.y,this.gameObject.transform.position.z);
			xVel += Incrementor;
			if (xVel >= 1)
				xVel = 1;
			anim.SetFloat ("x", xVel);
		}
		else
		{
			if (xVel > 0)
				xVel -= Incrementor;
			if (xVel < Incrementor && xVel > Incrementor * -1)
				xVel = 0;
			anim.SetFloat ("x", xVel);
		}

		if(Input.GetKey("w") && Input.GetKey("s"))//opposite directions
		{
			anim.SetFloat ("z", 0);
		}

		if(Input.GetKey("a") && Input.GetKey("d"))//opposite directions
		{
			anim.SetFloat ("x", 0);
		}

		if(Input.GetMouseButtonDown(0))//left click
		{
			Debug.Log ("attacking animation triggered");
			if(!Attacking)
			{
				Attacking = true;
				anim.SetBool("attack",true);
			}
		}
	}
}
