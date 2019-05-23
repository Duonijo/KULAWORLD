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
                PlayerMovement player = GameObject.Find("Sphere").GetComponent<PlayerMovement>();
                player.playerLife.LevelDeath(player);
                print("Done");
            }
            else
            {
                print("fail");
            }
        
        }
    }
}

