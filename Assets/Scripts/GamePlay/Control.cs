using System;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    }
}
