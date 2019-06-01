using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class IceBox : MonoBehaviour
{
    // Start is called before the first frame update
    private Timer _timer;
    void Start()
    {
        _timer = GameObject.Find("Canvas").GetComponent<Timer>();
    }

    // Update is called once per frame

    private void OnTriggerEnter(Collider other)
    {
        _timer.StateTimer = false;
        _timer.Resume = 5f;    }
}
