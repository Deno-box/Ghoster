using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TestPlayerJump : MonoBehaviour
{
    [SerializeField]
    private PlayerMoveData moveData;

    private CinemachineDollyCart myCart;

    private GameObject nextJumpPosObj;

    [SerializeField]
    private float jumpZone = 50.0f;
    [SerializeField]
    private float jumpSpeed = 30.0f;

    // Start is called before the first frame update
    void Start()
    {
        myCart = this.GetComponent<CinemachineDollyCart>();

        this.nextJumpPosObj = new GameObject();
        this.nextJumpPosObj.AddComponent<CinemachineDollyCart>();
        this.nextJumpPosObj.transform.parent = this.transform;
        this.nextJumpPosObj.name = "NextJumpPosObj";
    }

    // Update is called once per frame
    void Update()
    {
        // 上キーでジャンプ
       if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            myCart = this.GetComponent<CinemachineDollyCart>();
            // ジャンプゾーン範囲内なら次のパスへジャンプする
            if (moveData.nowPath.PathLength - jumpZone <= myCart.m_Position)
            {

            }
        }
    }
}
