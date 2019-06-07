using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GamePlay
{
	public class Life : MonoBehaviour
	{

		private Score _score;

		private int _startFruit;
			// Use this for initialization
		void Start ()
		{
			_startFruit = PlayerPrefs.GetInt("Fruits");
			_score = GameObject.Find("Canvas").GetComponent<Score>();

		}
		
		public void LevelDeath(GameObject player)
		{

			print("LEVEL DEATH");
			if (_score.sharedScore-1000>=0)
			{
				var tamponScore = _score.score.text;
				_score.sharedScore = int.Parse(tamponScore);
				_score.score.text = (_score.sharedScore-1000).ToString();
				_score.sharedScore = int.Parse(_score.score.text);
				PlayerPrefs.SetInt("Score", _score.sharedScore);
				Scene scene = SceneManager.GetActiveScene();
				if(_startFruit != PlayerPrefs.GetInt("Fruits")) PlayerPrefs.SetInt("Fruits", _startFruit);
				SceneManager.LoadScene(scene.name);

			}
			else
			{
				print("LOSE");
				_score.sharedScore = 0;
				_score.score.text = _score.sharedScore.ToString();
				PlayerPrefs.SetInt("Score", 0);
				PlayerPrefs.SetInt("GlobalScore",0);
				PlayerPrefs.SetInt("Fruits", 0);
				SceneManager.LoadScene(7);
				//reload from 0
			}
		
		}
	}
}
