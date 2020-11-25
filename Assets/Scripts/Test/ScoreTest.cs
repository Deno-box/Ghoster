using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Xキー押下
        if (Input.GetKeyDown(KeyCode.X))
        {
            GameDataManager.AddDecisionNum((int)GameDataManager.SCORE_TYPE.GREAT);
        }
        // Cキー押下
        if (Input.GetKeyDown(KeyCode.C))
        {
            GameDataManager.AddDecisionNum((int)GameDataManager.SCORE_TYPE.GOOD);
        }
        // Zキー押下
        if (Input.GetKeyDown(KeyCode.Z))
        {
            GameDataManager.AddDecisionNum((int)GameDataManager.SCORE_TYPE.MISS);
        }

    }
}
