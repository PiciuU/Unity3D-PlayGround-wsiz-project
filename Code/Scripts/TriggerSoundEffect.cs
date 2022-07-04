using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSoundEffect : MonoBehaviour
{
   public void PlaySound()
    {
        AudioManager._instance.PlaySound(gameObject);
    }
}
