using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyController : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        this.animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        animator.SetTrigger("Damage");
    }
}
