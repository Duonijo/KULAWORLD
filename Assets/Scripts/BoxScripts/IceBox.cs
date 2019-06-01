using System;
using UI;
using UnityEngine;

namespace BoxScripts
{
    public class IceBox : MonoBehaviour
    {
        // Start is called before the first frame update
        private Timer _timer;
        void Start()
        {
            _timer = GameObject.Find("Canvas").GetComponent<Timer>();
        }

        private void OnCollisionEnter(Collision other)
        {
            Freeze();
        }

        private void OnCollisionStay(Collision other)
        {
            Freeze();
        }

        private void OnCollisionExit(Collision other)
        {
            Resume();        
        }

        void Freeze()
        {
            _timer.StateTimer = false;

        }

        void Resume()
        {
            _timer.StateTimer = true;

        }
    }
}
