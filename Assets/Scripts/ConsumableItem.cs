using UnityEngine;
using System.Collections;

//child of item, one time use consumable items
public class ConsumableItem : Item
{

	public int hpAmnt; //amount restored if hp, food or water, can be negative to harm
	public int foodAmnt;
	public int waterAmnt;
	public int poisonType;//if 0, no effect, if positive gives negative effect, if -1 its medicene that removes negative effects
	public PlayerStats player;

	public ConsumableItem()
	{
		
	}

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public bool Consume()//returns true if consumed, else not consumed
	{
		if (!player)//since using prefab, have to assign at runtime
		{
			if(GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerStats> () == null)
				player = GameObject.FindGameObjectWithTag ("Player").transform.parent.GetComponent<PlayerStats> ();
			else
				player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerStats> ();
		}


		//hp
		player.hp += hpAmnt;
		if (player.hp > player.maxHp)
			player.hp = player.maxHp;
		else if (player.hp <= 0)
		{
			//player dies
		}
		player.updateHPBar ();

		//food
		if(player.getFood()+foodAmnt > 100)
			player.setFood (100);
		else if(player.getFood()+foodAmnt <= 0)
			player.setFood (0);
		else
			player.setFood (player.getFood() + foodAmnt);
		
		//water
		if(player.getWater()+waterAmnt > 100)
			player.setWater (100);
		else if(player.getWater()+waterAmnt <= 0)
			player.setWater(0);
		else
			player.setWater (player.getWater() + waterAmnt);


		return true;
	}
}

