using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong
{
    public class ComputerPaddle : Paddle
    {
        public Rigidbody2D ball;
        private void FixedUpdate()
        {
            if (ball.velocity.y > 0.0f)
            {
                if (ball.position.x > transform.position.x)
                {
                    _rigidbody.AddForce(Vector2.right * speed);
                }
                else if (ball.position.x < transform.position.x)
                {
                    _rigidbody.AddForce(Vector2.left * speed);
                }
            }
            else
            {
                if (transform.position.x > 0.0f)
                {
                    _rigidbody.AddForce(Vector2.left * speed);
                }
                else if (transform.position.x < 0.0f)
                {
                    _rigidbody.AddForce(Vector2.right * speed);
                }
            }
        }
    }
}