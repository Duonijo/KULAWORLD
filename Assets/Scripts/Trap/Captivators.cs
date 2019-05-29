using System;
using GamePlay;
using UnityEngine;

namespace Trap
{
    public class Captivators : MonoBehaviour
    {
        public GameObject startingPoint;
        public Vector3 startingPointV;
        public Vector3 endPointV;
        public GameObject endPoint;

        private bool _turn;
        public Vector3 direction;

        // Start is called before the first frame update
        void Start()
        {
            direction = Vector3.up * 2f;
            var position = startingPoint.transform.position;
            position += Vector3.up * 2f;
            startingPointV = position;
            endPointV = endPoint.transform.position + Vector3.up * 2f;
            _turn = true;
            transform.position = position ;
        } 
    
        // Update is called once per frame
        void Update()
        {
            var step = 5 * Time.deltaTime; // calculate distance to move

            if (Vector3.Distance(transform.position, endPointV) > 0.001f)
            {
                transform.position = Vector3.MoveTowards(transform.position,
                    endPointV, step);
            }
            else
            {
                print("DIRECTION : " + direction);
                var tampon = endPointV;
                endPointV = startingPointV;
                startingPointV = tampon;
            }

        }

        private void OnTriggerEnter(Collider other)
        {
            try
            {    
                var player = GameObject.Find("Sphere");
                var life = player.GetComponent<Life>();
                life.LevelDeath(player);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new NotImplementedException();
            }
        }
    }
}
