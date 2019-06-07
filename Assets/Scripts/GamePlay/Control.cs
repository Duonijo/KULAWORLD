using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GamePlay
{
    public class Control : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                BackToMenu();
            }
        }

        // Start is called before the first frame update
        public void BackToMenu()
        {
            Debug.Log("BACK TO MENU");
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        }

        public void Play()
        {
            var playerMenu = GameObject.Find("Canvas/Player/NamePanel/InputField").gameObject;
            var input = playerMenu.GetComponentInChildren<InputField>();
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetString("Name", input.text != "" ? input.text : "GUEST");
            SceneManager.LoadScene(7);
        }
    }
}
