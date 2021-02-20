using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHandler : MonoBehaviour
{
	private Animator animator;

	public Slider timerSlider;
	//public EnemyStats enemyStats;
	public Stats stats;
	public bool isTurn = false;
	public float timer = 0;
	public bool isSelected = false;
	public bool isAttacking = false;

	private void Awake()
	{
		animator = GetComponent<Animator>();
		animator.keepAnimatorControllerStateOnDisable = true;
		stats = new Stats();
		//enemyStats = new EnemyStats();
	}

	// Start is called before the first frame update
	void Start()
	{
		//timer = stats.SetTimer();
		//isAttacking = false;
	}

	// Update is called once per frame
	void Update()
	{
		if (!isTurn && !stats.dead)
		{
			if (timer >= stats.timer)
			{
				isTurn = true;
				//this.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
			}
			else
			{
				timer += Time.deltaTime;
			}
		}
		else if (isTurn)
		{
			//this.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
		}

		// This will need to be highlighted or something else.
		if (isSelected)
		{
			this.gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
		}
		else
		{
			this.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
		}
		
		//Might add keycodes to move the selection of the buttons or something later.
		//Instead of using mouse.

		//if (Input.GetKey(KeyCode.A))
		//{
		//	isAttacking = true;
		//}

		//if (isAttacking)
		//{
		//	animator.SetTrigger("RightAttacking");
		//	isAttacking = false;
		//}
		//else
		//{
		//	animator.SetBool("isWalking", true);
		//}
	}

	public void Attacking()
	{

		//animator.SetBool("isWalking", false);
		// This should go to the Enemy/Player Managers and see if they are using left or right handed weapon.
		//animator.SetTrigger("RightAttacking");
		//isTurn = false;
		//var t = testsss();
		isAttacking = true;
		animator.SetTrigger("RightAttacking");

		//// Testing this StartCoroutine. Might need a longer animation to test better behavior.
		//StartCoroutine(CheckAnimationCompleted("Cecil_Attack_Right", () =>
		//{
		//	//animator.SetTrigger("RightAttacking");
			
		//	isTurn = false;
		//	isAttacking = false;
		//}
	 //  ));
	}
	
	//public IEnumerable testsss()
	//{
	//	do
	//	{
	//		yield return null;
	//	} while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);
	//	//yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length + animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
	//}

	public void BeingHit(float dmg)
	{
		animator.SetTrigger("IsHit");

		if (stats != null && stats.hp > 0)
		{
			stats.hp -= dmg;
			if (stats.hp <= 0)
			{
				isTurn = false;
				stats.hp = 0;
				stats.ToggleDeath();
			}
			var percent = (stats.hp / stats.maxHp) * 100;
			animator.SetFloat("Health", percent);
		}
	}

	public void Setup(bool isOnRight)
	{
		if (isOnRight)
		{
			animator.SetBool("isWalking", true);
			//characterBase.SetAnimationReady();
			//characterBase.GetMaterial().mainTexture = BattleManager.GetInstance().playerSpriteSheet;
		}
		else
		{
			animator.SetBool("isWalking", true);
			//characterBase.SetAnimationWait();
			//characterBase.GetMaterial().mainTexture = BattleManager.GetInstance().enemySpriteSheet;
		}
	}

	//public IEnumerator CheckAnimationCompleted(string CurrentAnim, Action Oncomplete)
	//{
	//	isAttacking = true;
	//	//yield return null;
	//	while (!animator.GetCurrentAnimatorStateInfo(0).IsName(CurrentAnim))
	//		yield return null;
	//	//Oncomplete?.Invoke();
	//}

	public void AttackingEnded()
	{
		isTurn = false;
		isAttacking = false;
		timer = 0;
	}
}
