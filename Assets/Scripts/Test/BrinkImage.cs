using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrinkImage : MonoBehaviour
{
    [SerializeField]
    private float fadeSpeed = 0.0f;
    private Image image;
    private Color color;
    private float time = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        image = this.GetComponent<Image>();
        color = image.color;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime * fadeSpeed;
        float alpha = Mathf.Cos(time) + 1.0f;
        image.color = new Color(this.color.r, this.color.g, this.color.b, alpha);
    }
}
