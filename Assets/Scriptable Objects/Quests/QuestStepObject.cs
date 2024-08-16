using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest Step Object", menuName = "Quest System/Quest Step", order = 2)]

public class QuestStepObject : ScriptableObject
{
    [Header("Info")]
    public QuestStepType questStepType;
    [TextArea(5, 10)]
    public string completeMessage;
    [TextArea(5, 10)]
    public string nextObjective;
    [TextArea(5, 10)]
    public string questStepDescription;

}

public enum QuestStepType
{
    Reach,
    Find,
    Collect,
    Gather,
    Deliver,
    Default
}

