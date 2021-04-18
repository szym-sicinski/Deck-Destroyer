using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationButton : MonoBehaviour
{
    public LocationType location;

    public void Click()
    {
        FindObjectOfType<MapGUIManager>().LocationClick(location);
    }
}
