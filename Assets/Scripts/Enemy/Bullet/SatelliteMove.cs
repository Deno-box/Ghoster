using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteMove : MonoBehaviour
{
    [SerializeField]
    private float radius = 0.0f;
    [SerializeField]
    private float speed = 0.0f;

    // Update is called once per frame
    void FixedUpdate()
    {
        float x = this.radius * Mathf.Sin(Time.time * this.speed);
        float y = 0.0f;
        float z = this.radius * Mathf.Cos(Time.time * this.speed);

        this.transform.localPosition = new Vector3(x,y,z);
    }
}
