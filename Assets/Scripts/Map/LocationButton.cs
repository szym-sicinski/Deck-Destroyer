using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationButton : MonoBehaviour
{
    public LocationType location;
    //private PathManager pathManager;
    //private void Start()
    //{
    //    pathManager = FindObjectOfType<PathManager>();
    //    //Debug.Log(Camera.main.ViewportToWorldPoint(transform.position));
    //    Debug.Log(Camera.main.ViewportToWorldPoint(transform.position));
    //}
    public void Click()
    {
        FindObjectOfType<MapGUIManager>().LocationClick(location);
    }


}
