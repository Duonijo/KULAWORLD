using UI;
using UnityEngine;

namespace Bonus
{
    public class Hourglass : MonoBehaviour
    {
        // Start is called before the first frame update
        public void OnTriggerEnter(Collider collision)
        {
            if (collision.name != "Sphere") return;
            Timer timer = GameObject.Find("Canvas").GetComponent<Timer>();
            var atmTimer = 60 - int.Parse(timer.timer.text);
            timer.timerSet = atmTimer;
            timer.timer.text = atmTimer.ToString();
            Destroy(gameObject);

        }
    }
}
