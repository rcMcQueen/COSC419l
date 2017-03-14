using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

	public Animator anim;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
		anim.SetFloat ("x", 0);
		anim.SetFloat ("z", 0);

		if(Input.GetKey("w"))
		{
			this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x,this.gameObject.transform.position.y,this.gameObject.transform.position.z+0.1F);
			anim.SetFloat ("z", 1);
		}
		if(Input.GetKey("a"))
		{
			this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x-0.1F,this.gameObject.transform.position.y,this.gameObject.transform.position.z);
			anim.SetFloat ("x", -1);
		}
		if(Input.GetKey("s"))
		{
			this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x,this.gameObject.transform.position.y,this.gameObject.transform.position.z-0.1F);
			anim.SetFloat ("z", -1);
		}
		if(Input.GetKey("d"))
		{
			this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x+0.1F,this.gameObject.transform.position.y,this.gameObject.transform.position.z);
			anim.SetFloat ("x", 1);
		}
		if(Input.GetMouseButtonDown(0))//left click
		{
			this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x+0.1F,this.gameObject.transform.position.y,this.gameObject.transform.position.z);
		}
	}
}
