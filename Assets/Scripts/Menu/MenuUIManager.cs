using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuUIManager : MonoBehaviour
{
    [SerializeField] private new Renderer renderer;
    [SerializeField] private Canvas settings;
    [SerializeField] private Canvas mainCanvas;
    [SerializeField] private AudioMixer music;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundEffectsSlider;
    private const float SCROLL_SPEED = 0.05f;
    private float offset;
    private MusicManager musicManager;

    private void Start()
    {
        musicManager = FindObjectOfType<MusicManager>();

        musicSlider.value = PlayerPrefs.GetFloat("musicVolume", 0);
        soundEffectsSlider.value = PlayerPrefs.GetFloat("effectsVolume", 0);
        
        music.SetFloat("effectsVolume", soundEffectsSlider.value );
        music.SetFloat("musicVolume", musicSlider.value );
    }

    private void Update()
    {
        offset += SCROLL_SPEED * Time.deltaTime;
        renderer.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void FacebookClick()
    {
        System.Diagnostics.Process.Start("http://www.facebook.com");
    }
    public void TwitterClick()
    {
        System.Diagnostics.Process.Start("http://www.twitter.com");
    }

    public void SettingsVisibility(bool isVisible)
    {
        settings.gameObject.SetActive(isVisible);
        mainCanvas.gameObject.SetActive(!isVisible);
    }

    public void ChangeMusicVolume()
    {
        music.SetFloat("musicVolume", musicSlider.value );
    }
    public void ChangeEffectsVolume()
    {
        music.SetFloat("effectsVolume", soundEffectsSlider.value );
        musicManager.PlaySound(SoundType.CLICK);
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("musicVolume",musicSlider.value);
        PlayerPrefs.SetFloat("effectsVolume",soundEffectsSlider.value);
    }
}
