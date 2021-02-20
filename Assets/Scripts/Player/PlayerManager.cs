using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
	public Transform[] battleCharacters;
	//public List<PlayerCharacter> playerBattleCharacters;
	public List<PlayerCharacter> playerCharacters;

	// Start is called before the first frame update
	void Start()
	{
		playerCharacters = new List<PlayerCharacter>();
		var spot = 0;
		foreach (var bc in battleCharacters.Where(c => c != null))
		{
			var newCharacter = new PlayerCharacter
			{
				battleCharacter = bc,
				location = spot
			};
			playerCharacters.Add(newCharacter);
			spot += 2;
		}
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	public List<Transform> SpawnBattleCharacters(bool isOnRight, Transform battleManagerTransform)
	{
		Vector3 startingPosition;
		Transform characterTransform;
		List<Transform> spawnedCharacters = new List<Transform>();
		var spot = 0; //This needs to be dynamic later...

		foreach (var c in playerCharacters.Where(c => c != null && c.stats.isInBattle))
		{
			if (isOnRight)
			{
				startingPosition = new Vector3(3, spot, -8);
				characterTransform = Instantiate(c.battleCharacter, startingPosition, Quaternion.identity, battleManagerTransform);
			}
			else
			{
				startingPosition = new Vector3(-3, spot, -8);
				characterTransform = Instantiate(c.battleCharacter, startingPosition, Quaternion.identity * Quaternion.Euler(0f, 180f, 0f), battleManagerTransform);
			}

			BattleHandler battleHandler = characterTransform.GetComponent<BattleHandler>();
			battleHandler.Setup(isOnRight);
			battleHandler.stats = c.stats;

			spawnedCharacters.Add(characterTransform);
			spot += 2;
		}

		//foreach (var c in playerCharacters)
		//{
		//	c.SpawnBattleCharacter(isOnRight, battleManagerTransform);
		//}


		return spawnedCharacters;
	}
}
