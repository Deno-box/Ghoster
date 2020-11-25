using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(LineRenderer), typeof(CinemachinePathBase))]
[ExecuteAlways]

public class LineUpdater : MonoBehaviour
{
    [Range(1, 200)] public int tessellation;
    private CinemachinePathBase path;
    private LineRenderer line;

    private void Update()
    {
        if (this.path == null)
        {
            this.path = this.GetComponent<CinemachinePathBase>();
        }

        if (this.line == null)
        {
            this.line = this.GetComponent<LineRenderer>();
        }

        this.tessellation = Mathf.Max(this.tessellation, 1);
        var step = 1.0f / this.tessellation;
        var positionCount = ((int)(this.path.MaxPos - this.path.MinPos) * this.tessellation) + 1;
        this.line.positionCount = positionCount;
        for (var i = 0; i < positionCount; i++)
        {
            this.line.SetPosition(i, this.path.EvaluatePosition(i * step));
        }
    }
}
