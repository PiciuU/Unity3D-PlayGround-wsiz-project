using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Pong
{
    public abstract class Bonus : MonoBehaviour
    {
        [HideInInspector] public SpecialModeManager _specialModeManager;
        public string itemText;

        protected GameObject objectToApply;
        protected bool hasBeenUsed;

        protected bool IsCollisionCorrect(Collider2D collision)
        {
            if (collision.name != "Ball") return false;
            if (collision.GetComponent<Rigidbody2D>().velocity.y > 0.0f) objectToApply = GameObject.Find("Player");
            else objectToApply = GameObject.Find("Computer");
            return true;
        }

        public void TryToDestroy()
        {
            if (!hasBeenUsed) _specialModeManager.RemoveItem(gameObject);
        }

        public void ForceDestroy()
        {
            _specialModeManager.RemoveItem(gameObject);
        }
        protected void DisplayBonusInformation()
        {
            GameObject bonusEffectText = GameObject.Find("BonusEffectText");
            bonusEffectText.GetComponent<TextMeshProUGUI>().text = itemText;
            bonusEffectText.GetComponent<Animator>().SetTrigger("Play");
        }

        protected abstract void ApplyEffect();
        protected abstract void RevertEffect();
    }
}