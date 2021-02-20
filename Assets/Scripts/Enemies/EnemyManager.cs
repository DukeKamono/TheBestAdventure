using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
	[SerializeField] public Transform[] battleCharacters;
	public List<EnemyCharacter> enemyCharacters;

	public bool beingAttacked = false;

	// Start is called before the first frame update
	void Start()
	{
		enemyCharacters = new List<EnemyCharacter>();
		var spot = 0;
		foreach (var bc in battleCharacters.Where(c => c != null))
		{
			var newCharacter = new EnemyCharacter
			{
				battleCharacter = bc,
				location = spot
			};
			enemyCharacters.Add(newCharacter);
			spot += 2;
		}
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player" && SceneManager.GetSceneByName("BattleScene").name != "BattleScene")
		{
			beingAttacked = true;
			//Time.timeScale = 0;
			// Update this later for different battlescenes?
			SceneManager.LoadScene("BattleScene", LoadSceneMode.Additive);
			FindObjectOfType<GameData>().currentBattleSceneNumber = SceneManager.GetSceneByName("BattleScene").buildIndex;
		}
	}

	public List<Transform> SpawnBattleCharacter(bool isOnRight, Transform battleManagerTransform)
	{
		Vector3 startingPosition;
		Transform characterTransform;
		List<Transform> spawnedCharacters = new List<Transform>();
		var spot = 0; //This needs to be dynamic later...

		foreach (var c in enemyCharacters.Where(c => c != null))
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
		
		return spawnedCharacters;
	}
}
