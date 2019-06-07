using UI;
using UnityEngine;

namespace Bonus
{
    public class Hourglass : MonoBehaviour
    {
        public void OnTriggerEnter(Collider collision)
        {
            //Inverse Time. If time = 20 become 40.
            if (collision.name != "Sphere") return;
            Timer timer = GameObject.Find("Canvas").GetComponent<Timer>();
            var atmTimer = 60 - int.Parse(timer.timer.text);
            timer.timerSet = atmTimer;
            timer.timer.text = atmTimer.ToString();
            Destroy(gameObject);

        }
    }
}
