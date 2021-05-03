using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HelpCanvasManager : MonoBehaviour
{
    [SerializeField] private Canvas helpCanvas;
    [SerializeField] private Canvas mainCanvas;
    [SerializeField] private Button[] helpTabs;
    [SerializeField] private TMP_Text description;

    private void Start() //It will show help if player first time sees scene
    {
        Scene activeScene = SceneManager.GetActiveScene();

        MarkAsCurrentTab(activeScene.buildIndex - 1); //Set proper tab regarding to scene

        string key = activeScene.name + " seen help";
        if (PlayerPrefs.GetInt(key) != 1)
        {
            DisplayHelp(true);
            PlayerPrefs.SetInt(key, 1);
        }
    }
    public void TabClicked(int id)
    {
        for(int i = 0; i < helpTabs.Length; i++)
        {
            if (id == i)
                MarkAsCurrentTab(i);
            else
                helpTabs[i].interactable = true;
        }
    }

    private void MarkAsCurrentTab(int id)
    {
        helpTabs[id].interactable = false;
        description.SetText(helpTabs[id].GetComponent<HelpTab>().description);
    }

    public void DisplayHelp(bool isHelpVisible)
    {
        helpCanvas.gameObject.SetActive(isHelpVisible);
        //if(isHelpVisible)
        //mainCanvas.gameObject.SetActive(!isHelpVisible);
    }
}
