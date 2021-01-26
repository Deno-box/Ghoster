using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            GetComponent<Animator>().Play("Attack");
        }
        if(Input.GetKeyDown(KeyCode.X))
        {
            GetComponent<Animator>().Play("Damage");
        }
        if(Input.GetKeyDown(KeyCode.C))
        {
            GetComponent<Animator>().Play("Drawal");
        }
    }
}
