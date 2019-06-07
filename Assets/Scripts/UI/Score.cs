using TMPro;
using UnityEngine;
using static UnityEngine.PlayerPrefs;

namespace UI
{
	public class Score : MonoBehaviour {

		// Use this for initialization
		public TextMeshProUGUI score;
		public TextMeshProUGUI globalScore;

		public int sharedScore; //local score
		public int finalScore; //final score
		void Start ()
		{
			globalScore = GameObject.Find("Canvas/Score/GlobalScore").GetComponent<TextMeshProUGUI>();
			finalScore = GetInt("SaveGlobalScore") != 0 ? GetInt("SaveGlobalScore") : GetInt("GlobalScore",0);
			globalScore.text = finalScore.ToString();
			score.text = GetInt("Score",0).ToString();
			sharedScore = int.Parse(score.text);
			
		}
		
	}
}
