using UnityEngine;

namespace GamePlay
{
    public class CameraMovement : MonoBehaviour
    {
        public GameObject player;

        // Use this for initialization
        private void Start()
        {
            TargetPlayer();
        }

        // Update is called once per frame

        public void TargetPlayer()
        {
            var point = player.transform.position; //get target's coords
            transform.LookAt(point); //makes the camera look to it                 
        }
    }
}