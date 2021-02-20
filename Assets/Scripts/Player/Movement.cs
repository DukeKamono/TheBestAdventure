using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
	public float speed;
	private Rigidbody2D currentRigidbody2D;
	private Vector3 prevPos;

	private void Awake()
	{
		currentRigidbody2D = GetComponent<Rigidbody2D>();
	}

	private void OnMouseDown()
	{
		
	}

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		
		//if (Input.GetMouseButtonDown(0))
		//{
		//	//var tempPos = transform.position;
		//	//tempPos.x += 1f;
		//	transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z);
		//}

		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			//var tempPos = transform.position;
			//tempPos.x -= 1f;
			//transform.position = tempPos;
			prevPos = transform.position;
			transform.Translate(Vector3.left * speed * Time.deltaTime);
		}

		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			//var tempPos = transform.position;
			//tempPos.x += 1f;
			//transform.position = tempPos;
			prevPos = transform.position;
			transform.Translate(Vector3.right * speed * Time.deltaTime);
		}

		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			//var tempPos = transform.position;
			//tempPos.y += 1f;
			//transform.position = tempPos;
			prevPos = transform.position;
			transform.Translate(Vector3.up * speed * Time.deltaTime);
		}

		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			//var tempPos = transform.position;
			//tempPos.y -= 1f;
			//transform.position = tempPos;
			prevPos = transform.position;
			transform.Translate(Vector3.down * speed * Time.deltaTime);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Enemy")
		{
			transform.position = prevPos;
		}
	}
}
