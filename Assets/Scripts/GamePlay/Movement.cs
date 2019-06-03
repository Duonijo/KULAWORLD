using System;
using System.Collections;
using UI;
using UnityEngine;
using UnityEngine.UIElements;


namespace GamePlay
{
    public class Movement : MonoBehaviour
    {
        private bool _checkCol;
        private bool _move;
        private Vector3 _tarDir;
        private Vector3 _rollDir;
        private Vector3 _rotateDir;
        private bool _jump;
        private bool _gravity;
        private bool _fly;
        private bool _stuck;
        private bool _empty;
        private bool _goEmpty;
        private float _maxJump;
        private float _speed;
        private float _boost;
        private Timer _timer;
        private bool _obsTurn;
        private Vector3 _startJump;
        private Vector3 _endRotate;
        private bool _rotating;
        private Quaternion _curEuler;
        private bool mustTurn;

        private GameObject _left;
        private GameObject _right;
        private GameObject _face;
        private Quaternion _targetRotation;
        
        
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
        public bool PlayerMove
        {
            get => _move;
            set => _move = value;
        }
        private Vector3 _endpoint;
        public Transform ballMesh;

        public new Camera camera;
      
        // Use this for initialization
        void Start()
        {
            mustTurn = false;
            _targetRotation = transform.rotation;
            _rotating = false;
            _checkCol = true;
            _goEmpty = false;
            _obsTurn = false;
            _fly = false;
            _timer = GameObject.Find("Canvas").GetComponent<Timer>();
            _move = false;
            _jump = false;
            _gravity = false;
            _speed = 7f;
            _boost = 0f;
            _rollDir = new Vector3(0,0,0);

            _left = GameObject.Find("Sphere/Triggers/Left");
            _right = GameObject.Find("Sphere/Triggers/Right");
            _face = GameObject.Find("Sphere/Triggers/Face");
            
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
            if (!mustTurn && !_move && Input.GetKeyDown(KeyCode.D))
            {
                mustTurn = true;
                StartCoroutine(RotateUp(Vector3.up, 90f, 0.1f));
                
            }
            
            if (!mustTurn && !_move && Input.GetKeyDown(KeyCode.Q))
            {
                mustTurn = true;
                StartCoroutine(RotateUp(Vector3.up, -90f, 0.1f));
                
            }


            MoveForward();
            ColObstacle();
            Jump();
            Gravity();
            
        }
        private void InputMove()
        {
            
            if (!mustTurn && !_jump && !_move && Input.GetKey(KeyCode.Z) &&!Input.GetKey(KeyCode.Q) && !Input.GetKey(KeyCode.D) && !_obsTurn)
            {
                if (_face.GetComponent<SideCollision>().Collision || (!_face.GetComponent<SideCollision>().Collision && !_left.GetComponent<SideCollision>().Collision && !_right.GetComponent<SideCollision>().Collision))
                {
                    _move = true;
                    _rollDir = camera.transform.forward;
                    _endpoint = transform.position + transform.forward * 2;
                }

            }

            else if (!mustTurn && !_move && Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.Z) && !Input.GetKey(KeyCode.Q) &&
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

            if (_goEmpty)
            {
                transform.Rotate(Vector3.right*90f);
                _goEmpty = false;
                _endpoint = transform.position + transform.forward*1.5f;
                _move = true;
            }

            if (_endpoint == transform.position)
            {
                _move = false;
                _rollDir = Vector3.zero;
                _checkCol = true;
            }
        }
        private void ColObstacle()
        { 
            RaycastHit hit;
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward), Color.yellow);
            Debug.DrawRay(transform.position - 0.5f*transform.forward, transform.TransformDirection(Vector3.down), Color.blue);
            // Does the ray intersect any objects excluding the player layer
            if (_checkCol)
            {
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 0.5f))
                {
                    if (hit.collider.CompareTag("Ground"))
                    {
                        _move = false;
                        _obsTurn = true;
                        Debug.Log("Did Hit");
                    }
                }

                if (!Physics.Raycast(transform.position - 0.5f * transform.forward,
                        transform.TransformDirection(Vector3.down), out hit, 1f) && !(_jump || _gravity))
                {
                    _move = false;
                    _goEmpty = true;
                    _checkCol = false;
                }
            }
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
                angle = 5;
                transform.Rotate(Vector3.up*90f);
                _rollDir = Vector3.up*90f;

            }
            else if (!_move && Input.GetKeyDown(KeyCode.Q))
            {
                angle = -5;
                _rollDir -= Vector3.up*90f;
                transform.Rotate(-Vector3.up*90f);
                
            }
            return angle;

        }

        IEnumerator RotateUp(Vector3 axis, float angle, float duration)
        {
            Quaternion start = transform.rotation;
            Quaternion end = transform.rotation;
            
            end *= Quaternion.Euler(axis*angle);
            float elapsedTime = 0.0f;
            while (elapsedTime < duration && mustTurn)
            {
                transform.rotation = Quaternion.Slerp(start, end, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.rotation = end;
            mustTurn = false;
        }
    }
}




