using System.Drawing;
using GamePlay;
using UnityEngine;
using Image = UnityEngine.UI.Image;

namespace UI
{
	public class Keys : FloatingObj
	{

		//public IPanel panel;
		private RequestedKeys _destroyKeys;

		private bool _isDetect;
		// Use this for initialization
		
		void Start ()
		{
			_isDetect = false;
			_destroyKeys = GameObject.Find("Canvas").GetComponent<RequestedKeys>();

		}

		private void Update()
		{
			if (_destroyKeys != null && !_isDetect)
			{
				_destroyKeys.KeysFound++;
				_isDetect = true;

			}
		}
	
		// Update is called once per frame

		public override  void  OnTriggerEnter(Collider other)
		{
			if (other.name != "Sphere") return;
			switch (name)
			{
				case "Key1":
					_destroyKeys.Key1.sprite = _destroyKeys.hereKey;
					break;
				case "Key2":
					_destroyKeys.Key2.sprite = _destroyKeys.hereKey;
					break;
				case "Key3":
					_destroyKeys.Key3.sprite = _destroyKeys.hereKey;
					break;
			}

			_destroyKeys.KeysGot++;
			Destroy(gameObject);
		}
		
	}
}
