using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryManager : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{
		FindObjectOfType<GameData>().currentStorySceneNumber = SceneManager.GetActiveScene().buildIndex;
	}

	// Update is called once per frame
	void Update()
	{
		//if (SceneManager.GetSceneByName("BattleScene").name == "BattleScene" || SceneManager.GetSceneByName("PauseScene").name == "PauseScene")
		//{
		//	gameObject.SetActive(false);
		//}
		//else
		//{
		//	gameObject.SetActive(true);
		//}
	}
}
