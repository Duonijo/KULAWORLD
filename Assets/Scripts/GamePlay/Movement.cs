using TMPro;
using UI;
using UnityEngine;

namespace GamePlay
{
    public class Movement : MonoBehaviour
    {
        private bool _move;
        private Vector3 _tarDir;
        private Vector3 _rollDir;
        private Vector3 _rotateDir;
        private bool _stuck;
        private bool _empty;
        private bool _mustTurn;
        private float _speed;
        private float _boost;
        private Timer _timer;


        public float Boost
        {
            get => _boost;
            set => _boost = value;
        }

        public float Speed
        {
            get => _speed;
            set => _speed = value;
        }

        public bool MustTurn
        {
            get => _mustTurn;
            set => _mustTurn = value;
        }


        public bool Move
        {
            get => _move;
            set => _move = value;
        }

        public bool Empty
        {
            get => _empty;
            set => _empty = value;
        }

        public bool PlayerMove
        {
            get => _move;
            set => _move = value;
        }

        public bool Stuck
        {
            get => _stuck;
            set => _stuck = value;
        }

        private Vector3 _endpoint;

        public Transform ballMesh;

        public new Camera camera;
      
        // Use this for initialization
        void Start()
        {
            _timer = GameObject.Find("Canvas").GetComponent<Timer>();
            _move = false;
            _mustTurn = false;
            _speed = 10f;
            _boost = 0f;

        }

        // Update is called once per frame
        void Update()
        {
            if (_boost > 0f)
            {
                
                _boost -= Time.deltaTime;
                Debug.Log("BOOST : " + _boost);
                Debug.Log("BOOST");
            }
            else
            {
                _speed = 10f;
                _timer.Speed = 1f;
                Debug.Log("NORMAL MODE");
            }
            MoveForward();
            PlayerRotation();
        }
        private void InputMove()
        {
            
            if (!_move && Input.GetKey(KeyCode.Z) &&!Input.GetKey(KeyCode.Q) && !Input.GetKey(KeyCode.D))
            {
                if (!_empty)
                {
                    if (!_stuck)
                    {
                        _move = true;
                        _rollDir = camera.transform.forward;
                        _endpoint = transform.position + transform.forward * 2;
                    }
                    else
                    {
                        _move = true;
                        _rollDir = camera.transform.forward;
                        _endpoint = transform.position + 0.25f*transform.forward + 0.25f*transform.up;
                        _mustTurn = true;
                    }
                }
                else if(_stuck)
                {
                    _move = true;
                    _rollDir = camera.transform.forward;
                    _endpoint = transform.position + 0.25f*transform.forward + 0.25f*transform.up;
                    _mustTurn = true;
                }

                
            }
        }
        private void MoveForward()
        {
            InputMove();

            if (_move)
            {
                ballMesh.transform.Rotate(300 * Time.deltaTime * new Vector3(_rollDir.z, 0, -_rollDir.x), Space.World);
                transform.position = Vector3.MoveTowards(transform.position, _endpoint, _speed * Time.deltaTime);
            }

            if (_endpoint.x == transform.position.x && _endpoint.z == transform.position.z)
            {
                _move = false;
                _mustTurn = false;
                _rollDir = Vector3.zero;
                Debug.Log("Reached");
            }
            
        }

        public void Turn()
        {            

            if (!_move && Input.GetKeyDown(KeyCode.D))
            {
                // turn left

                _rotateDir += Vector3.up*90f;
            }
            else if (!_move && Input.GetKeyDown(KeyCode.Q))
            {
                _rotateDir -= Vector3.up*90f;


            }
            
            if (!_move && Input.GetKeyDown(KeyCode.F))
            {
                _rotateDir -= Vector3.right*90f;

            }
            
        }

        private void PlayerRotation()
        {
            Turn();
            var rotation = transform.rotation;
            Quaternion tarRot = rotation;
            tarRot.eulerAngles = _rotateDir;
            rotation = Quaternion.Lerp(rotation, tarRot, 0.25f);
            transform.rotation = rotation;


        }
    }
}




