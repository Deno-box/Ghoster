using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonController : MonoBehaviour
{
    // 最初にフォーカスするゲームオブジェクト
    [SerializeField]
    private GameObject firstSelect;

    bool flag;
    // Start is called before the first frame update
    void Start()
    {
        //選択状態にする
		EventSystem.current.SetSelectedGameObject(firstSelect);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
