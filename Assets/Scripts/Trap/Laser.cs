using GamePlay;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Trap
{
    public class Laser : MonoBehaviour
    {
        public LineRenderer _lr;

        public Vector3 position;
        // Use this for initialization
        void Start () {
            _lr = GetComponent<LineRenderer>();
            transform.position = position;
        }
	
        // Update is called once per frame
        void Update () {
            _lr.SetPosition(0, transform.position);
            if (Physics.Raycast(transform.position, transform.forward, out var hit))
            {
                if (hit.collider)
                {
                    _lr.SetPosition(1, hit.point);
                    var player = GameObject.Find("Sphere").GetComponent<PlayerMovement>();
                   if (hit.collider.gameObject.name == "Sphere")
                   {
                       player.playerLife.LevelDeath(player);
                       print("Done");
                   }
                   //Collision with laser
                }
            }
            else _lr.SetPosition(1, new Vector3(0,1.5f,-5000));

        }
    }
}
