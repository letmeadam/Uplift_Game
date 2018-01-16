using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * TO BE USED BY ANY BUTTONS THAT WANT TO TOGGLE THE INSTRUCTIONS PAGE
 */
public class InstructionToggler : MonoBehaviour {

    public GameObject instructionObject;

	// Use this for initialization
	void Start () {
        // Default, not active (just a precaustion. Shouldn't matter.)
        instructionObject.SetActive(false);
    }

    public void ToggleInstructionsOn()
    {
        instructionObject.SetActive(true);
    }

    public void ToggleInstructionsOff()
    {
        instructionObject.SetActive(false);
    }
}
