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
        
        private bool _jump;
        private bool _forwardJump;
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
        private bool _complexJump;
        private Vector3 _endRotate;
        private bool _rotating;
        private Quaternion _curEuler;
        private bool _mustTurn;
        private Vector3 _tarDir;
        private Vector3 _rotateDir;
        private GameObject _left;
        private GameObject _right;
        private GameObject _face;
        
        
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

      
        // Use this for initialization
        void Start()
        {
            _forwardJump = false;
            _mustTurn = false;
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
            
            if (_obsTurn)
            {
                StartCoroutine(RotateUp(Vector3.right, -90, 0.2f));
                
            }
            if (_goEmpty)
            {
                _rotating = true;
                StartCoroutine(RotateUp(Vector3.right, 90, 0.2f));
                
            }
            
            if (Input.GetKey(KeyCode.D) &&!_mustTurn && !_move )
            {
                _mustTurn = true;
                StartCoroutine(RotateUp(Vector3.up, 90f, 0.2f));
                
            }
            
            if (Input.GetKey(KeyCode.Q) &&!_mustTurn && !_move)
            {
                _mustTurn = true;
                StartCoroutine(RotateUp(Vector3.up, -90f, 0.2f));
                
            }

            if (!_gravity && !Input.GetKey(KeyCode.Z) && Input.GetKey(KeyCode.Space) && !_jump && !_move)
            {
                _jump = true;
                _move = false;
                _endpoint = transform.position + transform.up*1.5f;
            }
            
            if (!_gravity && Input.GetKey(KeyCode.Z) && Input.GetKey(KeyCode.Space) && !_jump && !_move)
            {
                _jump = true;
                _forwardJump = true;
                _move = false;
                _endpoint = transform.position + transform.forward * 2 + transform.up*1.5f;
            }

            

            MoveForward();
            ColObstacle();
            Jump();
            Gravity();
            
        }
        private void InputMove()
        {
            
            if (!_mustTurn && !_gravity && !_jump && !_move && Input.GetKey(KeyCode.Z) &&!Input.GetKey(KeyCode.Q) && !Input.GetKey(KeyCode.D) && !_obsTurn)
            {
                if (_face.GetComponent<SideCollision>().Collision || (!_face.GetComponent<SideCollision>().Collision && !_left.GetComponent<SideCollision>().Collision && !_right.GetComponent<SideCollision>().Collision))
                {
                    _move = true;
                    _endpoint = transform.position + transform.forward * 2;
                }

            }
            
        }
        private void MoveForward()
        {
            InputMove();
            if (_move)
            {
                ballMesh.transform.Rotate(300 * Time.deltaTime * transform.right, Space.World);
                transform.position = Vector3.MoveTowards(transform.position, _endpoint, _speed * Time.deltaTime);
                if (_endpoint == transform.position)
                {
                    _move = false;
                    _checkCol = true;
                }
            }
            
        }
        private void ColObstacle()
        { 
            RaycastHit hit;
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward), Color.yellow);
            Debug.DrawRay(transform.position - 0.5f*transform.forward, transform.TransformDirection(Vector3.down), Color.blue);
            if (_checkCol)
            {
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 0.5f))
                {
                    if (hit.collider.CompareTag("Ground"))
                    {
                        _move = false;
                        _obsTurn = true;
                        _mustTurn = true;
                        Debug.Log("Did Hit");
                    }
                }
                if (!Physics.Raycast(transform.position - 0.5f * transform.forward,
                        transform.TransformDirection(Vector3.down), out hit, 1f) && !(_jump || _gravity))
                {
                    _move = false;
                    _goEmpty = true;
                    _checkCol = false;
                    _mustTurn = true;
                }
            }
        }
        private void Gravity()
        {
            if (_gravity)
            {
                ballMesh.transform.Rotate(300 * Time.deltaTime * transform.right, Space.World);
                transform.position = Vector3.MoveTowards(transform.position, _endpoint, _speed * Time.deltaTime);
                if (transform.position == _endpoint)
                {
                    _endpoint = transform.position - transform.up * 2;
                    _forwardJump = false;
                }
            }

            
            
        }
        private void Jump()
        {
            if (_jump)
            {
                ballMesh.transform.Rotate(300 * Time.deltaTime * transform.right, Space.World);
                transform.position = Vector3.MoveTowards(transform.position, _endpoint, _speed * Time.deltaTime);
                if (transform.position == _endpoint)
                {
                    _jump = false;
                    _gravity = true;
                    if (_forwardJump)
                    {
                        _endpoint = transform.position + transform.forward * 2 - transform.up*1.5f;
                    }
                }
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            _gravity = false;
        }

        IEnumerator RotateUp(Vector3 axis, float angle, float duration)
        {
            _move = false;
            Quaternion start = transform.rotation;
            Quaternion end = transform.rotation;
            
            end *= Quaternion.Euler(axis*angle);
            float elapsedTime = 0.0f;
            while (elapsedTime < duration && _mustTurn)
            {
                transform.rotation = Quaternion.Slerp(start, end, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.rotation = end;
            _mustTurn = false;
            if (_goEmpty)
            {
                _goEmpty = false;
                _endpoint = transform.position + transform.forward*1.5f;
                _move = true;
            }

            if (_obsTurn)
            {
                _obsTurn = false;
                _endpoint = transform.position + transform.forward * 0.50f;
                _move = true;
            }
        }
    }
}




