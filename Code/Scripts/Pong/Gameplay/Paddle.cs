using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong
{
    public class Paddle : MonoBehaviour
    {

        [Header("Properties")]
        public float speed = 10.0f;

        [Header("Environment")]
        public Rigidbody2D _rigidbody;

        public void SetProperties(Dictionary<string, float> properties)
        {
            this.speed = properties["speed"];
            transform.localScale = new Vector3(transform.localScale.x, properties["scale"], transform.localScale.z);
        }

        public void ResetPosition()
        {
            _rigidbody.position = new Vector2(0.0f, _rigidbody.position.y);
            _rigidbody.velocity = Vector2.zero;
        }
    }
}
