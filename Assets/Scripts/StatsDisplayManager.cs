using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsDisplayManager : MonoBehaviour
{
    private SaveManager saveManager;
    [SerializeField] private Canvas statsCanvas;
    [SerializeField] private TMP_Text levelUpText;

    [SerializeField] private GameObject maleInfo;
    [SerializeField] private TMP_Text maleCurrentHp;
    [SerializeField] private TMP_Text maleMaxHp;
    [SerializeField] private TMP_Text maleStr;
    [SerializeField] private TMP_Text maleDex;

    [SerializeField] private GameObject femaleInfo;
    [SerializeField] private TMP_Text femaleCurrentHp;
    [SerializeField] private TMP_Text femaleMaxHp;
    [SerializeField] private TMP_Text femaleStr;
    [SerializeField] private TMP_Text femaleDex;



    private void Awake()
    {
        saveManager = FindObjectOfType<SaveManager>();
    }

    public void ShowStats(bool isLevelUp = false)
    {
        statsCanvas.gameObject.SetActive(true);

        foreach (Player player in saveManager.players)
        {
            if (player.CompareTag("Player Male"))
            {
                maleInfo.SetActive(true);
                maleCurrentHp.text = player.CurrentHP.ToString();
                maleMaxHp.text = player.MaxHP.ToString();
                maleStr.text = player.CurrentStr.ToString();
                maleDex.text = player.CurrentDex.ToString();
            }
            else if (player.CompareTag("Player Female"))
            {
                femaleInfo.SetActive(true);
                femaleCurrentHp.text = player.CurrentHP.ToString();
                femaleMaxHp.text = player.MaxHP.ToString();
                femaleStr.text = player.CurrentStr.ToString();
                femaleDex.text = player.CurrentDex.ToString();
            }
        }
        if (isLevelUp && levelUpText != null)
            levelUpText.gameObject.SetActive(true);
    }
}
