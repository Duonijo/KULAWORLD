using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UI;
using UnityEngine.SceneManagement;



public class EndBox : Box {

    // Use this for initialization
    public TextMeshProUGUI score;
    public Save save;
    void Start () {
		SetTag();
		save = GetComponent<Save>();
    }
	
	public override void OnCollisionEnter(Collision other)
	{
		print("end box");
        print(Keys.NumberKeys);
        if (Keys.NumberKeys == 0)
        {
	        print("BUILD INDEX = " + SceneManager.GetActiveScene().buildIndex);
	        if (SceneManager.GetActiveScene().buildIndex==2)
	        {
		        save.showSaveMenu();
	        }
	        else save.Continue();
	        
        }
        else { print("Continue to play"); }
        

	}
}
