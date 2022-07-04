using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong
{
    public class ScaleBonus : Bonus
    {
        [SerializeField] private float specialScale;
        [SerializeField] private float effectDuration;

        private float defaultSpecialScale;
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
            defaultSpecialScale = objectToApply.transform.localScale.y;
            objectToApply.transform.localScale = new Vector3(objectToApply.transform.localScale.x, specialScale, objectToApply.transform.localScale.z);
            Invoke(nameof(RevertEffect), effectDuration);
        }

        protected override void RevertEffect()
        {
            objectToApply.transform.localScale = new Vector3(objectToApply.transform.localScale.x, defaultSpecialScale, objectToApply.transform.localScale.z);
            ForceDestroy();
        }

    }
}
