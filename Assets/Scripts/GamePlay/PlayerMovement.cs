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

        public CameraMovement cam;
        public Life playerLife;

        private bool _isEmpty;
        private bool _isObstacle;
        private bool _onGround;
        private Rigidbody _rb;
        private StartingBox _startingBox;

        public bool IsMoving { get; set; }
        public bool canMove; 
        public bool IsRotating { get; set; }


        // Use this for initialization
        private void Start()
        {
            axisGame = Axis.Z;
            _isObstacle = false;
            _isEmpty = false;
            _onGround = true;
            
            canMove = true;
            IsMoving = false;
            IsRotating = false;


            SetGravity(axisGame);
            _rb = GetComponent<Rigidbody>();
            _rb.useGravity = false;
            _startingBox = GameObject.Find("Start").GetComponent<StartingBox>();
            //#BUG00
            print("sphere depart : " + transform.position);       
        }

        // Update is called once per frame
        private void Update()
        {
            //_isEmpty = CheckNextMove();
            if (_onGround) _rb.useGravity = false;
            if (Input.GetButtonDown("Jump"))
            {
                _rb.useGravity = true;
                JumpMove(axisGame);
            }

            if(canMove){
                if ((Input.GetKey("z") | Input.GetKey("w")) && !IsMoving && !IsRotating){

                    IsMoving = true;
                    _isEmpty = CheckNextMove();
                    print("is Empty : " + _isEmpty);
                    if (_isObstacle)
                    {
                        StartCoroutine(MoveToObstacle(axisGame));
                        _isObstacle = false;
                        print("case obstacle : " + transform.position);
                        axisGame = ReturnAxis(axisGame);
                    }
                    else if (_isEmpty)
                    {
                        StartCoroutine(MoveToEmpty(axisGame));
                        axisGame = ReturnAxis(axisGame);
                        print("case empty : " + transform.position);
                        //rb.useGravity = true;
                    }
                    else
                    {
                        StartCoroutine(MoveTo());
                        print("case suivante : " + transform.position);

                    }
                }

                if ((Input.GetKey("q") | Input.GetKey("a")) && !IsMoving && !IsRotating)
                {
                    IsRotating = true;
                    StartCoroutine(RotateSphere(-5, axisGame));
                }
                else if (Input.GetKey("d") && !IsMoving && !IsRotating)
                {
                    IsRotating = true;
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
            IsRotating = false;
        }


        private IEnumerator MoveTo() //Move to next Box
        {
            for (float f = 0; f < 1f; f += 0.1f)
            {
                transform.position += transform.forward * 0.2f;
                yield return new WaitForSeconds(0.001f);
            }

            
            yield return new WaitForSeconds(0.05f);
            IsMoving = false;
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
            IsMoving = false;
            
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
            IsMoving = false;
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
            //print(tag);
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

        private Vector3 GetDirection()
        {
            var x = transform.eulerAngles;
            return x;
        }
        
    }
}
