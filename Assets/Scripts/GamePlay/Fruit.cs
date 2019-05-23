using System.Collections;
using System.Collections.Generic;
using GamePlay;
using UnityEngine;

public class Fruit : FloatingObj {
    bool _isCollide = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //print(isCollide);
        if (_isCollide)
        {
            StartCoroutine(AddScore(2500));
            Destroy(gameObject);
            _isCollide = false;      
        }
    }

    public override void OnTriggerEnter(Collider other)
    {   
        _isCollide = true;
    }
    

}
