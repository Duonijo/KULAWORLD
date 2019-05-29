using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GamePlay
{
	public class Life : MonoBehaviour
	{

		private Vector3 _startPosition;
		private Score _score;

		// Use this for initialization
		void Start ()
		{
			_startPosition = transform.position;
			_score = GameObject.Find("Canvas").GetComponent<Score>();

		}
	
		// Update is called once per frame
		void Update () {
			
			if (transform.position.y < _startPosition.z - 100)
			{
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
				//Debug.Log("MORT");
			}
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
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

			}
			else
			{
				print("LOSE");
				PlayerPrefs.SetInt("Score", 0);
				SceneManager.LoadScene(1);
				//reload from 0
			}
		
		}
	}
}
