using UnityEngine;

namespace GamePlay
{
    public class DetectEmpty : MonoBehaviour
    {
        // Start is called before the first frame update
        private void OnTriggerEnter(Collider other)
        {
            var player = GameObject.Find("Sphere").GetComponent<Movement>();
            player.Empty = false;
        }
        
        private void OnTriggerStay(Collider other)
        {
            var player = GameObject.Find("Sphere").GetComponent<Movement>();
            player.Empty = false;
        }

        private void OnTriggerExit(Collider other)
        {
            var player = GameObject.Find("Sphere").GetComponent<Movement>();
            player.Empty = true;
            Debug.Log("Empty");    }
    }
}
