using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InteractableController))]
public class Signpost : MonoBehaviour
{
    //INTERACTABLE FOR SIGNPOST - WILL TRIGGER THE INSPECT CAMERA//

    public string interactName;
    public string interactPrompt;
    public int interactCooloffTime;

    private void Awake()
    {
        GetComponentInParent<InteractableController>().prompt = interactPrompt;
    }

    public void ReadSignpost()
    {
        //Add methhod for invoking inspect camera//
        GetComponentInParent<InteractableController>().interactionName = interactName;
        GetComponentInParent<InteractableController>().cooloffTime = interactCooloffTime;

        Debug.Log("Signpost read");
        return;
    }

}
