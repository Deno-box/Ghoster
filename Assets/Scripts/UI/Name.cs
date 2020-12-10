using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Name : MonoBehaviour {
    public InputField InputField;
    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        //Componentをお仕えるようにする
        InputField = InputField.GetComponent<InputField>();
        text = text.GetComponent<Text>();
    }

    public void InputText()
    {
        //テキストにinputFieldの内容を反映
        text.text = InputField.text;

        
    }
        void Update()
    {
        
    }
}
