using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerIcon : MonoBehaviour {

	public int bobHeight = 10;
	public int bobSpeed = 8;
	float yPos;
	public float moveSpeed;
	Vector3 targetPos;
	bool isMoving;

	// Use this for initialization
	void Start () {
		yPos = this.transform.position.y;
		isMoving = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(isMoving)
		{
			float step = moveSpeed * Time.deltaTime;
			this.gameObject.transform.position = Vector3.MoveTowards (transform.position, targetPos, step);
			if (transform.position == targetPos)
			{
				isMoving = false;
				yPos = transform.position.y;
			}
		}
		else //move player icon up and down for effect
			this.gameObject.transform.position = new Vector3 (this.gameObject.transform.position.x, yPos + Mathf.Sin(Time.time*bobSpeed)*bobHeight, this.gameObject.transform.position.z);
	}

	public void moveToPos(Vector3 newPos)
	{
		targetPos = newPos;
		isMoving = true;
	}

	public float getYPos()
	{
		return yPos;
	}

	public bool playerGoing()
	{
		return isMoving;
	}
}
