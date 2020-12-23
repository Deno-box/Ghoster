using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField]
    PlayerStatusData playerStatus = null;
    public PlayerStatusData PlayerStatus { get { return playerStatus; } }

    [SerializeField]
    AudioSource audioSource = null;
    public AudioSource AudioSource { get { return audioSource; } }
}
