using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCharacter
{
	public Transform battleCharacter;
	public Text nameText;
	public Text hpText;
	public Slider slider;
	public Transform storyCharacter;
	//public EnemyStats stats;
	public Stats stats;
	public int location;

	public EnemyCharacter()
	{
		stats = new Stats();
		location = 0;
	}

	//public Transform SpawnBattleCharacter(bool isOnRight, Transform battleManagerTransform)
	//{
	//	Vector3 startingPosition;

	//	if (isOnRight)
	//	{
	//		startingPosition = new Vector3(3, location, -8);
	//		characterTransform = Instantiate(characterTransform, startingPosition, Quaternion.identity, battleManagerTransform);
	//	}
	//	else
	//	{
	//		startingPosition = new Vector3(-3, location, -8);
	//		characterTransform = Instantiate(characterTransform, startingPosition, Quaternion.identity * Quaternion.Euler(0f, 180f, 0f), battleManagerTransform);
	//	}

	//	return characterTransform;
	//}
}
