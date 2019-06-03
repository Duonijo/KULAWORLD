using System;
using System.Collections;
using UI;
using UnityEngine;

namespace GamePlay
{
    public class FloatingObj : MonoBehaviour
    {
        public virtual void OnTriggerEnter(Collider collision)
        {
            Destroy(gameObject);
        }
        
        // Update is called once per frame
        private void Update()
        {
            StartCoroutine(RotateKey());
            //test
        }

        public IEnumerator RotateKey()
        {
            transform.Rotate(transform.up);
            yield return new WaitForSeconds(0.001f);
        }

        public IEnumerator AddScore(int point)
        {
            Score score = GameObject.Find("Canvas").GetComponent<Score>();

            for (float f = 0; f < 5f; f += 1f)
            {
                var result = int.Parse(score.score.text) + point;
                print(result);
                score.sharedScore = result;
                score.score.text = result.ToString();
                yield return new WaitForSeconds(0.1f);
            }
            //Destroy(gameObject);

        }

   
    }
}