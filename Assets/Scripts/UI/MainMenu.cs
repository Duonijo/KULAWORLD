using System;
using GamePlay;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    
    public class MainMenu : MonoBehaviour
    {
        // Use this for initialization
        public GameObject optionMenu;
        [FormerlySerializedAs("Menu")] public GameObject menu;
        [FormerlySerializedAs("StartMenu")] public GameObject startMenu;
        
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
           
           /* if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                print("build 0");
                PlayerPrefs.DeleteAll();
            }
            else print("Build failed");*/
            startMenu.SetActive(false);

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
            menu.SetActive(false);
            startMenu.SetActive(true);
            var resume = GameObject.Find("Canvas/StartMenu/Resume");
            if (PlayerPrefs.GetInt("Level",0) == 0)
            {
                resume.SetActive(false);
            }
            else
            {
                resume.SetActive(true);
            }
        }

        public void newGame()
        {
            SceneManager.LoadScene("Level_1");
            PlayerPrefs.DeleteKey("Level");
            PlayerPrefs.DeleteKey("Score");
        }
        public void ResumeGame()
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("Level")+1);

        }

        public void LaunchCustom()
        {
            SceneManager.LoadScene("CustomMap");

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
            PlayerPrefs.DeleteAll();
            Application.Quit();
            print("QUIT");
        }

        public void LaunchEditor()
        {
            SceneManager.LoadScene("Editor", LoadSceneMode.Single);

        }

        public void BackToMenu()
        {
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        }
    }
}