using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrickBreaker
{
    public class Paddle : MonoBehaviour
    {
        [Header("Environment")]
        public Rigidbody2D _rigidbody;
        public Vector2 _direction;
        [SerializeField]
        private int controls;

        [Header("Properties")]
        public float speed = 10.0f;
        public float maxBounceAngle = 75f;

        private void Awake()
        {
            controls = -1;
            _rigidbody = GetComponent<Rigidbody2D>();
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
            if (_direction != Vector2.zero)
            {
                _rigidbody.AddForce(_direction * speed);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Ball ball = collision.gameObject.GetComponent<Ball>();

            if (ball != null)
            {
                Rigidbody2D rigidbody = ball.GetComponent<Rigidbody2D>();
                Vector3 paddlePosition = transform.position;
                Vector2 contactPoint = collision.GetContact(0).point;

                float offset = paddlePosition.x - contactPoint.x;

                float paddleWidth = collision.otherCollider.bounds.size.x / 2;

                float currentAngle = Vector2.SignedAngle(Vector2.up, rigidbody.velocity);
                float bounceAngle = (offset / paddleWidth) * maxBounceAngle;
                float newAngle = Mathf.Clamp(currentAngle + bounceAngle, -maxBounceAngle, maxBounceAngle);

                Quaternion rotation = Quaternion.AngleAxis(newAngle, Vector3.forward);

                rigidbody.velocity = rotation * Vector2.up * rigidbody.velocity.magnitude;
            }
        }

        public void SetControls(int value)
        {
            controls = value;
        }

        public void DisableControls()
        {
            controls = -1;
            ResetPosition();
        }

        public void ResetPosition()
        {
            _direction = Vector2.zero;
            _rigidbody.position = new Vector2(0.0f, _rigidbody.position.y);
            _rigidbody.velocity = Vector2.zero;
        }
    }
}
