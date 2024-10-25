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
    int caseRemovedScore;
    int iceMeltedScore;
    int partsReassembledScore;

    [SerializeField]
    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        caseRemoved = objectives.CaseRemoved;
        iceMelted = objectives.IceMelted;
        partsReassembled = objectives.PartsReassembled;
        caseRemovedScore = ScoreTimer(caseRemoved);
    }

    public int ScoreTimer(bool objectiveDone)
    {
        int score = -1;
        if (objectiveDone == false)
        {
            timer = timer + Time.deltaTime;
        }
        if (objectiveDone == true)
        {
            if (timer <= 1)
            {
                score = 5;
            }
            else if (timer > 1 && timer <= 2)
            {
                score = 4;
            }
            else if (timer > 2 && timer <= 3)
            {
                score = 3;
            }
            else if (timer > 3 && timer <= 4)
            {
                score = 2;
            }
            else if (timer > 4 && timer <= 5)
            {
                score = 1;
            }
            else if (timer > 5)
            {
                score = 0;
            }
        }
        return score;
    }
}
