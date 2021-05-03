using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsDisplayManager : MonoBehaviour
{
    private SaveManager saveManager;
    [SerializeField] private Canvas statsCanvas;

    [SerializeField] GameObject maleInfo;
    [SerializeField] TMP_Text maleCurrentHp;
    [SerializeField] TMP_Text maleMaxHp;
    [SerializeField] TMP_Text maleStr;
    [SerializeField] TMP_Text maleDex;

    [SerializeField] GameObject femaleInfo;
    [SerializeField] TMP_Text femaleCurrentHp;
    [SerializeField] TMP_Text femaleMaxHp;
    [SerializeField] TMP_Text femaleStr;
    [SerializeField] TMP_Text femaleDex;



    private void Start()
    {
        saveManager = FindObjectOfType<SaveManager>();
    }

    public void ShowStats()
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

    }
}
