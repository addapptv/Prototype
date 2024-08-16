using UnityEngine;
using UnityEngine.UI;

public class LocationNamer : MonoBehaviour
{

    public string locationName;
    public GameObject locationUI;

    void OnTriggerEnter(Collider player)
    {
        if (player.gameObject.tag == "Player")
        {
            locationUI.SetActive(true);
            locationUI.GetComponent<Text>().text = locationName;
        }
    }
    void OnTriggerExit(Collider player)
    {
        if (player.gameObject.tag == "Player")
        {
            locationUI.SetActive(false);
        }
    }
}