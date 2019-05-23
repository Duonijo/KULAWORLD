using GamePlay;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace UI
{
	public class Timer : MonoBehaviour
	{
		public TextMeshProUGUI timer;
		public PlayerMovement playerGameObj; 

		public bool stateTimer; // true -> work false-> is stop



		[FormerlySerializedAs("_timerSet")] public float timerSet;
		// Use this for initialization
		void Start ()
		{	
			timerSet = 60;
			stateTimer = true;
			timer.text = timerSet.ToString();
		}
	
		// Update is called once per frame
		void Update ()
		{
			if (!NoTime() && GetTimer())
			{
				timerSet -= Time.deltaTime;
				var timePass = Mathf.RoundToInt(timerSet);
				timer.text = timePass.ToString();
			}
		
		
		}

		bool NoTime()
		{
			if(int.Parse(timer.text) == 0)
			{
				print("YOU LOSE");
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			
				return true;
			}
			return false;
		}

		public bool GetTimer(){
			return stateTimer;
		}

		public void BreakTimer(){
			stateTimer = false;
			playerGameObj.canMove = false;
		
		}

		public void ResumeTimer(){
			stateTimer = true;
			playerGameObj.canMove = true;
		}
	}
}
