using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GamePlay
{
	public class Life : MonoBehaviour
	{

		private Vector3 _startPosition;
		private bool _death;

		public bool Death
		{
			get => _death;
			set => _death = value;
		}

		// Use this for initialization
		void Start ()
		{
			_startPosition = transform.position;
			_death = false;
		}
	
		// Update is called once per frame
		void Update () {
			if (transform.position.y < _startPosition.z - 100)
			{
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
				//Debug.Log("MORT");
			}
		}

		public void LevelDeath(PlayerMovement player)
		{

			Score score = GameObject.Find("Canvas").GetComponent<Score>();
			print("LEVEL DEATH");
			if (score.sharedScore-1000>=0)
			{
				var tamponScore = score.score.text;
				score.sharedScore = int.Parse(tamponScore);
				score.score.text = (score.sharedScore-1000).ToString();
				score.sharedScore = int.Parse(score.score.text);
				_death = true;
			}
			else
			{
				print("LOSE");
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			}
		
		}
	}
}
