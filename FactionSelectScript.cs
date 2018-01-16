using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FactionSelectScript : MonoBehaviour {

    public Dropdown uiDropdown;
    private string chosenValue;

    // Use this for initialization
    void Start () {
       
       //Set On First Time
       if (chosenValue == null)
       {
            updatePlayerFaction();
            Debug.Log("FactionSelect(Instructions_Start)" + chosenValue);
       }
	}
	
	// Update is called once per frame
	void Update () {

        if (chosenValue != null && !chosenValue.Equals(uiDropdown.captionText.text))
        {
            updatePlayerFaction();
            Debug.Log("FactionSelect(Instructions)" + chosenValue);
        }
	}

    private void updatePlayerFaction()
    {
        chosenValue = uiDropdown.captionText.text;
        PlayerPrefs.SetString("PlayerFaction", chosenValue);
    }
}