using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    
    public class MainMenu : MonoBehaviour
    {
        // Use this for initialization
        public GameObject optionMenu;
        private Timer _timer;
        private Camera playerCamera;
        private Camera resumeCamera;


        public Timer Timer
        {
            get { return _timer; }
            set { _timer = value; }
        }

        public void Start()
        {
            Timer = GameObject.Find("Canvas").GetComponent<Timer>();
            try
            {
                playerCamera = GameObject.FindGameObjectWithTag("Camera").GetComponent<Camera>();
                resumeCamera = GameObject.FindGameObjectWithTag("SecondCamera").GetComponent<Camera>();
                resumeCamera.enabled = false;
                playerCamera.enabled = true;
            }
            catch (Exception e)
            {
                print("No camera");
            }
           
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                print("build 0");
                PlayerPrefs.DeleteAll();
            }
            else print("Build failed");
        }

        void Update()
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                Timer.stateTimer = !Timer.stateTimer;
                BreakTime();
               

            }
        }

        public void PlayGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        private void BreakTime()
        {
        
            var activeIt = !optionMenu.activeSelf;
            optionMenu.SetActive(activeIt);
            playerCamera.enabled = !playerCamera.enabled;
            resumeCamera.enabled = !resumeCamera.enabled;
            
        }
        public void QuitGame()
        {
            Application.Quit();
            print("QUIT");
        }

        public void LaunchEditor()
        {
            SceneManager.LoadScene("Editor", LoadSceneMode.Single);

        }
    }
}