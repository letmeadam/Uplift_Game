using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//TODO: Need to obtain TurnStatus programmatically...

public class EndTurnScript : MonoBehaviour {

    public Text turnStatus;

    private int numTurns;

    private void Start()
    {
        numTurns = 0;

        turnStatus.text = "Turn: " + numTurns;
    }

    public void OnEndTurn()
    {
        numTurns++;
     
        //Do End Turn Logic
        turnStatus.text = "Day: " + numTurns;
        GameManager g = GameObject.Find("GameManager").GetComponent<GameManager>();
        g.endTurn();
    }
}
