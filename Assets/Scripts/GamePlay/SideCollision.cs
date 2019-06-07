using UnityEngine;

namespace GamePlay
{
    public class SideCollision : MonoBehaviour
    {
        private bool _collision;
        private Collider _collider;

        public Collider Collider
        {
            get => _collider;
            set => _collider = value;
        }

        public bool Collision
        {
            get => _collision;
            set => _collision = value;
        }

        // Start is called before the first frame update
        private void OnTriggerEnter(Collider other)
        {
            _collider = other;
            _collision = true;
        }

        private void OnTriggerStay(Collider other)
        {
            _collision = true;
        }

        private void OnTriggerExit(Collider other)
        {
            _collider = null;
            _collision = false;
        }
    }
}
