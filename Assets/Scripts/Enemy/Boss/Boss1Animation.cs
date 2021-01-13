using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Animation : MonoBehaviour
{
    private CharacterController animController = null;

    private Animator animator = null;

    private GameObject bossModel = null;

    [SerializeField]
    private float damageAnimTimer = 0.0f;
    private float timer = 0.0f;
    private bool isDamage = false;

    // Start is called before the first frame update
    void Start()
    {

        bossModel = transform.GetChild(0).gameObject;

        animController = bossModel.GetComponent<CharacterController>();
        animator = bossModel.GetComponent<Animator>();

        timer = damageAnimTimer;
    }

    // Update is called once per frame
    void Update()
    {
        //if (isDamage)
        //{
        //    timer--;

        //    if (timer < 0.0f)
        //    {
        //        isDamage = false;
        //        animator.SetBool("Damage", false);
        //        timer = damageAnimTimer;
        //    }
        //}
        //Debug.Log(isDamage);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyBullet")
        {
            if (other.GetComponent<EnemyBulletStateController>().lastStateEnum == EnemyBulletStateController.BulletStateEnum.Parry)
                bossModel.GetComponent<Animator>().Play("DamageAnim");
        }
    }

}
