using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleFluffy : MonoBehaviour
{
    [SerializeField]
    private Image titleImage = null;

    private Vector3 nowPos = new Vector3(0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        nowPos = titleImage.rectTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        titleImage.rectTransform.position = new Vector3(nowPos.x, nowPos.y + Mathf.PingPong(Time.time*2, 5), nowPos.z);
    }
}
