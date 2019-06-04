using System;
using BoxScripts;
using GamePlay;
using UnityEngine;
using UnityEngine.Serialization;

namespace Bonus
{
    public class Transporters : BoxScript
    {
        public GameObject link;
        public Transporters _tpLink;
        private GameObject _player;
        private bool _canTp;

        public void Start()
        {
            _canTp = true;
            _player = GameObject.Find("Sphere");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.name != "Sphere" || !_canTp) return;
            _player.GetComponent<Movement>().PlayerMove = false;
            _player.transform.position = link.transform.position + link.transform.up * 1.5f;
            _player.transform.rotation = link.transform.rotation;
            _tpLink._canTp = false;

        }

        private void OnTriggerExit(Collider other)
        {
            if (!_canTp) _canTp = true;
        }
    }
}