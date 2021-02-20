using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		//if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetSceneByName("PauseScene").name != "PauseScene")
		//{
		//	SceneManager.LoadScene("PauseScene", LoadSceneMode.Additive);
		//	//Scene storyScene = SceneManager.GetSceneByName("StoryScene");
		//	Scene currentStoryScene = SceneManager.GetSceneByBuildIndex(FindObjectOfType<GameData>().currentSceneNumber);
		//	foreach (var s in currentStoryScene.GetRootGameObjects())
		//	{
		//		s.SetActive(false);
		//	}
		//}
	}
}
