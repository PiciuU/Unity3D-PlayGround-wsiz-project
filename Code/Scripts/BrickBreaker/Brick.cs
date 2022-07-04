using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrickBreaker
{
    public class Brick : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;

        [Header("Properties")]
        public Sprite[] states;
        public int points = 10;
        public int state;
        public bool unbreakable;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            if (!unbreakable)
            {
                state = states.Length;
                _spriteRenderer.sprite = states[state - 1];
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.name == "Ball")
            {
                OnHit();
            }
        }

        private void OnHit()
        {
            if (unbreakable) return;

            state -= 1;

            if (state <= 0) {
                gameObject.SetActive(false);
            } else {
                _spriteRenderer.sprite = states[state - 1]; 
            }

            FindObjectOfType<GameManager>().OnBrickHit(this);

        }
    }
}