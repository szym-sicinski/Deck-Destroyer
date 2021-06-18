using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpTab : MonoBehaviour
{
    [TextArea] public string description;
    [SerializeField] private int id;
    private HelpCanvasManager helpManager;
    private void Start()
    {
        helpManager = FindObjectOfType<HelpCanvasManager>();
        GetComponent<Button>().onClick.AddListener(() => { helpManager.TabClicked(id); });
    }
}
