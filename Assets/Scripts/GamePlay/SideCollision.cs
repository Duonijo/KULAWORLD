using UnityEngine;

namespace GamePlay
{
    public class SideCollision : MonoBehaviour
    {
        private bool _collision;

        public bool Collision
        {
            get => _collision;
            set => _collision = value;
        }

        // Start is called before the first frame update
        private void OnTriggerEnter(Collider other)
        {
            _collision = true;
        }

        private void OnTriggerStay(Collider other)
        {
            _collision = true;
        }

        private void OnTriggerExit(Collider other)
        {
            _collision = false;
        }
    }
}
