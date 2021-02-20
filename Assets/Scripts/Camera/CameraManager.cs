using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
	public float MoveSpeed;
	public BoxCollider2D BoundBox;

	private GameObject FollowTarget;
	private Vector3 TargetPos;
	private static bool CameraExists;
	private Vector3 MinBounds;
	private Vector3 MaxBounds;
	private Camera TheCamera;
	private float HalfHeight;
	private float HalfWidth;

	// Use this for initialization
	void Start()
	{
		//If this camera doesn't exists yet, make it true and don't destroy it.
		//if (!CameraExists)
		//{
		//	CameraExists = true;
		//	DontDestroyOnLoad(transform.gameObject);
		//}
		//else
		//{
		//	Destroy(gameObject);//Destroy camera is one already exists
		//}

		//find the bounds if any and set it up. Camera won't follow outside of it.
		if (BoundBox == null)
		{
			BoundBox = FindObjectOfType<CameraBounds>().GetComponent<BoxCollider2D>();
			MinBounds = BoundBox.bounds.min;
			MaxBounds = BoundBox.bounds.max;

			TheCamera = GetComponent<Camera>();
			HalfHeight = TheCamera.orthographicSize;
			HalfWidth = HalfHeight * Screen.width / Screen.height;
		}

		BoundBox.isTrigger = true;

		//Find the player (if any) and focus on it. Should be updated for multiple players
		var player = GameObject.FindGameObjectWithTag("Player");
		if (player)
		{
			FollowTarget = player;
		}
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		//Camera does stuff in Vector3
		TargetPos = new Vector3(FollowTarget.transform.position.x, FollowTarget.transform.position.y, transform.position.z);
		transform.position = Vector3.Lerp(transform.position, TargetPos, MoveSpeed * Time.deltaTime);

		if (BoundBox != null)//Only do this if we have a boundbox pls
		{
			//Stay within the bounds camera!
			float clampedX = Mathf.Clamp(transform.position.x, MinBounds.x + HalfWidth, MaxBounds.x - HalfWidth);
			float clampedY = Mathf.Clamp(transform.position.y, MinBounds.y + HalfHeight, MaxBounds.y - HalfHeight);
			transform.position = new Vector3(clampedX, clampedY, transform.position.z);
		}
	}

	public void SetBounds(BoxCollider2D newBounds)
	{
		BoundBox = newBounds;

		MinBounds = BoundBox.bounds.min;
		MaxBounds = BoundBox.bounds.max;
	}
}
