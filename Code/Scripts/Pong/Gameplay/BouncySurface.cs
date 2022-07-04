using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong
{
    public class BouncySurface : MonoBehaviour
    {
        enum Surface
        {
            Paddle,
            Border
        };

        [SerializeField]
        private Surface _surface;
        public float _bounceStrength;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Ball ball = collision.gameObject.GetComponent<Ball>();

            if (ball == null) return;

            AudioManager._instance.PlaySound($"Audio/{_surface}HitSoundEffect");
            Vector2 normal = collision.GetContact(0).normal;
            ball.AddForce(-normal * _bounceStrength);
        }
    }
}