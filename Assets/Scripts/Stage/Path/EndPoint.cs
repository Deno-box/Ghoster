using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[ExecuteAlways]
public class EndPoint : MonoBehaviour
{
    [SerializeField]
    private CinemachinePathBase path = null;
    [SerializeField]
    private CinemachineDollyCart myCart = null;

    // Update is called once per frame
    void Update()
    {
        this.myCart.m_Position = path.PathLength;
    }
}
