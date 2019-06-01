using BoxScripts;
using UnityEngine;

namespace Trap
{
    public class FlimsyBox : BoxScript
    {
        // Start is called before the first frame update
        private bool _destroyBox;
        public float timeLeft;


        public bool DestroyBox
        {
            get { return _destroyBox; }
            set { _destroyBox = value; }
        }

        public void Start()
        {
            DestroyBox = false;
            timeLeft = 3f;
        }
        public void Update()
        {
            if (!DestroyBox) return;
            if (timeLeft >= 0)
            {
                timeLeft -= Time.deltaTime;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        private void OnCollisionExit(Collision other) {
            this.SetTag();
        }
        private void OnCollisionEnter(Collision other)
        {
            collideObject = other.gameObject;
            this.tag = "ActualBox";
            // print("Sphere position : " + collideObject.transform.position);
            //throw new System.NotImplementedException();
            DestroyBox = true;

        }

    }
}

