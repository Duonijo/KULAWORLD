using GamePlay;
using UnityEngine;

namespace Trap
{
    public class Spikes : MonoBehaviour
    {
        GameObject _playerGameObj;
        GameObject _startingCube;

        // Start is called before the first frame update
        void Start()
        {
            _playerGameObj = GameObject.Find("Sphere");
            _startingCube = GameObject.Find("Start");
        }

        // Update is called once per frame
        void Update()
        {
    
        }

        private void OnCollisionEnter(Collision other)
        {
            if (_playerGameObj != null)
            {
                var player = GameObject.Find("Sphere");
                var life = player.GetComponent<Life>();
                life.LevelDeath(player);
                print("Done");
            }
            else
            {
                print("fail");
            }
        
        }
    }
}

