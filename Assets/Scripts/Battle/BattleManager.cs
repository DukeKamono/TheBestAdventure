using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
	private GameData game;
	private static BattleManager instance;

	public static BattleManager GetInstance()
	{
		return instance;
	}

	//[SerializeField] private Transform battleCharacter;
	//[SerializeField] private Transform enemyBattleCharacter;
	[SerializeField] private PlayerManager playerManager;
	[SerializeField] private EnemyManager enemyManager;
	//public Texture2D playerSpriteSheet;
	//public Texture2D enemySpriteSheet;
	public List<Slider> sliders;
	public List<Text> nameTexts;
	public List<Text> hpTexts;
	public List<Slider> enemySliders;
	public List<Text> enemyNameTexts;
	public List<Text> enemyHpTexts;
	public List<Slider> playerTimerSliders;
	public GameObject playerChoosingPanel;

	private List<Transform> playerBattleCharacters;
	private List<Transform> enemyBattleCharacters;
	private Queue<Transform> playerBattleQueue;
	private Queue<Transform> enemyBattleQueue;
	//private bool playerChoosing = false;
	private RaycastHit2D target;

	private void Awake()
	{
		instance = this;

		playerBattleQueue = new Queue<Transform>();
		enemyBattleQueue = new Queue<Transform>();
		playerBattleCharacters = new List<Transform>();
		enemyBattleCharacters = new List<Transform>();

		game = FindObjectOfType<GameData>();

		//// Player setup, maybe change it to the current player list in the battle..
		//foreach (var text in nameTexts)
		//{
		//	foreach (var player in game.playerManager.playerCharacters)
		//	{
		//		if (player.nameText == null)
		//		{
		//			player.nameText = text;
		//			player.nameText.text = player.stats.name;
		//			player.nameText.gameObject.SetActive(true);
		//			break;
		//		}
		//	}
		//}

		//foreach (var text in hpTexts)
		//{
		//	foreach (var player in game.playerManager.playerCharacters)
		//	{
		//		if (player.hpText == null)
		//		{
		//			player.hpText = text;
		//			player.hpText.text = player.stats.hp + "/" + player.stats.maxHp;
		//			player.hpText.gameObject.SetActive(true);
		//			break;
		//		}
		//	}
		//}

		//foreach (var slider in sliders)
		//{
		//	foreach (var player in game.playerManager.playerCharacters)
		//	{
		//		if (player.slider == null)
		//		{
		//			player.slider = slider;
		//			player.slider.maxValue = player.stats.maxHp;
		//			player.slider.value = player.stats.hp;
		//			player.slider.gameObject.SetActive(true);
		//			break;
		//		}
		//	}
		//}

		//foreach (var slider in playerTimerSliders)
		//{
		//	foreach (var player in game.playerManager.playerCharacters)
		//	{
		//		if (player.slider == null)
		//		{
		//			player.slider = slider;
		//			player.slider.maxValue = player.stats.maxHp;
		//			player.slider.value = player.stats.hp;
		//			player.slider.gameObject.SetActive(true);
		//			break;
		//		}
		//	}
		//}

		Scene currentStoryScene = SceneManager.GetSceneByBuildIndex(game.currentStorySceneNumber);
		foreach (var s in currentStoryScene.GetRootGameObjects())
		{
			playerManager = game.playerManager;//s.GetComponentInChildren<PlayerManager>();

			//Find all enemies on the current scene and load who is being attacked.
			//This could take awhile on big maps....
			var enemies = s.GetComponentsInChildren<EnemyManager>();
			foreach (var potentialEnemy in enemies)
			{
				if (potentialEnemy.beingAttacked == true && potentialEnemy.battleCharacters != null)
				{
					enemyManager = potentialEnemy;
				}
			}

			s.SetActive(false);
		}

		// Enemy setup
		foreach (var text in enemyNameTexts)
		{
			foreach (var enemy in enemyManager.enemyCharacters)
			{
				if (enemy.nameText == null)
				{
					enemy.nameText = text;
					enemy.nameText.text = enemy.stats.name;
					enemy.nameText.gameObject.SetActive(true);
					break;
				}
			}
		}

		foreach (var text in enemyHpTexts)
		{
			foreach (var enemy in enemyManager.enemyCharacters)
			{
				if (enemy.hpText == null)
				{
					enemy.hpText = text;
					enemy.hpText.text = enemy.stats.hp + "/" + enemy.stats.maxHp;
					enemy.hpText.gameObject.SetActive(true);
					break;
				}
			}
		}

		foreach (var slider in enemySliders)
		{
			foreach (var enemy in enemyManager.enemyCharacters)
			{
				if (enemy.slider == null)
				{
					enemy.slider = slider;
					enemy.slider.maxValue = enemy.stats.maxHp;
					enemy.slider.value = enemy.stats.hp;
					enemy.slider.gameObject.SetActive(true);
					break;
				}
			}
		}
	}

	// Start is called before the first frame update
	void Start()
	{
		Setup();

		//set timer or whatever here()
	}

	// Update is called once per frame
	void Update()
	{
		// Determine what is selected.
		if (Input.GetMouseButtonDown(0))
		{
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
			RaycastHit2D hit = Physics2D.RaycastAll(mousePos2D, Vector2.zero).FirstOrDefault();
			if (hit.collider != null)
			{
				Debug.Log("Hit " + hit.collider.gameObject.name);
				if (hit.collider.gameObject.CompareTag("Enemy") || hit.collider.gameObject.CompareTag("Player"))
				{
					//Set previous target to false if there was one..
					if (target.collider != null)
					{
						var test = playerManager.battleCharacters.Where(c => c == target.collider.gameObject);
						target.collider.gameObject.GetComponent<BattleHandler>().isSelected = false;
					}

					// Make new target..
					target = hit;
					// Set new target to true..
					target.collider.gameObject.GetComponent<BattleHandler>().isSelected = true;
				}
				else
				{
					Debug.Log("Not a valid object to select");
				}
			}
			else
			{
				// Did we not select a Ui element?
				if (!EventSystem.current.IsPointerOverGameObject())
				{
					// Deselect target.
					if (target.collider != null)
					{
						target.collider.gameObject.GetComponent<BattleHandler>().isSelected = false;
					}
					target = new RaycastHit2D();
				}
				
				//Debug.Log("No hit");
			}
			//Debug.Log("Mouse is down");
		}

		
		EnemyAttacking();

		EnqueueNextPlayerToAttack();

		EnqueueNextEnemyToAttack();

		UpdateUI();
	}

	private void Setup()
	{
		playerBattleCharacters = playerManager.SpawnBattleCharacters(true, instance.transform);

		enemyBattleCharacters = enemyManager.SpawnBattleCharacter(false, instance.transform);

		// Player setup
		foreach (var text in nameTexts)
		{
			foreach (var player in game.playerManager.playerCharacters.Where(c => c.stats.isInBattle))
			{
				if (player.nameText == null)
				{
					player.nameText = text;
					player.nameText.text = player.stats.name;
					player.nameText.gameObject.SetActive(true);
					break;
				}
			}
		}

		foreach (var text in hpTexts)
		{
			foreach (var player in game.playerManager.playerCharacters.Where(c => c.stats.isInBattle))
			{
				if (player.hpText == null)
				{
					player.hpText = text;
					player.hpText.text = player.stats.hp + "/" + player.stats.maxHp;
					player.hpText.gameObject.SetActive(true);
					break;
				}
			}
		}

		foreach (var slider in sliders)
		{
			foreach (var player in game.playerManager.playerCharacters.Where(c => c.stats.isInBattle))
			{
				if (player.slider == null)
				{
					player.slider = slider;
					player.slider.maxValue = player.stats.maxHp;
					player.slider.value = player.stats.hp;
					player.slider.gameObject.SetActive(true);
					break;
				}
			}
		}

		foreach (var slider in playerTimerSliders)
		{
			foreach (var player in playerBattleCharacters)
			{
				var pBattleHandler = player.GetComponent<BattleHandler>();
				if (pBattleHandler.timerSlider == null)
				{
					pBattleHandler.timerSlider = slider;
					pBattleHandler.timerSlider.maxValue = pBattleHandler.stats.timer;
					pBattleHandler.timerSlider.value = pBattleHandler.timer;
					pBattleHandler.timerSlider.gameObject.SetActive(true);
					break;
				}
			}
		}
	}

	public void Attacking()
	{
		if (target && !target.collider.gameObject.GetComponent<BattleHandler>().stats.dead)
		{
			if (playerBattleQueue.Count > 0 && !playerBattleCharacters.Where(c => c.GetComponent<BattleHandler>().isAttacking).Any())
			{
				//playerChoosing = false;
				var attacker = playerBattleQueue.Dequeue();
				attacker.GetComponent<BattleHandler>().Attacking();

				target.collider.gameObject.GetComponent<BattleHandler>().BeingHit(attacker.GetComponent<BattleHandler>().stats.atk);
			}
		}
		else
		{
			Debug.Log("Select a Target!");
		}
	}

	private void EnemyAttacking()
	{
		if (enemyBattleQueue.Count > 0 && playerBattleCharacters.Where(c => !c.GetComponent<BattleHandler>().stats.dead).Any() && !enemyBattleCharacters.Where(c => c.GetComponent<BattleHandler>().isAttacking).Any())
		{
			var attacker = enemyBattleQueue.Dequeue();
			var enemyBattleHandler = attacker.GetComponent<BattleHandler>();
			if (enemyBattleHandler.isTurn)
			{
				foreach (var player in playerBattleCharacters.Where(c => !c.GetComponent<BattleHandler>().stats.dead))
				{
					var playerBattleHandler = player.GetComponent<BattleHandler>();

					// Right now just attack the first one who is not dead..
					if (!playerBattleHandler.stats.dead)
					{
						playerBattleHandler.BeingHit(enemyBattleHandler.stats.atk);
						break;
					}
				}
				enemyBattleHandler.Attacking();
			}
			else
			{
				enemyBattleQueue.Enqueue(attacker);
			}
		}
	}

	// Might combine this with EnqueueNextPlayerToAttack
	private void EnqueueNextEnemyToAttack()
	{
		foreach (var enemy in enemyBattleCharacters)
		{
			var enemyBattleHandler = enemy.GetComponent<BattleHandler>();

			// It's my turn, i'm not dead, i'm not attacking, i'm not already in the queue. Add me.
			if (enemyBattleHandler.isTurn && !enemyBattleHandler.stats.dead && !enemyBattleHandler.isAttacking && !enemyBattleQueue.Where(p => p == enemy).Any())
			{
				enemyBattleQueue.Enqueue(enemy);
			}

			// If enemy died remove them from queue
			if (enemy.GetComponent<BattleHandler>().stats.dead)
			{
				enemyBattleQueue = new Queue<Transform>(enemyBattleQueue.Where(p => p != enemy));
			}
		}
	}

	private void EnqueueNextPlayerToAttack()
	{
		foreach (var player in playerBattleCharacters)
		{
			var playerBattleHandler = player.GetComponent<BattleHandler>();
			// It's my turn, i'm not dead, i'm not attacking, i'm not already in the queue. Add me.
			if (playerBattleHandler.isTurn && !playerBattleHandler.stats.dead && !playerBattleHandler.isAttacking && !playerBattleQueue.Where(p => p == player).Any())
			{
				playerBattleQueue.Enqueue(player);
				//playerChoosing = true;
			}

			// If player died remove them from queue
			if (player.GetComponent<BattleHandler>().stats.dead)
			{
				playerBattleQueue = new Queue<Transform>(playerBattleQueue.Where(p => p != player));
			}
		}
	}

	private void UpdateUI()
	{
		// We are not using playerBattleCharacters because the slider on the player will be used elsewhere. (i.e. Menu)
		foreach (var player in game.playerManager.playerCharacters.Where(c => c.stats.isInBattle))
		{
			player.slider.maxValue = player.stats.maxHp;
			player.slider.value = player.stats.hp;
			player.hpText.text = player.stats.hp + "/" + player.stats.maxHp;
		}

		foreach (var player in playerBattleCharacters)
		{
			var pBattleHandler = player.GetComponent<BattleHandler>();

			pBattleHandler.timerSlider.maxValue = pBattleHandler.stats.timer;
			pBattleHandler.timerSlider.value = pBattleHandler.timer;
		}

		foreach (var enemy in enemyManager.enemyCharacters)
		{
			enemy.slider.maxValue = enemy.stats.maxHp;
			enemy.slider.value = enemy.stats.hp;
			enemy.hpText.text = enemy.stats.hp + "/" + enemy.stats.maxHp;
		}

		if (playerBattleQueue.Count == 0)
		{
			playerChoosingPanel.gameObject.SetActive(false);
		}
		else
		{
			playerChoosingPanel.gameObject.SetActive(true);
		}
	}

	public void Resume()
	{
		Scene storyScene = SceneManager.GetSceneByBuildIndex(game.currentStorySceneNumber);
		foreach (var s in storyScene.GetRootGameObjects())
		{
			s.SetActive(true);
		}
		//Time.timeScale = 1;
		game.currentBattleSceneNumber = 0; // Set battle number to default 0.
		SceneManager.UnloadSceneAsync("BattleScene");
		Destroy(gameObject);
	}
}
