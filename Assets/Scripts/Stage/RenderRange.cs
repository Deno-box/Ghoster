using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderRange : MonoBehaviour
{
    [SerializeField]
    private float range = 0.0f;
    [SerializeField]
    private Transform target = null;

    private Transform trs;

    private void Start()
    {
        trs = this.transform;
    }
    // Update is called once per frame
    void Update()
    {
    }
}
