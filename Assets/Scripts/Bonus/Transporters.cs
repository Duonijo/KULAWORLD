using BoxScripts;
using GamePlay;
using UnityEngine;
using UnityEngine.Serialization;

namespace Bonus
{
    public class Transporters : BoxScript
    {
        public GameObject link;
        [FormerlySerializedAs("_tpLink")] public Transporters tpLink;
        private GameObject _player;
        private bool _canTp;

        public void Start()
        {
            _canTp = true;
            _player = GameObject.Find("Sphere");
        }

        private void OnTriggerEnter(Collider other)
        {
            //Teleport player to tp link
            if (other.name != "Sphere" || !_canTp) return;
            _player.GetComponent<Movement>().PlayerMove = false;
            _player.transform.position = link.transform.position + link.transform.up * 1.5f;
            _player.transform.rotation = link.transform.rotation;
            tpLink._canTp = false; //Block tp when you pop on the other one, else you will tp in loop
        }
        private void OnTriggerExit(Collider other)
        {
            if (!_canTp) _canTp = true;
        }
    }
}