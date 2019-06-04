using GamePlay;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace BoxScripts
{
	public class EndBox : BoxScript {

		// Use this for initialization
		public Score score;
		private Save _save;
		private RequestedKeys _requestedKeys;
		
		void Start () {
			SetTag();
			//_save = GameObject.Find("Save").GetComponent<Save>();
			score = GameObject.Find("Canvas").GetComponent<Score>();
			_requestedKeys = GameObject.Find("Canvas").GetComponent<RequestedKeys>();
		}
	
		public void OnTriggerEnter(Collider other1)
		{
			print("end box");
			if (_requestedKeys.KeysFound == _requestedKeys.KeysGot)
			{
				print("BUILD INDEX = " + SceneManager.GetActiveScene().buildIndex);
				PlayerPrefs.SetInt("Score", score.sharedScore);
				if ((SceneManager.GetActiveScene().buildIndex + 4) % 4 == 0 &&
				    SceneManager.GetActiveScene().buildIndex != 4){
					_save = BuildSave().GetComponent<Save>();
				}
				else
				{
					if (SceneManager.GetActiveScene().buildIndex + 1 <= SceneManager.sceneCountInBuildSettings)
					{
						SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
					}
					else
					{
						gameObject.AddComponent<Win>();
						gameObject.GetComponent<EndBox>().enabled = false;
					}
				}
	        
			}
			else { print("Continue to play"); }
		}

		private GameObject BuildSave()
		{
			var pathSave = "Map_Asset/PREFAB/Models/Save";
			var loadSave = Resources.Load(pathSave) as GameObject;
			var instance = Instantiate(loadSave);
			return instance;
		}
	}
}
