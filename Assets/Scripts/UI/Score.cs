using TMPro;
using UnityEngine;

namespace UI
{
	public class Score : MonoBehaviour {

		// Use this for initialization
		public Canvas layer;
		public TextMeshProUGUI score;
		public int sharedScore;
		void Start ()
		{
			score.text = "0";
			sharedScore = int.Parse(score.text);
		}
	
		// Update is called once per frame
		void Update () {
		}
	}
}
