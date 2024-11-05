using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class UImanager : MonoBehaviour
{
    #region Fields
    #region private
    //three objectives
    private bool caseRemoved = false;
    private bool iceMelted = false;
    private bool partsReassembled = false;

    //track which step you're on
    private int objectiveNum = 0;

    //Get the UI spot for this text
    [SerializeField]
    private TextMeshProUGUI objectives;

    //get the Basic Win script to pull variables from
    [SerializeField]
    private BasicWin b;
    #endregion
    #endregion

    #region Methods
    #region private
    // Start is called before the first frame update
    private void Start()
    {
        //fill text into the UI spot
        objectives.text = "Objectives:\r\nOpen Case\r\nMelt the Ice\r\nReassemble and Close";


    }

    // Update is called once per frame
    private void Update()
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
        if (caseRemoved == true && objectiveNum == 0)
        {
            objectives.text = "Objectives:\r\nMelt the Ice\r\nReassemble and Close";
            objectiveNum++;
        }

        //Is the ice melted?
        if (iceMelted == true && objectiveNum == 1)
        {
            objectives.text = "Objectives:\r\nReassemble and Close";
            objectiveNum++;
        }

        //is the case Reassembled?
        if (partsReassembled == true && objectiveNum == 2)
        {
            objectives.text = "Objectives complete!";
            objectiveNum++;
        }
    }
    #endregion
    #endregion
}
