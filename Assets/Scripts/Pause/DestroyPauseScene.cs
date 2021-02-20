using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyPauseScene : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public void Resume()
	{
		var game = FindObjectOfType<GameData>();

		// Are we in a battle(not 0) or not (0)? I want to change this later..
		if (game.currentBattleSceneNumber == 0)
		{
			Scene storyScene = SceneManager.GetSceneByBuildIndex(game.currentStorySceneNumber);
			foreach (var s in storyScene.GetRootGameObjects())
			{
				s.SetActive(true);
			}
			//Time.timeScale = 1;
			SceneManager.UnloadSceneAsync("PauseScene");
			Destroy(gameObject);
		}
		else
		{
			Scene battleScene = SceneManager.GetSceneByBuildIndex(game.currentBattleSceneNumber);
			foreach (var s in battleScene.GetRootGameObjects())
			{
				s.SetActive(true);
			}
			//Time.timeScale = 1;
			SceneManager.UnloadSceneAsync("PauseScene");
			Destroy(gameObject);
		}
		
	}
}
