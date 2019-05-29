using TMPro;
using UnityEngine;
using static UnityEngine.PlayerPrefs;

namespace UI
{
	public class Score : MonoBehaviour {

		// Use this for initialization
		public Canvas layer;
		public TextMeshProUGUI score;
		public int sharedScore;
		void Start ()
		{
			if (GetInt("Score")!=0)
			{
				score.text = GetInt("Score").ToString();
			}
			else
			{
				score.text = "0";
				SetInt("Score", 0);
			}
			sharedScore = int.Parse(score.text);
			
		}
	
		// Update is called once per frame
		void Update () {
			
		}
	}
}
