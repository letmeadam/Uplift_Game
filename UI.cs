using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public GameObject statusCanvas;
    public Text status;
    public Text foodStatus;
    public Text primarySelect;
    public Text secondarySelect;

    public Button UpgradeButton;
    public Button CreatePawnButton;
    public Button BurrowButton;
    public Button UnBurrowButton;

    // INHERITED
    private void Start()
    {
        // Display Selection Text only if Object Currently Selected
        primarySelect.text = "";
        secondarySelect.text = "";
        //foodStatus.text = "FOOD: "
    }

    public void showStatus(Unit u)
    {
        string statusStr;
        statusStr = "Status " +
        "\n Hitpoints: " + u.Hitpoints +
            "\n Attack: " + u.Attack +
            "\n Defense: " + u.Defense;

        if (u.IsPlayerUnit)
        {
            statusStr += "\n Speed: " + u.Speed +
            "\n Upkeep: " + u.Upkeep +
            "\n Movement:: " + u.RemainingMovement + "/" + u.Speed;
        }

        showStatus(statusStr);
    }
    
    public void showStatus(UnitStats s, bool playerUnit)
    {
        ResourceManager r = GameObject.Find("GameManager").GetComponent<ResourceManager>();
        string statusStr = "";

        statusStr = "Status " +
        "\n Hitpoints: " + s.hitpoints +
            "\n Attack: " + s.attack +
            "\n Defense: " + s.defense;

        if (playerUnit)
        {
            statusStr += "\n Speed: " + s.speed +
            "\n Upkeep: " + s.upkeep +
            "\n Movement:: " + (s.speed - s.tilesMoved) + "/" + s.speed;
        }

        showStatus(statusStr);
    }

    public void showStatus(string str)
    {
		//ResourceManager r = GameObject.Find("GameManager").GetComponent<GameManager>().rManager;
		//string s = "FOOD:\t" + r.getFactionResources(r.PlayerFaction) + "\n\n";
        //status.text = s + str;
		status.text = str;
        showStatus();
    }

    public void showStatus()
    {
        statusCanvas.SetActive(true);
    }

    public void hideStatus()
    {
        statusCanvas.SetActive(false);
    }

    public void setPrimary(string s)
    {
		//Debug.Log ("setPrimary called with: " + s);
        if (s.Length > 0)
        {
            primarySelect.text = "Primary:" + s;
			//Debug.Log ("setPrimary primarySelect.text: " + primarySelect.text);
        }
        else
        {
            primarySelect.text = "";
        }
    }

    public void removePrimary()
    {
        primarySelect.text = "";
    }

    public void setSecondary(string s)
    {
        if (s.Length > 0)
        {
            secondarySelect.text = "Secondary:" + s;
        }
        else
        {
            secondarySelect.text = "";
        }
    }

    public void removeSecondary()
    {
        secondarySelect.text = "";
    }

    public void setFoodStatus(ResourceManager rManager)
    {
        string str = ResourceManager.PlayerFood + "\n" + ResourceManager.PlayerUpkeep;
        foodStatus.text = str;
    }

    public void updateButtons(Unit primary)
    {
        if (primary == null)
        {
            disableButton(UpgradeButton);
            disableButton(CreatePawnButton);
            disableButton(BurrowButton);
            disableButton(UnBurrowButton);
        }
        else if (primary is Alpha)
        {
            disableButton(UpgradeButton);

            if (((Alpha)primary).Burrowed == true)
            {
                enableButton(CreatePawnButton);
                disableButton(BurrowButton);
                enableButton(UnBurrowButton);
            }
            else
            {
                disableButton(CreatePawnButton);
                enableButton(BurrowButton);
                disableButton(UnBurrowButton);
            }
        }
        else if (primary is Pawn)
        {
            disableButton(CreatePawnButton);
            disableButton(BurrowButton);
            disableButton(UnBurrowButton);
            if (((Pawn)primary).CanUpgrade == true)
                enableButton(UpgradeButton);
            else
                disableButton(UpgradeButton);
        }
    }

    private void enableButton(Button button)
    {
        //button.gameObject.SetActive(true);
        button.enabled = true;
    }

    private void disableButton(Button button)
    {
        //button.gameObject.SetActive(false);
        button.enabled = false;
    }

    /*
    // TODO:
    // Bug: The Selection Click Should Check if "Second" click is a different sprite, and then re-populate
    //      or if it's the same sprite, and then toggle-off.
    public void defaultLeftClick()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("Base Class: Left click, item-select.");
            setPrimary(this.ToString());

            //TODO: Check if Tile, FactionEntity, Alpha, NotAlpha, ClickableItem

            //TODO: Not necessary if each sub-class: Tile, Alpha, and Pawn all independently overwrite
            //      this method and handle it as appropriate.
        }
    }

    // TODO:
    // Bug: The Selection Click Should Check if "Second" click is a different sprite, and then re-populate
    //      or if it's the same sprite, and then toggle-off.
    public void defaultRightClick()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Base Class: Right click, action-select.");
            setSecondary(this.ToString());
        }
    }
    */
}
