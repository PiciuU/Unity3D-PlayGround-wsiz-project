using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong
{
    public class PlayerPaddle : Paddle
    {
        public Vector2 _direction;
        private int controls;

        private void Awake()
        {
            controls = -1;
        }

        private void Update()
        {

            if (controls == 1)
            {
                if (Input.acceleration.x < -0.05f)
                {
                    if (Input.acceleration.x < -0.15f) _direction = Vector2.left;
                    else _direction = Vector2.left / 2;
                }
                else if (Input.acceleration.x > 0.05f)
                {
                    if (Input.acceleration.x > 0.15f) _direction = Vector2.right;
                    else _direction = Vector2.right / 2;
                }
                else
                {
                    _direction = Vector2.zero;
                }
            }
            else if (controls == 0)
            {
                if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);
                    Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

                    if (touchPosition.x < 0.0f)
                    {
                        _direction = Vector2.left;
                    }
                    else if (touchPosition.x > 0.0f)
                    {
                        _direction = Vector2.right;
                    }
                }
                else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                {
                    _direction = Vector2.left;
                }
                else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                {
                    _direction = Vector2.right;
                }
                else
                {
                    _direction = Vector2.zero;
                }
            }
        }

        private void FixedUpdate()
        {
            //if (_direction.sqrMagnitude != 0)
            //{
            //    _rigidbody.AddForce(_direction * this.speed);
            //}
            if (_direction != Vector2.zero)
            {
                _rigidbody.AddForce(_direction * speed);
            }
        }

        public void SetControls(int value)
        {
            controls = value;
        }
    }
}
