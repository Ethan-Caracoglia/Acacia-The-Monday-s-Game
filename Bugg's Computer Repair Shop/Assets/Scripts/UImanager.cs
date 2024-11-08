using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class UImanager : MonoBehaviour
{
    //three objectives
    bool caseRemoved = false;
    bool iceMelted = false;
    bool partsReassembled = false;

    public bool CaseRemoved
    {
        get
        {
            return caseRemoved;
        }
    }
    public bool IceMelted
    {
        get
        {
            return iceMelted;
        }
    }
    public bool PartsReassembled
    {
        get
        {
            return partsReassembled;
        }
    }

    //track which step you're on
    int objectiveNum = 0;

    //Get the UI spot for this text
    [SerializeField]
    private TextMeshProUGUI objectives;

    //get the Basic Win script to pull variables from
    [SerializeField]
    BasicWin b;

    //get the Score Manager script to pull the score list from
    [SerializeField]
    ScoreManager s;


    // Start is called before the first frame update
    void Start()
    {
        //fill text into the UI spot
        objectives.text = "Objectives:\r\nOpen Case\r\nMelt the Ice\r\nReassemble and Close";


    }

    // Update is called once per frame
    void Update()
    {
        //check if any objectives have been met

        //if the hinge isnt snapped in place, set caseRemoved to true
        if (!b.parts[0].snapped)
        {
            caseRemoved= true;
        }

        //if theres no ice remaining set iceMelted to true
        if (b.iceCount <= 0)
        {
            iceMelted = true;
        }

        //If the basic win condition is met, set partsReassembled to true
        if (b.won == true)
        {
            partsReassembled = true;
        }

        //Change the lines of text depending on if that objective is done or not. ObjectiveNum makes sure steps can only be completed one time and in order

        //Is the case open?
        if (caseRemoved == true && objectiveNum == 0 && s.ObjectivesScore[0] != -1)
        {
            //objectives.text = "Objectives:\r\nMelt the Ice\r\nReassemble and Close";
            // Placeholder text until Objective list UI is updated
            objectives.text = $"Objectives:\r\nOpen Case - Score:{s.ObjectivesScore[0]}\r\nMelt the Ice\r\nReassemble and Close";
            objectiveNum++;
        }

        //Is the ice melted?
        if (iceMelted == true && objectiveNum == 1 && s.ObjectivesScore[1] != -1)
        {
            //objectives.text = "Objectives:\r\nReassemble and Close";
            // Placeholder text until Objective list UI is updated
            objectives.text = $"Objectives:\r\nOpen Case - Score:{s.ObjectivesScore[0]}\r\nMelt the Ice - Score:{s.ObjectivesScore[1]}\r\nReassemble and Close";
            objectiveNum++;
        }

        //is the case Reassembled?
        if (partsReassembled == true && objectiveNum == 2 && s.ObjectivesScore[2] != -1)
        {
            //objectives.text = "Objectives complete!";
            // Placeholder text until Objective list UI is updated
            objectives.text = $"Objectives:\r\nOpen Case - Score:{s.ObjectivesScore[0]}\r\nMelt the Ice - Score:{s.ObjectivesScore[1]}\r\nReassemble and Close - Score:{s.ObjectivesScore[2]}";
            objectiveNum++;
        }
    }
}
