using TMPro;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BoxScripts
{
	public class EndBox : BoxScript {

		// Use this for initialization
		public TextMeshProUGUI score;
		public Save save;
		void Start () {
			SetTag();
			save = GetComponent<Save>();
			score = GameObject.Find("Canvas/ScoreTxt").GetComponent<TextMeshProUGUI>();
		}
	
		public override void OnCollisionEnter(Collision other)
		{
			print("end box");
			print(Keys.NumberKeys);
			if (Keys.NumberKeys == 0)
			{
				print("BUILD INDEX = " + SceneManager.GetActiveScene().buildIndex);
				if (SceneManager.GetActiveScene().buildIndex==2)
				{
					save.showSaveMenu();
				}
				else SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	        
			}
			else { print("Continue to play"); }
        

		}
	}
}
