using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrinkText : MonoBehaviour
{
    [SerializeField]
    private float fadeSpeed = 0.0f;
    private Text text;
    private Color color;
    private float time = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        text = this.GetComponent<Text>();
        color = text.color;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime * fadeSpeed;
        float alpha = Mathf.Cos(time) + 1.0f;
        text.color = new Color(this.color.r, this.color.g, this.color.b,alpha) ;
    }
}
