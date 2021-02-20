using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats
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
	//public bool isTurn = false;

	public EnemyStats()
	{
		hp = 10f;
		level = 1;
		maxHp = 10f;
		agi = 5f;
		atk = 1;
		speed = 1;
		name = "Enemytest";
	}

	public bool ToggleDeath()
	{
		return dead = !dead;
	}

	public int SetTimer()
	{
		return (int)Math.Floor(agi * speed / Time.deltaTime);
	}
}
