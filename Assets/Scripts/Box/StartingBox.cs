using System.Collections;
using System.Collections.Generic;
using GamePlay;
using UnityEngine;

public class StartingBox : Box {

    public PlayerMovement player;
	// Use this for initialization
	void Start ()
	{
		player = GameObject.Find("Sphere").GetComponent<PlayerMovement>();
		var transform1 = transform;
		player.transform.position = transform1.position + transform1.up*1.5f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
