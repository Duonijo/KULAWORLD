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
