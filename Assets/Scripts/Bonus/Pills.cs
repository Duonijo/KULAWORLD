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
            if (name == "BluePills" | name == "BluePills(Clone)")
            {
                _player.GetComponent<Movement>().Speed += 20f;

            }
            else
            {
                _player.GetComponent<Movement>().Speed -= 5f;
                _timer.Speed = 3f;
                Debug.Log("sloooow");


            }

            _player.GetComponent<Movement>().Boost = 10f;
            Destroy(gameObject);
        
        }
    }
}
