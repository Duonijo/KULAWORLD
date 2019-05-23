using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Image = UnityEngine.UI.Image;

public class Game : MonoBehaviour {

    public GameObject[] keyArrays;
    public static int NbKeys;
    public Canvas layer;
     
    // Use this for initialization
    void Start () {
        NbKeys = keyArrays.Length;
        Image[] list = layer.GetComponentsInChildren<Image>();

        switch (keyArrays.Length)
        {
            case (1):
                list[1].enabled = false;
                list[2].enabled = false;
                break;
            case (2):
                list[2].enabled = false;
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update () {
		
	}

    

}
