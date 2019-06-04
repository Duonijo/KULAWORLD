using TMPro;
using UnityEngine;
using static UnityEngine.PlayerPrefs;

namespace UI
{
	public class Score : MonoBehaviour {

		// Use this for initialization
		public TextMeshProUGUI score;
		public int sharedScore;
		void Start ()
		{

			score.text = GetInt("Score",0).ToString();
			sharedScore = int.Parse(score.text);
			
		}
	
		// Update is called once per frame
		void Update () {
			
		}
	}
}
