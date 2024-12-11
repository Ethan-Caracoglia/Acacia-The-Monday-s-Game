using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    UImanager iceObjectives;

    #region Future Code
    // Not needed until more than one minigame
    /*
    [SerializeField]
    int activeMinigame;
    */
    // 0 = ice;
    #endregion

    [SerializeField]
    int totalScore = 0;
    int calculateTotalScore = 0;
    int totalPossibleScore = 0;

    // Lists for objective/score management
    [SerializeField]
    List<int> objectivesScore;
    [SerializeField]
    List<bool> objectivesProgress;
    [SerializeField]
    List<int> objectivesDelay;

    [SerializeField]
    int objectivesAmount;

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
    
    public List<int> ObjectivesScore
    {
        get
        {
            return objectivesScore;
        }
    }



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
        #region Future Code
        // Not Needed until there is more than one minigame
        /*
        switch (activeMinigame)
        {
            case 0:
                #region Ice Minigame
                objectivesProgress[0] = iceObjectives.CaseRemoved;
                objectivesProgress[1] = iceObjectives.IceMelted;
                objectivesDelay[1] = 4;
                objectivesProgress[2] = iceObjectives.PartsReassembled;
                #endregion
                break;
        }
        */
        #endregion

        // Hard coded for Ice Minigame
        #region Ice Minigame
        objectivesProgress[0] = iceObjectives.CaseRemoved;
        objectivesProgress[1] = iceObjectives.IceMelted;
        objectivesDelay[1] = 6;
        objectivesProgress[2] = iceObjectives.PartsReassembled;
        #endregion

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
