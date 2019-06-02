using System;
using GamePlay;
using UnityEngine;

namespace BoxScripts
{
	public class StartingBox : BoxScript {

		public Movement player;
		// Use this for initialization
		void Start ()
		{
			player = GameObject.Find("Sphere").GetComponent<Movement>();
			var transform1 = transform;
			try
			{
				player.transform.position = transform1.position + transform1.up*1.5f;
			}
			catch (Exception e)
			{
				Console.WriteLine("There is no player yet : " + e);
			}
		}
	
		// Update is called once per frame
		void Update () {
		
		}
	}
}
