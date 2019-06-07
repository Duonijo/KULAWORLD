using CustomMap;
using GamePlay;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Trap
{
    public class Laser : MonoBehaviour
    {
        private LineRenderer _lr;

        //public Vector3 position;

        private Vector3 _direction;

        public Vector3 Direction
        {
            get => _direction;
            set => _direction = value;
        }

        // Use this for initialization
        void Start () {
            _lr = GetComponent<LineRenderer>();
            //transform.position = position;
            var dir = gameObject.GetComponent<GameData>();
            _direction = new Vector3(dir.endX, dir.endY, dir.endZ);
            _lr.SetPosition(0, transform.position);

        }
	
        // Update is called once per frame
        void Update () {
            RaycastHit hit;
            if (_direction == new Vector3(0, 0, 0))
            {
                _direction = -transform.forward;
            }
            if (Physics.Raycast(transform.position, _direction, out hit))
            {
                if (hit.collider)
                {
                    _lr.SetPosition(1, hit.point);
                    
                    var player = GameObject.Find("Sphere");
                    var life = player.GetComponent<Life>();
                    if (hit.collider.gameObject.name != "Sphere") return;
                    life.LevelDeath(player);
                    print("Done");
                }
            }
            else _lr.SetPosition(1, _direction);

        }
    }
}
