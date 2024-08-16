using UnityEngine;
using UnityEngine.UI;

public class LocationNamer : MonoBehaviour
{
    public string locationName;
    public LocationManager _locationManager;


    void OnTriggerEnter(Collider player)
    {
        if (player.gameObject.tag == "Player")
        {
/*            _locationManager = FindObjectOfType<LocationManager>();*/
            _locationManager.currentLocation = locationName;
            _locationManager.droppedItemParent = this.transform;
            _locationManager.locationUI.SetActive(true);
            _locationManager.locationUI.GetComponent<Text>().text = _locationManager.currentLocation;
        }
    }
    void OnTriggerExit(Collider player)
    {
        if (player.gameObject.tag == "Player")
        {
            _locationManager.currentLocation = "";
            _locationManager.droppedItemParent = _locationManager.defaultItemParent;
            _locationManager.locationUI.SetActive(false);
        }
    }
}