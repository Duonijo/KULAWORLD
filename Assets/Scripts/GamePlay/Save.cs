using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Save : MonoBehaviour
{
    private Score _score;
    private int _level;
    public Canvas save;
    

    // Start is called before the first frame update
    void Start()
    {
        save.enabled = false;

        _score = GameObject.Find("Canvas").GetComponent<Score>();
        _level = SceneManager.GetActiveScene().buildIndex;
    }

    public void showSaveMenu()
    {
        save.enabled = true;
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
