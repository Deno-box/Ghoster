using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Animation : MonoBehaviour
{
    private GameObject bossModel = null;

    // Start is called before the first frame update
    void Start()
    {
        bossModel = transform.GetChild(0).gameObject;
        bossModel.GetComponent<Animator>().Play("AttackAnim");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyBullet")
        {
            if (other.GetComponent<EnemyBulletStateController>().LastStateEnum == EnemyBulletStateController.BulletStateEnum.Parry)
                bossModel.GetComponent<Animator>().Play("DamageAnim");
        }
    }
}
