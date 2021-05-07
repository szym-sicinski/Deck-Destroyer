using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class ButtonClickSound : MonoBehaviour
{
    private MusicManager musicManager;
    private void Start()
    {
        musicManager = FindObjectOfType<MusicManager>();
    }
    public void ClickSound()
    {
        musicManager.PlaySound(SoundType.CLICK);
    }
}
