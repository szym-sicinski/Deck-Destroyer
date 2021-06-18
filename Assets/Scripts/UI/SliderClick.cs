using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderClick : MonoBehaviour
{
    private MusicManager musicManager;
    private void Start()
    {
        musicManager = FindObjectOfType<MusicManager>();
    }

    private void OnMouseDown()
    {
        Debug.Log("OnMouseUP");
        musicManager.PlaySound(SoundType.CLICK);
    }
}
