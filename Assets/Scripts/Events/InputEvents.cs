using UnityEngine;
using System;

//TO DO//
// Remove need for input events and use the InputHandler.Instance directly for calling input actions

public class InputEvents
{
    public event Action OnSubmitPressed;
    public void SubmitPressed()
    {
        if (OnSubmitPressed != null)
        {
            OnSubmitPressed();
        }
    }

    public event Action OnMenuPressed;
    public void MenuPressed()
    {
        if (OnMenuPressed != null)
        {
            OnMenuPressed();
        }
    }

    public event Action OnQuestListPressed;
    public void QuestListPressed()
    {
        if (OnQuestListPressed != null)
        {
            OnQuestListPressed();
        }
    }

    public event Action OnSwitchCamPressed;
    public void SwitchCamPressed()
    {
        if (OnSwitchCamPressed != null)
        {
            OnSwitchCamPressed();
        }
    }
}
