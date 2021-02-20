using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBounds : MonoBehaviour
{
	private BoxCollider2D CameraBound;
	private CameraManager CameraManager;

	// Use this for initialization
	void Start()
	{
		CameraBound = GetComponent<BoxCollider2D>();
		CameraManager = FindObjectOfType<CameraManager>();
		CameraManager.SetBounds(CameraBound);
	}
}
