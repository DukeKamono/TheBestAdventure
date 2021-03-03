using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats
{
	public string name;
	public int location;
	public int level;
	public int exp;
	public int expToNextLevel;
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
		exp = 0;
		expToNextLevel = 50;
		maxHp = 10f;
		agi = 5f;
		atk = 1;
		speed = 1;
		name = "test";
		timer = SetTimer();
		isInBattle = true;
	}

	public Stats(string n_name, float n_hp, float n_agi, float n_atk, int n_speed, bool n_isInBattle)
	{
		name = n_name;
		level = 1;
		exp = 0;
		expToNextLevel = 50 * level;
		hp = n_hp;
		maxHp = n_hp;
		agi = n_agi;
		atk = n_atk;
		speed = n_speed;
		isInBattle = n_isInBattle;
		timer = SetTimer();
	}

	public void CheckForLevelUp(int n_exp)
	{
		exp += n_exp;

		if (exp >= expToNextLevel)
		{
			LevelUp();
		}
	}

	private void LevelUp()
	{
		level++;
		exp = 0;
		expToNextLevel = 50 * level;
		hp += 5;
		maxHp = hp;
		agi += 1;
		atk += 1;
		timer = SetTimer();
	}

	public bool ToggleInBattle()
	{
		return isInBattle = !isInBattle;
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
