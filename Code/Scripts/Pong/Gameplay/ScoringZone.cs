using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Pong
{
    public class ScoringZone : MonoBehaviour
    {
        public EventTrigger.TriggerEvent scoreTrigger;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Ball ball = collision.gameObject.GetComponent<Ball>();

            if (ball == null) return;

            AudioManager._instance.PlaySound($"Audio/ScorePointSoundEffect");
            BaseEventData eventData = new BaseEventData(EventSystem.current);
            this.scoreTrigger.Invoke(eventData);

        }
    }
}
