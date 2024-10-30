using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    UImanager objectives;

    // Score for each objective
    [SerializeField]
    int caseRemovedScore = -1;
    [SerializeField]
    int iceMeltedScore = -1;
    [SerializeField]
    int partsReassembledScore = -1;
    [SerializeField]
    int totalScore = 0;
    int totalPossibleScore = 0;

    [SerializeField]
    List<int> objectivesScore;
    [SerializeField]
    List<bool> objectivesProgress;
    [SerializeField]
    int objectivesAmount;



    public int CaseRemovedScore
    {
        get
        {
            return caseRemovedScore;
        }
    }
    public int IceMeltedScore
    {
        get
        {
            return caseRemovedScore;
        }
    }
    public int PartsReassembledScore
    {
        get
        {
            return caseRemovedScore;
        }
    }

    // Keeps track of time taken for an objectove
    [SerializeField]
    float timer = 0;

    // Tracks onjective progress
    [SerializeField]
    int currentObjective = 0;

    // Records if all objectives are done
    [SerializeField]
    bool objectivesDone;

    // Delay to increase time conversion for score
    int caseRemovedDelay;
    int iceMeltedDelay;
    int partsReassembledDelay;

    [SerializeField]
    private TextMeshProUGUI scoreDisplay;

    // Start is called before the first frame update
    void Start()
    {
        caseRemovedDelay = 1;
        iceMeltedDelay = 4;
        partsReassembledDelay = 1;
        objectivesScore = new List<int>(objectivesAmount);
        for (int i = 0; i < objectivesAmount; i++)
        {
            objectivesProgress.Add(false);
        }
        for (int i = 0; i < objectivesAmount; i++)
        {
            objectivesScore.Add(-1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        objectivesProgress[0] = objectives.CaseRemoved;
        objectivesProgress[1] = objectives.IceMelted;
        objectivesProgress[2] = objectives.PartsReassembled;
        /*
        if (currentObjective == 0)
        {
            if (caseRemovedScore == -1)
            {
                caseRemovedScore = ScoreTimer(objectivesProgress[0], caseRemovedDelay);
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
                iceMeltedScore = ScoreTimer(objectivesProgress[1], iceMeltedDelay);
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
                partsReassembledScore = ScoreTimer(objectivesProgress[2], partsReassembledDelay);
            }
            else
            {
                timer = 0;
                currentObjective = 3;
                objectivesDone = true;
            }
        }
        */
        if (currentObjective < objectivesAmount)
        {
            if (objectivesScore[currentObjective] == -1)
            {
                objectivesScore[currentObjective] = ScoreTimer(objectivesProgress[2], 1);
            }
            else
            {
                timer = 0;
                currentObjective++;
            }
        }
        else
        {
            objectivesDone = true;
        }


        if (objectivesDone == true)
        {
            totalScore = caseRemovedScore + iceMeltedScore + partsReassembledScore;
            totalPossibleScore = 5 * objectivesAmount;
            scoreDisplay.text = $"Total Score = {totalScore}/{totalPossibleScore}";
        }
        if (objectivesDone == false)
        {
            totalPossibleScore = 5 * objectivesAmount;
            scoreDisplay.text = $"Total Score = ?/{totalPossibleScore}";
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
            if (timer <= (2*timeDelay))
            {
                score = 5;
            }
            else if (timer > (2 * timeDelay) && timer <= (3 * timeDelay))
            {
                score = 4;
            }
            else if (timer > (3 * timeDelay) && timer <= (4 * timeDelay))
            {
                score = 3;
            }
            else if (timer > (4 * timeDelay) && timer <= (5 * timeDelay))
            {
                score = 2;
            }
            else if (timer > (5 * timeDelay) && timer <= (6 * timeDelay))
            {
                score = 1;
            }
            else if (timer > (6 * timeDelay))
            {
                score = 0;
            }
        }
        return score;
    }
}
