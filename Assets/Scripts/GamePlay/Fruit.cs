using System;
using System.Collections;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

namespace GamePlay
{
	public class Fruit : FloatingObj {
		bool _isCollide;
		private bool _instant;
		private GameObject _fruit;
		private GameObject _bonus;
		private string _fruitName;

		private string _path;
		// Use this for initialization
		public void Start ()
		{
			if(PlayerPrefs.GetInt("Fruits",0) == 5)
			{
				PlayerPrefs.SetInt("Fruits",0);
			}
			_bonus = GameObject.Find("Bonus");
			_isCollide = false;
			try
			{
				var basicFruit = GameObject.Find("Fruit");
				if (basicFruit != null)
				{
					_instant = true;

				}
			}
			catch (Exception e)
			{
				_instant = false;
				Console.WriteLine(e);
				throw;
			}

		}
		
	
		// Update is called once per frame
		public void Update () {
			//print(isCollide);
			if (_isCollide)
			{
				StartCoroutine(AddScore(2500));
				Destroy(gameObject);
				_isCollide = false;      
			}
			
			if (_instant)
			{
				switch (PlayerPrefs.GetInt("Fruits",0))
				{
					case 0:
						_path = "Map_Asset/PREFAB/Models/Apple";
						_fruitName = "Apple";
						break;
					case 1:
						_path = "Map_Asset/PREFAB/Models/WaterMelon";
						_fruitName = "WaterMelon";

						break;
					case 2:
						_path = "Map_Asset/PREFAB/Models/Tomato";
						_fruitName = "Tomato";

						break;
					case 3:
						_path = "Map_Asset/PREFAB/Models/Banana";
						_fruitName = "Banana";

						break;
					case 4:
						_path = "Map_Asset/PREFAB/Models/Strawberry";
						_fruitName = "Strawberry";

						break;
				}
				var loadPrefab = Resources.Load(_path);
				var _prefab = loadPrefab as GameObject;
				_fruit = Instantiate(_prefab);
				_fruit.name = _fruitName;
				_fruit.transform.position = gameObject.transform.position;
				_fruit.transform.rotation = gameObject.transform.rotation;
				
				_fruit.transform.SetParent(_bonus.transform);
				Destroy(gameObject);
			}
		}

		public override void OnTriggerEnter(Collider other)
		{   
			_isCollide = true;
			PlayerPrefs.SetInt("Fruits",PlayerPrefs.GetInt("Fruits",0)+1);
			
		}
		
	}
}
