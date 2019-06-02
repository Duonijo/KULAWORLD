using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay
{
    public class Movement : MonoBehaviour
    {
        private bool _move;
        private Vector3 _tarDir;
        private Vector3 _rollDir;
        private Vector3 _rotateDir;
        private bool _jump;
        private bool _gravity;
        private bool _fly;
        private bool _stuck;
        private bool _empty;
        private bool _mustTurn;
        private float _maxJump;
        private float _speed;
        private float _boost;
        private Timer _timer;
        private bool _obsTurn;
        private Vector3 _startJump;
        private Vector3 _endRotate;
        


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
            _obsTurn = false;
            _fly = false;
            _timer = GameObject.Find("Canvas").GetComponent<Timer>();
            _move = false;
            _mustTurn = false;
            _jump = false;
            _gravity = false;
            _speed = 10f;
            _boost = 0f;
            _rollDir = new Vector3(0,0,0);

        }

        // Update is called once per frame
        void Update()
        {
            if (_boost > 0f)
            {
                _boost -= Time.deltaTime;
            }
            else
            {
                _speed = 10f;
                _timer.Speed = 1f;
                Debug.Log("NORMAL MODE");
            }
            Turn();
            MoveForward();
            //PlayerRotation();
            ColObstacle();
            Jump();
            Gravity();
            
        }
        private void InputMove()
        {
            
            if (!_move && Input.GetKey(KeyCode.Z) &&!Input.GetKey(KeyCode.Q) && !Input.GetKey(KeyCode.D) && !_obsTurn)
            {
                if (!_empty)
                {
                    if (!_stuck)
                    {
                        _move = true;
                        _rollDir = camera.transform.forward;
                        _endpoint = transform.position + transform.forward * 2;
                    }
                }
                else
                {
                    _move = true;
                    _rollDir = camera.transform.forward;
                    _endpoint = transform.position + transform.forward;
                }
            }

            else if (!_move && Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.Z) && !Input.GetKey(KeyCode.Q) &&
                !Input.GetKey(KeyCode.D) && !_fly)
            {
                _fly = true;
                _rollDir = transform.up;
                _startJump = transform.position;
                _endpoint = transform.position + transform.up*2;
                _jump = true;
            }

            
        }
        private void MoveForward()
        {
            InputMove();
            if (_move)
            {
                ballMesh.transform.Rotate(300 * Time.deltaTime * transform.right, Space.World);
                transform.position = Vector3.MoveTowards(transform.position, _endpoint, _speed * Time.deltaTime);
            }

            if (_obsTurn)
            {
                //_rotateDir -= Vector3.right*90f;
                transform.Rotate(-Vector3.right*90f);
                _obsTurn = false;
                _endpoint = transform.position + transform.forward * 0.50f;
                _move = true;
            }

            if (_empty && !_obsTurn)
            {
                transform.Rotate(Vector3.right*90f);
                _endpoint = transform.position + transform.forward * 0.50f;
                _move = true;
            }
            else if (_endpoint == transform.position)
            {
                _move = false;
                _rollDir = Vector3.zero;
                Debug.Log("Reached");
            }
            
        }

        private void ColObstacle()
        { 
            RaycastHit hit;
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward), Color.yellow);
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 0.5f))
            {
                //_move = false;
                //_stuck = true;
                //_endpoint = transform.position + transform.up * 0.50f;*
                _move = false;
                _obsTurn = true;
                Debug.Log("Did Hit");
            }
        }

        private void MoveToEmpty()
        {
            
        }
        private void Gravity()
        {
            if (_gravity)
            {
                transform.position = Vector3.MoveTowards(transform.position, _endpoint, _speed * Time.deltaTime);
                if (transform.position == _startJump)
                {
                    _gravity = false;
                    _fly = false;
                    _rollDir = Vector3.zero;
                    Debug.Log("Reached");
                }
            }
            
           
        }

        private void Jump()
        {
            InputMove();
            if (_jump && !_gravity)
            {
                transform.position = Vector3.MoveTowards(transform.position, _endpoint, _speed * Time.deltaTime);
                if (_endpoint.y == transform.position.y)
                {
                    _rollDir = Vector3.zero;
                    _jump = false;
                    if (!_gravity)
                    {
                        _gravity = true;
                        _endpoint = transform.position - transform.up * 2;

                    }
                    Debug.Log("Reached");
                }
            }
            
            
        }
        private float Turn()
        {

            var angle = 0;
            if (!_move && Input.GetKeyDown(KeyCode.D))
            {
                // turn left

                angle = 5;
                transform.Rotate(Vector3.up*90f);
                _mustTurn = true;
                _rollDir = Vector3.up*90f;

            }
            else if (!_move && Input.GetKeyDown(KeyCode.Q))
            {
                angle = -5;
                _rollDir -= Vector3.up*90f;
                transform.Rotate(-Vector3.up*90f);
                _mustTurn = true;




            }
            
            if (!_move && Input.GetKeyDown(KeyCode.F))
            {
                _rotateDir -= Vector3.right*45;

            }

            return angle;

        }

        private void PlayerRotation(float angle)
        {

        }
    }
}




