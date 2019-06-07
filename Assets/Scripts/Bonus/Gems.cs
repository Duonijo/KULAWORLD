using GamePlay;
using UI;
using UnityEngine;

namespace Bonus
{
    public class Gems : FloatingObj
    {
        private Score _score;

        void Start()
        {
            _score = GameObject.Find("Canvas").GetComponent<Score>();
        }

        void Update()
        {
            StartCoroutine(RotateKey());
        }

        public override void OnTriggerEnter(Collider collision)
        {
            //Increase Score
            if (collision.name != "Sphere") return;
            _score.sharedScore += 500;
            _score.score.text = _score.sharedScore.ToString();
            Destroy(gameObject);
        }
    }
}
