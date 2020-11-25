using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyScriptable/Create PlayerMoveDataList")]
public class PlayerMoveDataList : ScriptableObject
{
    public List<PlayerMoveData> moveDataList = new List<PlayerMoveData>();
}
