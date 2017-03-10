using UnityEngine;
using System.Collections;

//weapon equiped by player
public class Weapon : Item
{
	public int atk;
	int atkSpd;
	int type;//0 = melee, 1 = ranged
	int range;//how far attack goes, very low for melee, farther for ranged
	int atkType;//0=straight line, 1 = cone
	public int equipType;//0=main hand, 1=off-hand,2=2-handed, 3 = 1-handed, used to equip mostly
	Item ammo;//points to ammo used in inventory to reduce when fired, null if melee
	int ammoType; //TODO figure out ammo types and values
	float atkTimer;
	Inventory inventory;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		atkTimer += Time.deltaTime;
	}

	public bool Attack()//returns true if successful attack
	{
		if ((type == 1) && (ammo == null)) {//ranged, but no ammo, can't attack
			return false;
		}
		else 
		{
			if(atkTimer < atkSpd)//cant attack yet, too fast
			{
				return false;
			}
			else//successful attack
			{
				if(atkType == 0)//straight line
				{
					//TODO attack in straight line with reach = range
				}
				else if (atkType == 1)//cone
				{
					//TODO attack in cone
				}

				if(type == 1)//if ranged, reduce ammo, check if there is still ammo left
				{
					if(!inventory.consumeBullet(ammo))
					{
						ammo = inventory.ammoSearch (ammoType);
					}
				}
				atkTimer = 0;//reset attack timer
			}
			return true;
		}
	}
}

