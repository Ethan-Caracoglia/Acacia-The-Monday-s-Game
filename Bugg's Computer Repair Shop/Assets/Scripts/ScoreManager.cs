using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // Objectives
    [SerializeField]
    bool caseRemoved = false;
    [SerializeField]
    bool iceMelted = false;
    [SerializeField]
    bool partsReassembled = false;

    [SerializeField]
    UImanager objectives;

    [SerializeField]
    int caseRemovedScore = -1;
    [SerializeField]
    int iceMeltedScore = -1;
    [SerializeField]
    int partsReassembledScore = -1;

    [SerializeField]
    float timer = 0;
    int currentObjective = 0;

    int caseRemovedDelay;
    int iceMeltedDelay;
    int partsReassembledDelay;

    // Start is called before the first frame update
    void Start()
    {
        caseRemovedDelay = 1;
        iceMeltedDelay = 2;
        partsReassembledDelay = 1;
    }

    // Update is called once per frame
    void Update()
    {
        caseRemoved = objectives.CaseRemoved;
        iceMelted = objectives.IceMelted;
        partsReassembled = objectives.PartsReassembled;
        if (currentObjective == 0)
        {
            if (caseRemovedScore == -1)
            {
                caseRemovedScore = ScoreTimer(caseRemoved, caseRemovedDelay);
            }
            else
            {
                timer = 0;
                currentObjective = 1;
            }
        }
        if (currentObjective == 1)
        {
            if (iceMeltedScore == -1)
            {
                iceMeltedScore = ScoreTimer(iceMelted, iceMeltedDelay);
            }
            else
            {
                timer = 0;
                currentObjective = 2;
            }
        }
        if (currentObjective == 2)
        {
            if (partsReassembledScore == -1)
            {
                partsReassembledScore = ScoreTimer(partsReassembled, partsReassembledDelay);
            }
            else
            {
                timer = 0;
                currentObjective = 3;
            }
        }
    }

    public int ScoreTimer(bool objectiveDone, int timeDelay)
    {
        int score = -1;
        if (objectiveDone == false)
        {
            timer = timer + Time.deltaTime;
        }
        if (objectiveDone == true)
        {
            if (timer <= (1*timeDelay))
            {
                score = 5;
            }
            else if (timer > (1 * timeDelay) && timer <= (2 * timeDelay))
            {
                score = 4;
            }
            else if (timer > (2 * timeDelay) && timer <= (3 * timeDelay))
            {
                score = 3;
            }
            else if (timer > (3 * timeDelay) && timer <= (4 * timeDelay))
            {
                score = 2;
            }
            else if (timer > (41 * timeDelay) && timer <= (5 * timeDelay))
            {
                score = 1;
            }
            else if (timer > (5 * timeDelay))
            {
                score = 0;
            }
        }
        return score;
    }
}
