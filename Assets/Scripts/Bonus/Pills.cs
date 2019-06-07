using GamePlay;
using UI;
using UnityEngine;

namespace Bonus
{
    public class Pills : MonoBehaviour
    {
        // Start is called before the first frame update
        private GameObject _player;
        private Timer _timer;

        private void Start()
        {
            _player = GameObject.Find("Sphere");
            _timer = GameObject.Find("Canvas").GetComponent<Timer>();
        }

        private void OnTriggerEnter(Collider other)
        {
            //Add speed to player
            if (other.name != "Sphere") return;
            if (name == "BluePills" | name == "BluePills(Clone)")
            {
                _player.GetComponent<Movement>().Speed += 20f;

            }
            else // Slow down the player and increase timer speed for 10s
            {
                _player.GetComponent<Movement>().Speed -= 5f;
                _timer.Speed = 3f;
                
            }
            _player.GetComponent<Movement>().Boost = 10f; // Timer for bonus
            Destroy(gameObject);
        }
    }
}
