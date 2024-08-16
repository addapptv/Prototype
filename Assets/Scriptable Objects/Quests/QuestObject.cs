using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Quest Object", menuName = "Quest System/Quest", order = 1)]

public class QuestObject : ScriptableObject
{
    [field: SerializeField] public string id { get; private set; }

    [Header("Info")]

    public string displayName;
    public Sprite questIcon;
    [TextArea(5, 10)]
    public string questDescription;
    [TextArea(5, 10)]
    public string startQuestPrompt;
    [TextArea(5, 10)]
    public string firstObjective;

    [Header("Requirements")]
    public QuestObject[] questPrerequisites;

    [Header("Steps")]
    public GameObject[] questStepPrefabs;

    [Header("Rewards")]

    public float progressValue;
    public float xpValue;
    public float moneyValue;

    // ensure the id is always the name of the Scriptable Object asset
    private void OnValidate()
    {
        #if UNITY_EDITOR
        id = this.name;
        UnityEditor.EditorUtility.SetDirty(this);
        #endif
    }

}



