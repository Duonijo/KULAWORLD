using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace GamePlay
{
    public class PlayerMovement : MonoBehaviour
    {
        public enum Axis
        {
            Y,
            Z
        }

        public Axis axisGame;
        private bool _isEmpty;
        private bool _isObstacle;
        private bool _onGround;
        private Rigidbody _rb;

        private bool _isMoving { get; set; }
        public bool canMove;
        private bool _isRotating { get; set; }


        // Use this for initialization
        private void Start()
        {
            axisGame = Axis.Z;
            _isObstacle = false;
            _isEmpty = false;
            _onGround = true;
            
            canMove = true;
            _isMoving = false;
            _isRotating = false;


            SetGravity(axisGame);
            _rb = GetComponent<Rigidbody>();
            _rb.useGravity = false;

        }

        // Update is called once per frame
        private void Update()
        {
            //_isEmpty = CheckNextMove();
            if (_onGround) _rb.useGravity = false;
            if (canMove)
            {
                if ((Input.GetKey("z") | Input.GetKey("w")) && !_isMoving && !_isRotating)
                {
                    _isMoving = true;
                    _isEmpty = CheckNextMove();
                    if (_isObstacle)
                    {
                        StartCoroutine(MoveToObstacle(axisGame));
                        _isObstacle = false;
                        axisGame = ReturnAxis(axisGame);
                    }
                    else if (_isEmpty)
                    {
                        StartCoroutine(MoveToEmpty(axisGame));
                        axisGame = ReturnAxis(axisGame);
                    }
                    else
                    {
                        StartCoroutine(MoveTo());
                    }
                }

                if (Input.GetButtonDown("Jump"))
                {
                    _rb.useGravity = true;
                    JumpMove(axisGame);
                }

                if ((Input.GetKey("q") | Input.GetKey("a")) && !_isMoving && !_isRotating)
                {
                    _isRotating = true;
                    StartCoroutine(RotateSphere(-5, axisGame));
                }
                else if (Input.GetKey("d") && !_isMoving && !_isRotating)
                {
                    _isRotating = true;
                    StartCoroutine(RotateSphere(5, axisGame));
                }
            }
            

            SetGravity(axisGame);
        }

        private IEnumerator RotateSphere(int angle, Axis axisGame)//Rotation of sphere when next position is empty or a wall
        {
            for (float f = 0; f < 18; f += 1f)
            {
                if ((axisGame == Axis.Z) | (axisGame == Axis.Y)) RotateOnHimself(Vector3.up, angle);
                yield return new WaitForSeconds(0.001f);
            }

            yield return new WaitForSeconds(0.05f);
            _isRotating = false;
        }


        private IEnumerator MoveTo() //Move to next Box
        {
            for (float f = 0; f < 1f; f += 0.1f)
            {
                transform.position += transform.forward * 0.2f;
                yield return new WaitForSeconds(0.001f);
            }

            
            yield return new WaitForSeconds(0.05f);
            _isMoving = false;
        }

        private IEnumerator MoveToObstacle(Axis axisGame)
        {
            for (float f = 0; f <= 1f; f += 0.1f)
            {
                transform.position += transform.forward * 0.05f;
                yield return new WaitForSeconds(0.001f);
            }

            for (float f = 0; f < 18; f += 1f)
            {
                RotateOnHimself(Vector3.right, -5);
                yield return new WaitForSeconds(0.001f);
            }

            for (float f = 0; f <= 1f; f += 0.1f)
            {
                transform.position += transform.forward * 0.05f;
                yield return new WaitForSeconds(0.001f);
            }

            yield return new WaitForSeconds(0.1f);
            _isMoving = false;
            
        }

        private IEnumerator MoveToEmpty(Axis axisGame)
        {
            for (float f = 0; f <= 1f; f += 0.1f)
            {
                transform.position += transform.forward * 0.15f;
                yield return new WaitForSeconds(0.001f);
            }

            for (float f = 0; f < 18; f += 1f)
            {
                RotateOnHimself(Vector3.right, 5);
                yield return new WaitForSeconds(0.001f);
            }

            for (float f = 0; f <= 1f; f += 0.1f)
            {
                transform.position += transform.forward * 0.15f;
                yield return new WaitForSeconds(0.001f);
            }

            yield return new WaitForSeconds(0.05f);
            _isMoving = false;
        }

        private bool CheckNextMove()
        {
            var ground = GameObject.FindGameObjectsWithTag("Ground");
            var box = GameObject.FindGameObjectWithTag("ActualBox");
            var nextBox = box.transform.position + transform.forward * 2;
            print("nextBox = " + nextBox);
            foreach (var boxes in ground)
            {
                if (boxes.transform.position == nextBox) return false;
            }
            return true;
        }

        private void RotateOnHimself(Vector3 vectorRotation, int angle)
        {
            if (angle == -5)
                transform.Rotate(-vectorRotation * 5);
            else if (angle == 5) transform.Rotate(vectorRotation * 5);
        }

        private void OnTriggerEnter(Collider other)
        {
            var colliderTag = other.tag;
            if ("Ground" == colliderTag) _isObstacle = true;

        
        }

        private void OnTriggerExit(Collider other)
        {
            _isObstacle = false;
        }

        private void OnCollisionEnter(Collision collision)
        {
            _onGround = true;
        }

        private void OnCollisionExit(Collision collision)
        {
            _onGround = false;
        }

        private void SetGravity(Axis axis)
        {
            Physics.gravity = -transform.up * 10;
        }

        private void JumpMove(Axis axisGame)
        {
            _onGround = false;
            _rb.velocity = transform.up * 5f;
        }

        private Axis ReturnAxis(Axis axisGame)//Axis of the game
        {
            switch (axisGame)
            {
                case Axis.Z:
                {
                    axisGame = Axis.Y;
                    break;
                }
                case Axis.Y:
                {
                    axisGame = Axis.Z;
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException("axisGame", axisGame, null);
            }

            return axisGame;
        }
    }
}
