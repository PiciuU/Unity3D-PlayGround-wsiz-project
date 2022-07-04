using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong
{
    public class FreezeBonus : Bonus
    {
        [SerializeField] private float effectDuration;

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
            objectToApply.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            Invoke(nameof(RevertEffect), effectDuration);
        }

        protected override void RevertEffect()
        {
            objectToApply.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            ForceDestroy();
        }

    }
}
