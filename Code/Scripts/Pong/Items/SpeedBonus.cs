using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong
{
    public class SpeedBonus : Bonus
    {
        [SerializeField] private float specialSpeed;
        [SerializeField] private float effectDuration;

        private float defaultSpecialSpeed;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!IsCollisionCorrect(collision)) return;
            ApplyEffect();
        }

        protected override void ApplyEffect()
        {
            hasBeenUsed = true;
            DisplayBonusInformation();
            gameObject.SetActive(false);
            defaultSpecialSpeed = objectToApply.GetComponent<Paddle>().speed;
            objectToApply.GetComponent<Paddle>().speed = specialSpeed;
            Invoke(nameof(RevertEffect), effectDuration);
        }

        protected override void RevertEffect()
        {
            objectToApply.GetComponent<Paddle>().speed = defaultSpecialSpeed;
            ForceDestroy();
        }

    }
}
