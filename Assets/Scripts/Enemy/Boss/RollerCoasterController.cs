using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class RollerCoasterController : MonoBehaviour
{
    float drawlPos = 0.0f;
    CinemachineDollyCart rootCart;
    bool isDrawl = false;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        drawlPos = this.GetComponent<BossEnemyBulletGenerator>().DrawlPos;
        rootCart = this.GetComponent<CinemachineDollyCart>();
    }

    // Update is called once per frame
    void Update()
    {
        if(rootCart.m_Position >=drawlPos && isDrawl == false)
        {
            isDrawl = true;
            this.animator.Play("Drawal");
        }
    }
}
