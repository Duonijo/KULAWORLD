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
            playerCamera = GameObject.Find("Sphere/MainCamera").GetComponent<Camera>();
            resumeCamera = GameObject.Find("ResumeCamera").GetComponent<Camera>();
            resumeCamera.enabled = false;
            playerCamera.enabled = true;
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
            playerCamera.enabled = false;
            resumeCamera.enabled = true;



        }
        public void QuitGame()
        {
            Application.Quit();
            print("QUIT");
        }
    }
}