using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public bool IsDetectable { get; }
    public bool IsTriggered { get; }
    public string InteractionPrompt { get; }
    public bool Interact(Interactor interactor);
}
