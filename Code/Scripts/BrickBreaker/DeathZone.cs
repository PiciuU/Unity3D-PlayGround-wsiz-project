using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrickBreaker
{
    public class DeathZone : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.name == "Ball")
            {
                FindObjectOfType<GameManager>().DropHealth();
            }
        }
    }
}
