using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong
{
    public class BouncyBonus : Bonus
    {
        [SerializeField] private float specialBounceStrength;
        [SerializeField] private float effectDuration;

        private float defaultBounceStrength;
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
            defaultBounceStrength = objectToApply.GetComponent<BouncySurface>()._bounceStrength;
            objectToApply.GetComponent<BouncySurface>()._bounceStrength = specialBounceStrength;
            Invoke(nameof(RevertEffect), effectDuration);
        }

        protected override void RevertEffect()
        {
            objectToApply.GetComponent<BouncySurface>()._bounceStrength = defaultBounceStrength;
            ForceDestroy();
        }

    }
}
