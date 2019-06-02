using GamePlay;
using UnityEngine;
using Image = UnityEngine.UI.Image;

namespace UI
{
	public class Keys : FloatingObj
	{

		//public IPanel panel;

		public Sprite noKey;
		public Sprite hereKey;
		public Image spriteKey;
		public bool isTrigger;

		public static int  NumberKeys = 0;

		// Use this for initialization


		void Start () {
			isTrigger = false;
			NumberKeys++;
			var key1 = GameObject.Find("Canvas/Panel/Key1").GetComponent<Image>() as Image;
			var key2 = GameObject.Find("Canvas/Panel/Key2").GetComponent<Image>() as Image;
			var key3 = GameObject.Find("Canvas/Panel/Key3").GetComponent<Image>() as Image;
			switch (NumberKeys)
			{
				case 3:
					if (name == "Key1") spriteKey = key1;
					GameObject.Find("Canvas/Panel/Key2").SetActive(false);
					GameObject.Find("Canvas/Panel/Key3").SetActive(false);
					break;
				case 2:
					GameObject.Find("Canvas/Panel/Key3").SetActive(false);
					spriteKey = name == "Key1" ? key1 : key2;
					break;
				case 1:
					switch (name)
					{
						case "Key1":
							spriteKey = key1;
							break;
						case "Key2":
							spriteKey = key2;
							break;
						default:
							spriteKey = key3;
							break;
					}
					break;
			}
		}
	
		// Update is called once per frame
		void Update ()
		{
			StartCoroutine(base.RotateKey());
			if(isTrigger){
				isTrigger = false;
				StartCoroutine(AddScore(1000));
				Debug.Log("BEFORE : " + NumberKeys);

				NumberKeys--;
				Debug.Log("AFTER : " +NumberKeys);
				Destroy(gameObject);
				// numberKeys++;
			
			}
		}

		public override  void  OnTriggerEnter(Collider other)
		{
			spriteKey.sprite = hereKey;
			// numberKeys = numberKeys + 1;
			isTrigger = true;		
			//throw new System.NotImplementedException();
		}

	}
}
