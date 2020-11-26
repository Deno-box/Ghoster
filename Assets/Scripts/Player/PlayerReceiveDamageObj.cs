using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReceiveDamageObj : MonoBehaviour
{
    private PlayerIdleState idlestate;

    private void Start()
    {
        idlestate = this.transform.parent.GetComponent<PlayerIdleState>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "EnemyBullet")
        {
            idlestate.ReceiveDamage();
        }
    }
}
