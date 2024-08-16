using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class ReachQuestStep : QuestStep
{
    private void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
            FinishQuestStep();
        }
    }

    protected override void SetQuestStepState(string state)
    {

    }
}