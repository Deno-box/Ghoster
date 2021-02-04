using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInputHightLightUI : MonoBehaviour
{
    [SerializeField]
    private Image LTImage = null;
    [SerializeField]
    private Image RTImage = null;
    [SerializeField]
    private Image jumpImage = null;
    [SerializeField]
    private Image stickImage = null;

    private void Update()
    {
        LTImage.gameObject.SetActive(false);
        RTImage.gameObject.SetActive(false);
        jumpImage.gameObject.SetActive(false);
        stickImage.gameObject.SetActive(false);

        float stickHori = Input.GetAxisRaw("Horizontal");
        float trigger = Input.GetAxis("LRTrigger");
        // LTを発光
        if (trigger < 0)
            LTImage.gameObject.SetActive(true);
        // RTを発光
        if (trigger > 0)
            RTImage.gameObject.SetActive(true);
        // JumpUIを発光
        if (Input.GetKey("joystick button 0"))
            jumpImage.gameObject.SetActive(true);
        // Stickを発光
        if (stickHori != 0)
            stickImage.gameObject.SetActive(true);

    }
}
