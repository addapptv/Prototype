using System;

public class MiscEvents
{
    public event Action<string> OnInteract;
    public void Interact(string interactableName)
    {
        if (OnInteract != null)
        {
            OnInteract(interactableName);
        }
    }
}
