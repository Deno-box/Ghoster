using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireFrame : MonoBehaviour
{
    // ワイヤーフレームで表示
    void Start()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh.SetIndices(meshFilter.mesh.GetIndices(0), MeshTopology.Lines, 0);
    }
}
