using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySoundEffect : MonoBehaviour
{
    [SerializeField]
    private int _secondsToDestroy = 1;
    private void Start()
    {
        Destroy(gameObject, _secondsToDestroy);
    }
}
