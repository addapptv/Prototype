using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestUIManager : MonoBehaviour
{
    //Change Quest list to show active quest plus latest quest step


    public GameObject questStatusUI;
    public GameObject questStepUI;
    public GameObject questListUI;

    private List<string> activeQuests = new List<string>();

    private void OnEnable()
    {
        GameEventsManager.instance.questEvents.OnStartQuest += StartQuestUI;
        GameEventsManager.instance.questEvents.OnAdvanceQuest += DisplayAdvanceQuestUI;
        GameEventsManager.instance.questEvents.OnFinishQuest += FinishQuestUI;

    }

    private void OnDisable()
    {
        GameEventsManager.instance.questEvents.OnStartQuest -= StartQuestUI;
        GameEventsManager.instance.questEvents.OnAdvanceQuest -= DisplayAdvanceQuestUI;
        GameEventsManager.instance.questEvents.OnFinishQuest -= FinishQuestUI;
        
    }

    private void StartQuestUI(string id, Quest quest)
    {
        StartCoroutine(QuestStartUI(quest));
        activeQuests.Add(quest.info.displayName);
        UpdateQuestList();
    }

    private void DisplayAdvanceQuestUI(string id, QuestStep questStep)
    {
        StartCoroutine(QuestAdvanceUI(questStep));
        UpdateQuestList();
    }
        
    private void FinishQuestUI(string id, Quest quest)
    {
        StartCoroutine(QuestFinishUI(quest));
        activeQuests.Remove(quest.info.displayName);
        UpdateQuestList();
    }

    private void UpdateQuestList()
    {
        questListUI.GetComponent<TextMeshProUGUI>().text = "";

        foreach (string quest in activeQuests)
        {
            questListUI.GetComponent<TextMeshProUGUI>().text += quest + "\n";
        }
    }


    IEnumerator QuestStartUI(Quest quest)
    {
        string questName = quest.info.displayName;
        string firstObjective = quest.info.firstObjective;
        questStatusUI.GetComponent<TextMeshProUGUI>().text = questName;
        questStepUI.GetComponent<TextMeshProUGUI>().text = firstObjective;
        questStatusUI.SetActive(true);
        questStepUI.GetComponent<TextMeshProUGUI>().CrossFadeAlpha(1, 0.2f, true);
        yield return new WaitForSeconds(1);
        questStatusUI.GetComponent<TextMeshProUGUI>().CrossFadeAlpha(0, 1.5f, true);
        yield return new WaitForSeconds(2);
        questStatusUI.SetActive(false);
        questStepUI.SetActive(true);
        questStepUI.GetComponent<TextMeshProUGUI>().CrossFadeAlpha(1, 0.2f, true);
        yield return new WaitForSeconds(1);
        questStepUI.GetComponent<TextMeshProUGUI>().CrossFadeAlpha(0, 1.5f, true);
        yield return new WaitForSeconds(1.5f);
        questStepUI.SetActive(false);
    }

    IEnumerator QuestCollectableUI(string itemType, int itemsCollected, int itemsRequired)
    {
        questStepUI.GetComponent<TextMeshProUGUI>().text = itemsCollected + "/" + itemsRequired + " " + itemType + " collected";
        questStepUI.SetActive(true);
        questStepUI.GetComponent<TextMeshProUGUI>().CrossFadeAlpha(1, 0.2f, true);
        yield return new WaitForSeconds(1);
        questStepUI.GetComponent<TextMeshProUGUI>().CrossFadeAlpha(0, 1.5f, true);
        yield return new WaitForSeconds(1.5f);
        questStepUI.SetActive(false);
    }

    IEnumerator QuestAdvanceUI(QuestStep questStep)
    {
        string questStepComplete= questStep.info.completeMessage;
        string nextObjective = questStep.info.nextObjective;
        if(questStepComplete != "")
        {
            questStepUI.GetComponent<TextMeshProUGUI>().text = questStepComplete;
            questStepUI.SetActive(true);
            questStepUI.GetComponent<TextMeshProUGUI>().CrossFadeAlpha(1, 0.2f, true);
            yield return new WaitForSeconds(1);
            questStepUI.GetComponent<TextMeshProUGUI>().CrossFadeAlpha(0, 1.5f, true);
            yield return new WaitForSeconds(2);
            questStepUI.SetActive(false);
            questStepUI.GetComponent<TextMeshProUGUI>().text = nextObjective;
            questStepUI.SetActive(true);
            questStepUI.GetComponent<TextMeshProUGUI>().CrossFadeAlpha(1, 0.2f, true);
            yield return new WaitForSeconds(1);
            questStepUI.GetComponent<TextMeshProUGUI>().CrossFadeAlpha(0, 1.5f, true);
            yield return new WaitForSeconds(1.5f);
            questStepUI.SetActive(false);
        }
        else
        {
            questStepUI.GetComponent<TextMeshProUGUI>().text = nextObjective;
            questStepUI.SetActive(true);
            questStepUI.GetComponent<TextMeshProUGUI>().CrossFadeAlpha(1, 0.2f, true);
            yield return new WaitForSeconds(1);
            questStepUI.GetComponent<TextMeshProUGUI>().CrossFadeAlpha(0, 1.5f, true);
            yield return new WaitForSeconds(1.5f);
            questStepUI.SetActive(false);
        }
    }

    IEnumerator QuestFinishUI(Quest quest)
    {
        string questName = quest.info.displayName;
        questStatusUI.GetComponent<TextMeshProUGUI>().text = questName + " completed";
        yield return new WaitForSeconds(2.5f);
        questStatusUI.SetActive(true);
        questStatusUI.GetComponent<TextMeshProUGUI>().CrossFadeAlpha(1, 0.2f, true);
        yield return new WaitForSeconds(1);
        questStatusUI.GetComponent<TextMeshProUGUI>().CrossFadeAlpha(0, 1.5f, true);
        yield return new WaitForSeconds(2);
        questStatusUI.SetActive(false);
    }

}
