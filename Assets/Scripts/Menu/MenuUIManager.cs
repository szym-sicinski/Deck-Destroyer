using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUIManager : MonoBehaviour
{
    [SerializeField] private new Renderer renderer;
    private const float SCROLL_SPEED = 0.05f;
    private float offset;

    private void Update()
    {
        offset += SCROLL_SPEED * Time.deltaTime;
        renderer.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
    public void Exit()
    {
        
    }
}
