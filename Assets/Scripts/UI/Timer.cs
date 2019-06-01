using System;
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
		public Movement playerGameObj; 

		private bool _stateTimer; // true -> work false-> is stop
		private bool _break;

		public bool Break
		{
			get => _break;
			set => _break = value;
		}

		public bool StateTimer
		{
			get => _stateTimer;
			set => _stateTimer = value;
		}

		private float _resume;

		public float Resume
		{
			get => _resume;
			set => _resume = value;
		}

		private float _speed;

		public float Speed
		{
			get => _speed;
			set => _speed = value;
		}


		[FormerlySerializedAs("_timerSet")] public float timerSet;
		// Use this for initialization
		void Start ()
		{
			_resume = 0f;
			_break = false;
			playerGameObj = GameObject.Find("Sphere").GetComponent<Movement>();
			_speed = 1f;
			timerSet = 60;
			_stateTimer = true;
			timer.text = timerSet.ToString();
		}
	
		// Update is called once per frame
		void Update ()
		{
			if (_break)
			{
				_stateTimer = false;
			}
			if (_resume>0f)
			{
				_resume -= Time.deltaTime;
			}
			else
			{
				if (!_break) _stateTimer = true ;
			}
				
			if (!NoTime() && GetTimer())
			{
				timerSet -= Time.deltaTime*_speed;
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
			return _stateTimer;
		}

		public void BreakTimer(){
			_break = true;
			playerGameObj.PlayerMove = false;
		
		}

		public void ResumeTimer(){
			_break = true;
			playerGameObj.PlayerMove = true;
		}
	}
}
