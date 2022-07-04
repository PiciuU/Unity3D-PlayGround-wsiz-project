using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrickBreaker
{
    public class OnSurfaceEnter : MonoBehaviour
    {
        enum Surface
        {
            Paddle,
            Border
        };

        [SerializeField]
        private Surface _surface;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Ball ball = collision.gameObject.GetComponent<Ball>();

            if (ball == null) return;

            AudioManager._instance.PlaySound($"Audio/{_surface}HitSoundEffect");
        }
    }
}