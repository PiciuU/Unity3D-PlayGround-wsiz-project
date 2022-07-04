using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong
{
    public class Ball : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;

        [Header("Properties")]
        public float speed = 5.0f;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }
        private void FixedUpdate()
        {
            _rigidbody.velocity = _rigidbody.velocity.normalized * speed;
        }

        public void AddStartingForce()
        {
            float x = Random.value < 0.5f ? -1.0f : 1.0f;
            float y = Random.value < 0.5f ? Random.Range(-1.0f, -0.5f) : Random.Range(0.5f, 1.0f);
            Vector2 direction = new Vector2(x, y);
            _rigidbody.AddForce(direction * speed);
        }

        public void AddForce(Vector2 force)
        {
            _rigidbody.AddForce(force);
        }

        public void ResetPosition()
        {
            _rigidbody.position = Vector3.zero;
            _rigidbody.velocity = Vector3.zero;
        }
    }
}
