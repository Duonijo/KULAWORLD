using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GamePlay
{
    public class Save : MonoBehaviour
    {
        private Score _score;
        private int _level;
        
        // Start is called before the first frame update
        void Start()
        {

            _score = GameObject.Find("Canvas").GetComponent<Score>();
            _level = SceneManager.GetActiveScene().buildIndex;
        }

        public void SaveState()
        {
            PlayerPrefs.SetInt("Score", _score.sharedScore);
            PlayerPrefs.SetInt("Level", _level);
            Continue();
        }

        public void Continue()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
