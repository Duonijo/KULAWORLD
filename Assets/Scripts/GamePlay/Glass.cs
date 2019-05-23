using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using GamePlay;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class Glass : FloatingObj
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnTriggerEnter(Collider other)
    {
        print("TRIGGER GLASS");    
        var invisibleBox = GameObject.FindGameObjectsWithTag("Invisible");
        foreach (var box in invisibleBox)
        {
            print(box.name);
            box.GetComponent<Renderer>().enabled = true;
            box.GetComponent<Collider>().enabled = true;
            box.tag = "Ground";
        }
        Destroy(gameObject);
    }
    
}
