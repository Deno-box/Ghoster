using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 常に実行する
//[ExecuteAlways]
public class LineEditor : MonoBehaviour
{
    // レーンのポイント
    [SerializeField]
    private List<Vector3> pointList = new List<Vector3>();
    private List<Vector3> PointList { get { return pointList; } }

    private List<GameObject> objList = new List<GameObject>();


    // Update is called once per frame
    private void Update()
    {
        if(objList.Count>0)
        {
            foreach(var obj in objList)
            {
                Destroy(obj);
            }
        }

        if(pointList.Count > 0)
        {
            for(int i=0;i<pointList.Count;i++)
            {
                GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                obj.transform.position = pointList[i];
                objList.Add(obj);
            }
        }
    }
}
