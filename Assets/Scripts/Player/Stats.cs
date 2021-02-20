using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats
{
	public string name;
	public int location;
	public int level;
	public float hp;
	public float maxHp;
	public float agi;
	public float atk;
	public int speed;
	public bool dead = false;
	public float timer;
	public bool isInBattle;

	public Stats()
	{
		hp = 10f;
		level = 1;
		maxHp = 10f;
		agi = 5f;
		atk = 1;
		speed = 1;
		name = "test";
		timer = SetTimer();
		isInBattle = true;
	}

	public bool ToggleDeath()
	{
		return dead = !dead;
	}

	public float SetTimer()
	{
		return 20 - agi * speed; //25 secs - (agi * speed)
	}
}
