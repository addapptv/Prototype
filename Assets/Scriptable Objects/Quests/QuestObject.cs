using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestType
{
    Reach,
    Investigate,
    Collect,
    Talk,
    Default
}


public abstract class QuestObject : ScriptableObject
{
    public int Id;
    public string questName;
    public Sprite questIcon;
    public QuestType type;
    [TextArea(15, 20)]
    public string description;
    public float progressValue;
    public float expValue;

    public Quest CreateQuest()
    {
        Quest newQuest = new Quest(this);
        return newQuest;
    }
}


[System.Serializable]
public class Quest
{
    public int Id;
    public string questName;
    public Quest(QuestObject quest)
    {
        questName = quest.questName;
        Id = quest.Id;

    }
}
