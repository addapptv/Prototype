using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour, IDataPersistence
{
    //SAVING TO PLAYERPREFS
    //PLAYER LEVEL PREREQUISITES DISABLED
    //IN PROGRESS - CHANGES TO INTEGRATE WITH IDATAPERSISTENCE SAVE SYSTEM SAVING AND LOADING BUT QUEST STEPS NOT INSTANTIATING PROPERLY
        //CAN GET THEM TO INSTANTIATE BUT WITH ERRORS WITH MULTIPLE QUESTS IN PROGRESS, THEN THEY DON'T PROGRESS TO NEXT STEP

    public static QuestManager instance { get; private set; }

    [Header("Config")]
    [SerializeField] private bool loadQuestState = true;

    private Dictionary<string, Quest> questMap;

    // quest start requirements
    /*    private int currentPlayerLevel;*/

    private void Awake()
    {
        questMap = CreateQuestMap();
    }

    private void OnEnable()
    {
        GameEventsManager.instance.questEvents.OnStartQuest += StartQuest;
        GameEventsManager.instance.questEvents.OnAdvanceQuest += AdvanceQuest;
        GameEventsManager.instance.questEvents.OnFinishQuest += FinishQuest;

        GameEventsManager.instance.questEvents.OnQuestStepStateChange += QuestStepStateChange;


        /*        GameEventsManager.instance.playerEvents.onPlayerLevelChange += PlayerLevelChange;*/
    }

    private void OnDisable()
    {
        GameEventsManager.instance.questEvents.OnStartQuest -= StartQuest;
        GameEventsManager.instance.questEvents.OnAdvanceQuest -= AdvanceQuest;
        GameEventsManager.instance.questEvents.OnFinishQuest -= FinishQuest;

        GameEventsManager.instance.questEvents.OnQuestStepStateChange -= QuestStepStateChange;

/*        GameEventsManager.instance.playerEvents.onPlayerLevelChange -= PlayerLevelChange;*/
    }

    private void Start()
    {
        //TREVER MOCK ORIGINAL

        foreach (Quest quest in questMap.Values)
        {
            // initialize any loaded quest steps
            if (quest.state == QuestState.IN_PROGRESS)
            {
                quest.InstantiateCurrentQuestStep(this.transform);
            }
            // broadcast the initial state of all quests on startup
            GameEventsManager.instance.questEvents.QuestStateChange(quest);
        }

        /*//IN PROGRESS CUSTOM CODE

        //Broadcast the initial state of all quests on startup

        foreach (Quest quest in questMap.Values)
        {
            GameEventsManager.instance.questEvents.QuestStateChange(quest);
        }*/

    }

    private void ChangeQuestState(string id, QuestState state)
    {
        Quest quest = GetQuestById(id);
        quest.state = state;
        GameEventsManager.instance.questEvents.QuestStateChange(quest);
    }

    /*    private void PlayerLevelChange(int level)
        {
            currentPlayerLevel = level;
        }*/

    private bool CheckRequirementsMet(Quest quest)
    {
        // start true and prove to be false
        bool meetsRequirements = true;

/*        // check player level requirements
        if (currentPlayerLevel < quest.info.levelRequirement)
        {
            meetsRequirements = false;
        }*/

        // check quest prerequisites for completion
        foreach (QuestObject prerequisiteQuestInfo in quest.info.questPrerequisites)
        {
            if (GetQuestById(prerequisiteQuestInfo.id).state != QuestState.FINISHED)
            {
                meetsRequirements = false;
            }
        }

        return meetsRequirements;

    }

    private void Update()
    {
        // loop through ALL quests
        foreach (Quest quest in questMap.Values)
        {
            // if we're now meeting the requirements, switch over to the CAN_START state
            if (quest.state == QuestState.REQUIREMENTS_NOT_MET && CheckRequirementsMet(quest))
            {
                ChangeQuestState(quest.info.id, QuestState.CAN_START);
            }
        }
    }

    private void StartQuest(string id, Quest quest)
    {
        quest = GetQuestById(id);
        Debug.Log(quest.info.displayName + " quest started");
        quest.InstantiateCurrentQuestStep(this.transform);

        ChangeQuestState(quest.info.id, QuestState.IN_PROGRESS);
    }

    private void AdvanceQuest(string id, QuestStep questStep)
    {
        Quest quest = GetQuestById(id);

        // move on to the next step
        quest.MoveToNextStep();

        // if there are more steps, instantiate the next one
        if (quest.CurrentStepExists())
        {
            quest.InstantiateCurrentQuestStep(this.transform);
        }
        // if there are no more steps, then we've finished all of them for this quest, finish quest
        else
        {
            GameEventsManager.instance.questEvents.FinishQuest(id, quest);
        }
    }

    private void FinishQuest(string id, Quest quest)
    {
        quest = GetQuestById(id);
        ClaimRewards(quest);
        Debug.Log(quest.info.displayName + " quest finished");
        ChangeQuestState(quest.info.id, QuestState.FINISHED);
    }

    private void ClaimRewards(Quest quest)
    {
        float money = quest.info.moneyValue;
        float experience = quest.info.xpValue;
        float progress = quest.info.progressValue;
        GameEventsManager.instance.questEvents.QuestReward(money, experience, progress);
    }

    private void QuestStepStateChange(string id, int stepIndex, QuestStepState questStepState)
    {
        Quest quest = GetQuestById(id);
        quest.StoreQuestStepState(questStepState, stepIndex);
        ChangeQuestState(id, quest.state);
    }

    private Dictionary<string, Quest> CreateQuestMap()
    {
        // loads all QuestInfoSO Scriptable Objects under the Assets/Resources/Quests folder
        QuestObject[] allQuests = Resources.LoadAll<QuestObject>("Quests");
        
        // Create the quest map
        Dictionary<string, Quest> idToQuestMap = new Dictionary<string, Quest>();
        foreach (QuestObject questInfo in allQuests)
        {
            if (idToQuestMap.ContainsKey(questInfo.id))
            {
                Debug.LogWarning("Duplicate ID found when creating quest map: " + questInfo.id);
            }
            idToQuestMap.Add(questInfo.id, LoadQuest(questInfo));
        }
        return idToQuestMap;
    }

    public Quest GetQuestById(string id)
    {
        Quest quest = questMap[id];
        if (quest == null)
        {
            Debug.LogError("ID not found in the Quest Map: " + id);
        }
        return quest;
    }

    //TREVER MOCK ORIGINAL SAVEQUEST CODE - SAVES TO PLAYER PREFS

    private void SaveQuest(Quest quest)
    {
        try
        {
            QuestData questData = quest.GetQuestData();
            // serialize using JsonUtility, but use whatever you want here (like JSON.NET)
            string serializedData = JsonUtility.ToJson(questData);
            // saving to PlayerPrefs is just a quick example for this tutorial video,
            // you probably don't want to save this info there long-term.
            // instead, use an actual Save & Load system and write to a file, the cloud, etc..
            PlayerPrefs.SetString(quest.info.id, serializedData);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to save quest with id " + quest.info.id + ": " + e);
        }
    }

    //TREVER MOCK ORIGINAL LOADQUEST CODE - LOADS FROM PLAYER PREFS
    private Quest LoadQuest(QuestObject questInfo)
    {
        //TREVER MOCK ORIGINAL

        Quest quest = null;
        try
        {
            // load quest from saved data
            if (PlayerPrefs.HasKey(questInfo.id) && loadQuestState)
            {
                string serializedData = PlayerPrefs.GetString(questInfo.id);
                QuestData questData = JsonUtility.FromJson<QuestData>(serializedData);
                quest = new Quest(questInfo, questData.state, questData.questStepIndex, questData.questStepStates);
            }
            // otherwise, initialize a new quest
            else
            {
                quest = new Quest(questInfo);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to load quest with id " + quest.info.id + ": " + e);
        }
        return quest;

        //IN PROGRESS INTEGRSATIOJN WITH IDATAPERSISTENCE

        /*  Quest quest = null;
          GameData data = new GameData();

          try
          {
              if (data.questSaveData.ContainsKey(questInfo.id) && loadQuestState)
              {
                  questMap.Remove(questInfo.id);
                  QuestData questData = data.questSaveData[questInfo.id];
                  quest = new Quest(questInfo, questData.state, questData.questStepIndex, questData.questStepStates);
                  questMap.Add(questInfo.id, quest);

                  GameEventsManager.instance.questEvents.QuestStateChange(quest);
              }
              else
              {
                  quest = new Quest(questInfo);

                  GameEventsManager.instance.questEvents.QuestStateChange(quest);
              }

          }
          catch (System.Exception e)
          {
              Debug.LogError("Failed to load quest with id " + quest.info.id + ": " + e);
          }

          return quest;
  */
    }
    
    public void LoadData(GameData data)
    {
/*        // Load all QuestObject Scriptable Objects under the Assets/Resources/Quests folder

                        QuestObject[] allQuests = Resources.LoadAll<QuestObject>("Quests");
                        Quest quest = null;

                        foreach (QuestObject questInfo in allQuests)
                        {
                            if (data.questSaveData.ContainsKey(questInfo.id) && loadQuestState)
                            {
                                questMap.Remove(questInfo.id);
                                QuestData questData = data.questSaveData[questInfo.id];
                                quest = new Quest(questInfo, questData.state, questData.questStepIndex, questData.questStepStates);
                                questMap.Add(questInfo.id, quest);

                                GameEventsManager.instance.questEvents.QuestStateChange(quest);
                            }
                            else
                            {
                                quest = new Quest(questInfo);

                                GameEventsManager.instance.questEvents.QuestStateChange(quest);
                            }


                        }*//*

                //Update state for each quest and instantiate quest steps//

                foreach (Quest quest in questMap.Values)
                {
                    // initialize any loaded quest steps
                    if (quest.state == QuestState.IN_PROGRESS)
                    {
                        quest.InstantiateCurrentQuestStep(this.transform);
                    }
                    // broadcast the initial state of all quests on startup
                    GameEventsManager.instance.questEvents.QuestStateChange(quest);
                }*/

        //YOU TUBE COMMENTS CODE - NEED TO INTEGRATE
        /*
        QuestObject[] allQuests = Resources.LoadAll<QuestObject>("Quests");
        Quest quest = null;

        foreach (QuestObject questInfo in allQuests)
        {
            if (data.questSaveData.ContainsKey(questInfo.id) && loadQuestState)
            {
                QuestData questData = data.questSaveData[questInfo.id];

                quest = new Quest(questInfo, questData.state, questData.questStepIndex, questData.questStepStates);
                Debug.Log(questData.questStepStates[0]);
            }
            else
            {
                quest = new Quest(questInfo);
            }
        }
        */
        
        //MOVED TREVER MOCK START CODE HERE AFTER LOAD
        /*        
        foreach (Quest quest in questMap.Values)
        {
            // initialize any loaded quest steps
            if (quest.state == QuestState.IN_PROGRESS)
            {
                quest.InstantiateCurrentQuestStep(this.transform);
            }
            // broadcast the initial state of all quests on startup
            GameEventsManager.instance.questEvents.QuestStateChange(quest);
        }
        */

    }

    public void SaveData(GameData data)
    {
        //TREVER MOCK SCRIPT ORIGINALLY CALLED ON APPLICATION QUIT MOVED TO SAVE ON EVENT
        
        foreach (Quest quest in questMap.Values)
        {
            SaveQuest(quest);
        }

/*            //WORKING SAVE DATA SCRIPT

            //Loop through the existing quest map to access each quests Unique ID
            foreach (Quest quest in questMap.Values)
            {
                //Get the Quests data
                QuestData questData = quest.GetQuestData();

                if (data.questSaveData.ContainsKey(quest.info.id))
                {
                    data.questSaveData.Remove(quest.info.id);
                }

                data.questSaveData.Add(quest.info.id, questData);
            }*/
        
    }
}