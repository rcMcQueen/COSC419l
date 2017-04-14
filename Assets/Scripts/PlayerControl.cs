using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

	public Animator anim;
	float xVel;
	float zVel;
	static float Incrementor = 0.05F;
	public float moveSpeed = 0.1F;
	bool Attacking;
	static int attackFrames = 27;
	int currAttackframes;
	public GameObject attackHitBox;
	public float rotateSpeed = 0.5F;
	public AudioSource audio;

	public AudioClip attackClip;
	public AudioClip moveClip;

	public bool paused = false;

	// Use this for initialization
	void Start () {
		Attacking = false;
		currAttackframes = 0;
		paused = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (paused)
			return;

		if(Attacking)
		{
			currAttackframes++;
			if(currAttackframes >= attackFrames)
			{
				currAttackframes = 0;
				Attacking = false;
				anim.SetBool ("attack", false);
				attackHitBox.SetActive (false);
			}
			return;
		}
	

		if(Input.GetKey("w"))
		{
			this.gameObject.transform.position += transform.forward * Time.deltaTime*moveSpeed;
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
			this.gameObject.transform.position -= transform.right * Time.deltaTime*moveSpeed;
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
			this.gameObject.transform.position -= transform.forward * Time.deltaTime*moveSpeed;
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
			this.gameObject.transform.position += transform.right * Time.deltaTime*moveSpeed;
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

		if(xVel != 0 || zVel != 0)
		{
			if(!audio.isPlaying)
			{
				audio.clip = moveClip;
				audio.Play ();
			}
		}
		else
		{
			if (!Attacking)
				audio.Stop ();
		}

		if(Input.GetMouseButtonDown(0))//left click
		{
			if(!Attacking)
			{
				audio.clip = attackClip;
				audio.Play ();
				attackHitBox.SetActive (true);
				Attacking = true;
				anim.SetBool("attack",true);
			}
		}

		if(Input.GetKey("q"))
		{
			this.gameObject.transform.localEulerAngles = new Vector3 (this.gameObject.transform.localEulerAngles.x, this.gameObject.transform.localEulerAngles.y - rotateSpeed, this.gameObject.transform.localEulerAngles.z);
		}

		if(Input.GetKey("e"))
		{
			this.gameObject.transform.localEulerAngles = new Vector3(this.gameObject.transform.localEulerAngles.x,this.gameObject.transform.localEulerAngles.y+rotateSpeed,this.gameObject.transform.localEulerAngles.z);
		}
	}
}
