using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReceiveDamageObj : MonoBehaviour
{
    [SerializeField]
    private PlayerReceiveDamage receiveDamage = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnemyBullet")
        {
            receiveDamage.Initialize();
        }
    }
}
