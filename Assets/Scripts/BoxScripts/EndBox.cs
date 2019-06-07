using GamePlay;
using LevelEditor;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace BoxScripts
{
	public class EndBox : BoxScript {
		
		public Score score;
		private Save _save;
		private RequestedKeys _requestedKeys;
		
		void Start () {
			SetTag();
			score = GameObject.Find("Canvas").GetComponent<Score>(); // Find Score component on game
			_requestedKeys = GameObject.Find("Canvas").GetComponent<RequestedKeys>();
			//Find numbers of key needed to finish the level
		}
	
		public void OnTriggerEnter(Collider other)
		{
			if (_requestedKeys.KeysFound == _requestedKeys.KeysGot && other.name == "Sphere") // if we got all keys
			{
				score.finalScore += score.sharedScore;
				PlayerPrefs.SetInt("Score",0); //local score go to 0 for next level
				PlayerPrefs.SetInt("GlobalScore", score.finalScore);
				int actualScene = SceneManager.GetActiveScene().buildIndex + 1;
				int totalScene = SceneManager.sceneCountInBuildSettings;

				if (actualScene < totalScene)//if it s not last map
				{
					if (PlayerPrefs.GetInt("isBonus", 0) == 1)// if we were in bonus map
                    {
                    	PlayerPrefs.SetInt("BonusIndex", PlayerPrefs.GetInt("BonusIndex")+1);
                    	PlayerPrefs.SetInt("isBonus", 0);
                    	PlayerPrefs.SetInt("Fruits",0);
                    	SceneManager.LoadScene(PlayerPrefs.GetInt("NextLevel"));
                    }
                    else
                    {
                    	if (PlayerPrefs.GetInt("Fruits") == 5) // 5 fruits go to bonus map
                    	{
	                        PlayerPrefs.SetInt("isBonus", 1);
                    		PlayerPrefs.SetInt("NextLevel", SceneManager.GetActiveScene().buildIndex + 1); // save the normal index of next lvl without bonus
                    		var path = "Bonus_" + PlayerPrefs.GetInt("BonusIndex", 1);
                    		SceneManager.LoadScene(path);
                    	}
                    	else
                    	{
	                        if ((SceneManager.GetActiveScene().buildIndex + 4) % 5 == 0 &&
	                            SceneManager.GetActiveScene().buildIndex != 4){ // Every 5 levels can Save
		                        _save = BuildSave().GetComponent<Save>();
		                        _save.SaveState();
	                        }
	                        else
	                        {
		                        score.sharedScore = 0;
		                        score.score.text = score.sharedScore.ToString();
		                        score.globalScore.text = score.finalScore.ToString();
		                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	                        }
                    	}
                    }
				}
				else
				{
					gameObject.AddComponent<SaveScript>();
					gameObject.AddComponent<Win>();
					gameObject.GetComponent<EndBox>().enabled = false;
				}
			}
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
