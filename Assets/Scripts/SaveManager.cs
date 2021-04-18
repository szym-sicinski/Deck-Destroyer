using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private SaveManager singleton;

    private void Start()
    {
        SaveManager[] managers = FindObjectsOfType<SaveManager>();
        if (managers != null)
        {
            foreach (SaveManager manager in managers)
            {
                Destroy(manager.gameObject);
            }
        }
        else
            singleton = this;
    }

}
