using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    UImanager objectives;

    // Score for each objective
    /*
    [SerializeField]
    int caseRemovedScore = -1;
    [SerializeField]
    int iceMeltedScore = -1;
    [SerializeField]
    int partsReassembledScore = -1;
    */
    [SerializeField]
    int totalScore = 0;
    int calculateTotalScore = 0;
    int totalPossibleScore = 0;

    [SerializeField]
    List<int> objectivesScore;
    [SerializeField]
    List<bool> objectivesProgress;
    [SerializeField]
    List<int> objectivesDelay;
    [SerializeField]
    int objectivesAmount;


    /*
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
    */

    // Keeps track of time taken for an objectove
    [SerializeField]
    float timer = 0;

    // Tracks onjective progress
    [SerializeField]
    int currentObjective = 0;

    // Records if all objectives are done
    [SerializeField]
    bool objectivesDone;

    [SerializeField]
    private TextMeshProUGUI scoreDisplay;

    // Start is called before the first frame update
    void Start()
    {
        // Sets list to default data
        objectivesScore = new List<int>(objectivesAmount);
        for (int i = 0; i < objectivesAmount; i++)
        {
            objectivesProgress.Add(false);
        }
        for (int i = 0; i < objectivesAmount; i++)
        {
            objectivesScore.Add(-1);
        }
        for (int i = 0; i < objectivesAmount; i++)
        {
            objectivesDelay.Add(1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Hard coded for ice minigame
        objectivesProgress[0] = objectives.CaseRemoved;
        objectivesProgress[1] = objectives.IceMelted;
        objectivesDelay[1] = 4;
        objectivesProgress[2] = objectives.PartsReassembled;
        
        // Old Code
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
        
        // Runs for each objective
        if (currentObjective < objectivesAmount)
        {
            // Controls score update for current objective
            if (objectivesScore[currentObjective] == -1)
            {
                objectivesScore[currentObjective] = ScoreTimer(objectivesProgress[currentObjective], objectivesDelay[currentObjective]);
            }
            // Resets for next objective
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

        // Calculates and displays the total score
        if (objectivesDone == true)
        {
            if (calculateTotalScore < objectivesAmount)
            {
                totalScore = totalScore + objectivesScore[calculateTotalScore];
                calculateTotalScore++;
            }

            totalPossibleScore = 5 * objectivesAmount;
            scoreDisplay.text = $"Total Score = {totalScore}/{totalPossibleScore}";
        }
        if (objectivesDone == false)
        {
            totalPossibleScore = 5 * objectivesAmount;
            scoreDisplay.text = $"Total Score = ?/{totalPossibleScore}";
        }
    }

    // Calculates the score for an objective
    public int ScoreTimer(bool objectiveDone, int timeDelay)
    {
        int score = -1;
        // While an objective is not done, increases the timer
        if (objectiveDone == false)
        {
            timer = timer + Time.deltaTime;
        }
        // When an objectvie is done, converts time to score
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
