using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameData : MonoBehaviour
{
	private static bool GameDataExists;

	public PlayerManager playerManager;
	public int currentStorySceneNumber;
	public int currentBattleSceneNumber;

	//public Stats stats;

	// Start is called before the first frame update
	void Start()
	{
		//If this GameDataExists doesn't exists yet, make it true and don't destroy it.
		if (!GameDataExists)
		{
			GameDataExists = true;
			DontDestroyOnLoad(transform.gameObject);
		}
		else
		{
			Destroy(gameObject);//Destroy GameDataExists is one already exists
		}

		//stats = new Stats();
		playerManager = GetComponent<PlayerManager>();
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetSceneByName("PauseScene").name != "PauseScene" && SceneManager.GetSceneByName("StartScene").name != "StartScene")
		{
			SceneManager.LoadScene("PauseScene", LoadSceneMode.Additive);

			Scene currentStoryScene = SceneManager.GetSceneByBuildIndex(this.currentStorySceneNumber);
			if (currentStoryScene.name != null)
			{
				foreach (var s in currentStoryScene.GetRootGameObjects())
				{
					s.SetActive(false);
				}
			}

			Scene currentBattleScene = SceneManager.GetSceneByBuildIndex(this.currentBattleSceneNumber);
			if (currentBattleScene.name != null)
			{
				foreach (var s in currentBattleScene.GetRootGameObjects())
				{
					s.SetActive(false);
				}
			}
			
		}
	}
}
