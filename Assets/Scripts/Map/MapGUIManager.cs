using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum LocationType
{
    NORMAL_FIGHT,
    HARD_FIGHT,
    MERCHANT,
    CAMPFIRE,
    RANDOM
}
public class MapGUIManager : MonoBehaviour
{

    [SerializeField] private GameObject[] buttonsSpawners;
    [SerializeField] private GameObject[] buttonsPrefabs;
    public LocationType clickLocation;

    public void LocationClick(LocationType type)
    {
        switch (type)
        {
            case LocationType.NORMAL_FIGHT:
                SceneManager.LoadScene(2);
                break;
            case LocationType.HARD_FIGHT:

                break;
            case LocationType.CAMPFIRE:

                break;
            case LocationType.MERCHANT:

                break;
            case LocationType.RANDOM:

                break;
            default:
                break;
        }
    }

}
