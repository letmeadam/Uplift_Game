using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeButtons : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GoToInstructionsScreen()
    {
        //Debug.Log("Going to Instructions Screen.");
        SceneManager.LoadScene("InstructionsBak");
    }

    public void GoToGamePlayScreen()
    {
        //Debug.Log("Going to Game Play Screen.");
        SceneManager.LoadScene("Story");
    }

    public void GoToStartGame()
    {
        //Debug.Log("Going to Playable Scene");
        SceneManager.LoadScene("Scene_1");
    }

    public void GoToMainMenu()
	{
		SceneManager.LoadScene("TitlePage");
	}
}
