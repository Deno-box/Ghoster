using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletStatus : MonoBehaviour
{
    public enum Dir
    {
        None,
        Right,
        Left
    }

    public Dir dir = Dir.None;
}
