using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamSwitcher : MonoBehaviour
{

    [SerializeField]
    private CinemachineVirtualCamera virtualCam;
    [SerializeField]
    private CinemachineFreeLook freeLookCam;

    private bool topDownCamera = true;

    public void SwitchCam()
    {
        if(topDownCamera)
        {
            StartCoroutine(SwitchToTPControls());
            virtualCam.Priority = 0;
            freeLookCam.Priority = 1;
            Cursor.visible = true;
        }
        else
        {
            StartCoroutine(SwitchToTDControls());
            virtualCam.Priority = 1;
            freeLookCam.Priority = 0;
            GameObject.Find("Player").GetComponent<TopDownController>().enabled = true;
            GameObject.Find("Player").GetComponent<ThirdPersonController>().enabled = false;
            Cursor.visible = true;
        }

        topDownCamera = !topDownCamera;

    }

    IEnumerator SwitchToTPControls()
    {
        yield return new WaitForSeconds(1);
        GameObject.Find("Player").GetComponent<TopDownController>().enabled = false;
        GameObject.Find("Player").GetComponent<ThirdPersonController>().enabled = true;
    }

    IEnumerator SwitchToTDControls()
    {
        GameObject.Find("Player").GetComponent<ThirdPersonController>().enabled = false;
        yield return new WaitForSeconds(1);
        GameObject.Find("Player").GetComponent<TopDownController>().enabled = true;
    }


}
