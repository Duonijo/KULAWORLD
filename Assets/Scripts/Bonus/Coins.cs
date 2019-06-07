using GamePlay;
using UI;
using UnityEngine;

namespace Bonus
{
    public class Coins : FloatingObj
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
            //Add Score when trigger coin and destroy the coin
            if (collision.name != "Sphere") return;
            _score.sharedScore += 100;
            _score.score.text = _score.sharedScore.ToString();
            Destroy(gameObject);
        }
    }
}
