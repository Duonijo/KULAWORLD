using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UI;
using UnityEngine.SceneManagement;



public class EndBox : Box {

    // Use this for initialization
    public TextMeshProUGUI score;

    void Start () {
		SetTag();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void OnCollisionEnter(Collision other)
	{
		print("end box");
        print(Keys.NumberKeys);
        if (Keys.NumberKeys == 0)
        {
            print("nextLevel");
			if(SceneManager.GetActiveScene().buildIndex + 1 != null){
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
			}

        }
        else { print("Continue to play"); }
        
		//Verif du nombres de cle : 
		
		//Level a un nombre de clé précis 
		
		//Si joueur a nombre de cle du lvl
		
		//Afficher stat du joueur, w8, lancer prochain niveau 
		//Fct StartCoroutine
		//throw new System.NotImplementedException();
	}
}
