using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class Alert : MonoBehaviour
{
    [SerializeField]
    private Image alertImage = null;

    [SerializeField]
    private float alertDistance = 0.0f;

    [SerializeField]
    private GameObject player;

    private CinemachineDollyCart myCart = null;
    private float pathLength = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        this.alertImage.enabled = false;

        this.myCart = player.GetComponent<CinemachineDollyCart>();
    }

    // Update is called once per frame
    void Update()
    {
        this.pathLength = this.myCart.m_Path.PathLength;

        // 警告ゾーンに入るとUIを有効化
        if (this.pathLength - this.alertDistance <= this.myCart.m_Position)
        {
            this.alertImage.enabled = true;
        }
        else
            this.alertImage.enabled = false;

    }
}
