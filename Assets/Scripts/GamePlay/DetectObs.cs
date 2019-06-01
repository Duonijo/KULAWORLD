using UnityEngine;

namespace GamePlay
{
    public class DetectObs : MonoBehaviour
    {
        // Start is called before the first frame update
        private void OnTriggerEnter(Collider other)
        {
            var player = GameObject.Find("Sphere").GetComponent<Movement>();
            player.Stuck = true;
            Debug.Log("OBSTACLE");
        }
        
        

        private void OnTriggerExit(Collider other)
        {
            var player = GameObject.Find("Sphere").GetComponent<Movement>();
            player.Stuck = false;
            player.MustTurn = false;

        }
    }
}
