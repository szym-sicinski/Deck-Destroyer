using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SoundType
{
    CLICK,
    SMACK,
    COINS,
    LEVEL_UP,
    SPELL,
    BLOCK,
    DOOR,
    DIE
}
public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;
    [SerializeField] private AudioClip[] smackClips;
    [SerializeField] private AudioClip[] coinsClips;

    [SerializeField] private AudioClip clickClip;
    [SerializeField] private AudioClip spellClip;
    [SerializeField] private AudioClip blockClip;
    [SerializeField] private AudioClip levelUpClip;
    [SerializeField] private AudioClip doorClip;
    [SerializeField] private AudioClip dieClip;
    

    [SerializeField] private AudioClip mainThemeClip;
    //[SerializeField] private AudioSource soundEffects;

    [SerializeField] private GameObject soundEffectPrefab;

    //private AudioSource audioSource;
    // Start is called before the first frame update
    private void Awake()
    {
        // SINGLETON
        if (instance == null)
            instance = this;
        else
            if (instance != this)
        {
           // gameObject.SetActive(false);
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
    }
    public void PlaySound(SoundType type)
    {
        AudioClip chosenClip = null;

        switch (type)
        {
            case SoundType.CLICK:
                chosenClip = clickClip;
                break;
            case SoundType.SMACK:
                chosenClip = smackClips[UnityEngine.Random.Range(0, smackClips.Length)];
                break;
            case SoundType.COINS:
                chosenClip = coinsClips[UnityEngine.Random.Range(0, coinsClips.Length)];
                break;
            case SoundType.LEVEL_UP:
                chosenClip = levelUpClip;
                break;
            case SoundType.SPELL:
                chosenClip = spellClip;
                break;
            case SoundType.BLOCK:
                chosenClip = blockClip;
                break;
            case SoundType.DOOR:
                chosenClip = doorClip;
                break;
            case SoundType.DIE:
                chosenClip = dieClip;
                break;
        }
        GameObject soundEffect = Instantiate(soundEffectPrefab, transform);
        AudioSource soundEffectAS = soundEffect.GetComponent<AudioSource>();

        soundEffectAS.clip = chosenClip;
        soundEffectAS.Play();

        Destroy(soundEffect, chosenClip.length);
    }

}
