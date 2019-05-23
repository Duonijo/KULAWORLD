using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UI;
using UnityEngine;

public class Hourglass : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
 
        Timer timer = GameObject.Find("Canvas").GetComponent<Timer>();
        var atmTimer = 60 - int.Parse(timer.timer.text);
        print(atmTimer);
        timer.timerSet = atmTimer;
        timer.timer.text = atmTimer.ToString();
        Destroy(gameObject);

    }
}
