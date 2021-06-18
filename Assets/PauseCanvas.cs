using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseCanvas : MonoBehaviour
{
    [SerializeField] private Canvas pauseCanvas;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseCanvas.gameObject.SetActive(!pauseCanvas.gameObject.active);
        }
    }

    public void Exit()
    {
        Application.Quit();
    }
}
