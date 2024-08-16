using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InteractableController))]
public class QuestStartPoint : MonoBehaviour
{
    //INTERACTABLE IS LINKED TO CURRENT QUEST STATE TO SEE IF IT IS DISPLAYED AND/OR INTERACTABLE
    //CHANGE WAY TO START QUESTS - INTERACTABLE WITH INTERACT TRIGGER OR INSPECTING/COLLECTING AN ITEM//
    //ADJUST SO YOU CAN ONLY INTERACT IF QUEST STEPS ARE COMPLETE? OR GENERATE DAILOGUE SAYING STEPS NOT COMPLETE?//

    [Header("Quest")]
    [SerializeField] private QuestObject questInfoForPoint;

    private string questId;
    [HideInInspector]
    public string questPrompt;
    private Quest currentQuest;
    private QuestState currentQuestState;
    InteractableController interactableController;

/*    private GameObject questIcon;*/

    private void Awake()
    {
        questId = questInfoForPoint.id;
        questPrompt = questInfoForPoint.startQuestPrompt;
/*        questIcon = GetComponentInChildren<QuestIcon>();*/
        interactableController = GetComponent<InteractableController>();
        interactableController.prompt = questPrompt;
    }

    private void Start()
    {
        //NOT WORKING CODE TO TEST QUEST STATE AND DETERMINE IF INTERACTABLE
        
        /*currentQuest = QuestManager.instance.GetQuestById(questInfoForPoint.id);

        Debug.Log(currentQuest.info.name);

        currentQuestState = currentQuest.state;
        if (currentQuestState == QuestState.CAN_START)
        {
            interactableController._detectable = true;
        }
        else
        {
            interactableController._detectable = false;                                                                         
        }*/
    }

    private void OnEnable()
    {
        GameEventsManager.instance.questEvents.OnQuestStateChange += QuestStateChange;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.questEvents.OnQuestStateChange -= QuestStateChange;
    }


    private void QuestStateChange(Quest quest)
    {
        // only update the quest state if this point has the corresponding quest
        if (quest.info.id.Equals(questId))
        {
            currentQuest = quest;
            currentQuestState = quest.state;
            /*            questIcon.SetState(currentQuestState, startPoint, finishPoint);*/
        }

    }

    public void QuestPointInteract()
    {

        if (currentQuestState.Equals(QuestState.CAN_START))
        {
            GameEventsManager.instance.questEvents.StartQuest(questId, currentQuest);        
        }
        else return;

    }

}