using System;

public class QuestEvents
{
    public event Action<string, Quest> OnStartQuest;
    public void StartQuest(string id, Quest quest)
    {
        if (OnStartQuest != null)
        {
            OnStartQuest(id, quest);
        }
    }

    public event Action<string, QuestStep> OnAdvanceQuest;
    public void AdvanceQuest(string id, QuestStep questStep)
    {
        if (OnAdvanceQuest != null)
        {
            OnAdvanceQuest(id, questStep);
        }
    }

    public event Action<string, Quest> OnFinishQuest;
    public void FinishQuest(string id, Quest quest)
    {
        if (OnFinishQuest != null)
        {
            OnFinishQuest(id, quest);
        }
    }

    public event Action<Quest> OnQuestStateChange;
    public void QuestStateChange(Quest quest)
    {
        if (OnQuestStateChange != null)
        {
            OnQuestStateChange(quest);
        }
    }

    public event Action<string, int, QuestStepState> OnQuestStepStateChange;
    public void QuestStepStateChange(string id, int stepIndex, QuestStepState questStepState)
    {
        if (OnQuestStepStateChange != null)
        {
            OnQuestStepStateChange(id, stepIndex, questStepState);
        }
    }

    public event Action<float, float, float> OnQuestReward;
    public void QuestReward(float money, float experience, float progress)
    {
        if (OnQuestReward != null)
        {
            OnQuestReward(money, experience, progress);
        }
    }

}