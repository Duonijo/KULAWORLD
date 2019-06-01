using UnityEngine;

namespace BoxScripts
{
    public class InvisibleBox : BoxScript
    {
        public float timeLeft;
        // Start is called before the first frame update
        void Start()
        {
            timeLeft = 30f;
            SetTag();
        }

        void Update()
        {
            if (!CompareTag("Invisible"))
            {
                if (timeLeft >= 0)
                {
                    timeLeft -= Time.deltaTime;
                }
                else
                {
                    GetComponent<Renderer>().enabled = false;
                    GetComponent<Collider>().enabled = false;
                }
            }
        }
        // Update is called once per frame
        public override void SetTag()
        {
            if (CompareTag("Ground") || CompareTag("ActualBox"))
            {
                tag = "Ground";
            }
            else
            {
                tag = "Invisible";
            }
        }
        private void OnCollisionExit(Collision other) {
            this.SetTag();
        }
    }
}
