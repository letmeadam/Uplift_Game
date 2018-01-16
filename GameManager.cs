using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public MapManager mManager;
    public UnitManager uManager;
    public ResourceManager rManager;
    public UI ui;

    public Faction playerFaction;

    public Unit primaryUnit;
    public Unit secondaryUnit;

    private int turnCount = 0;
    private int maxTurns = 50;
    
    // Use this for initialization
    void Start () {
        mManager = GameObject.Find("MapObject").GetComponent<MapManager>();
        rManager.init();
        uManager.init();
        //Vector2 startPos = Map.vectorize(11, 11);
        //Alpha a = (Alpha)uManager.addUnit(new AlphaStats(startPos, startPos), grid, UnitType.Alpha, true, 11, 11);
        //a.isPlayerUnit = true;
        //rManager.currentFood = 100;
        //Vector2 testPos = Map.vectorize(11, 14);
        //Pawn p = (Pawn)uManager.addUnit(new PawnStats(testPos, testPos), grid, UnitType.Pawn, false, 11, 14);

        //Vector2 testPos = Map.vectorize(22, 19);
        //Pawn p = (Pawn)uManager.addUnit(new PawnStats(testPos, testPos), grid, UnitType.Pawn, false, 11, 14);
        //p.isPlayerUnit = false;

        // uManager.units.Add(a);
        //a.transform.parent = this.transform;
        //spawnBarbarians();
        //grid.selectedUnit = a.gameObject;
        //ui.showStatus(stats, true);
        //a.isPlayerUnit = true;

        ////////////////////////
        //update Tile info!!!
        ////////////////////////

        //
        //FOR KODY, If Value Is Null, Set To Some "DEFAULT" Faction.
        //
        Debug.Log("PlayerFaction(GameManager): " + PlayerPrefs.GetString("PlayerFaction"));
    }
	
	// Update is called once per frame
	void Update () {
        if (turnCount == 0)
        {
            turnCount++;
            int x = 21;
            int y = 4;
            Vector2 startPos = Map.vectorize(x, y);
            Alpha a = (Alpha)uManager.addUnit(mManager.map, UnitType.Alpha, Faction.Feline, true, x, y);
            ui.setFoodStatus(rManager);
            spawnEnemyAlphas();

            GameObject camera = GameObject.Find("MainCamera");
            camera.GetComponent<CameraController>().xPos =  startPos.x;
            camera.GetComponent<CameraController>().yPos =  startPos.y;
            camera.GetComponent<CameraController>().zPos = -40.0f;
            camera.transform.position = new Vector3(startPos.x, startPos.y, -40);
        }

		//checkWin();
    }

    public void spawnEnemyAlphas()
    {
        //AlphaStats canineStats = new AlphaStats();
        Alpha canineAlpha = (Alpha)uManager.addUnit(mManager.map, UnitType.Alpha, Faction.Canine, false, 10, 2);
        //uManager.units.Add(canineAlpha);

        //Vector2 avianPos = Map.vectorize(8, 11);
        //AlphaStats avianStats = new AlphaStats(avianPos);
        Alpha avianAlpha = (Alpha)uManager.addUnit(mManager.map, UnitType.Alpha, Faction.Avian, false, 16, 12);
        //uManager.units.Add(avianAlpha);
        //avianAlpha.name = "AvianAlpha";
        //avianAlpha.Faction = Faction.Avian;

        //Vector2 amphibianPos = Map.vectorize(10, 3);
        //AlphaStats amphibianStats = new AlphaStats(amphibianPos);
        Alpha amphibianAlpha = (Alpha)uManager.addUnit(mManager.map, UnitType.Alpha, Faction.Amphibian, false, 7, 15);
        //uManager.units.Add(amphibianAlpha);
        //amphibianAlpha.name = "AvianAlpha";
        //amphibianAlpha.Faction = Faction.Amphibian;

    }


    //Selection Methods

    public Unit getPrimaryUnit()
    {
        return primaryUnit;
    }

    public void setPrimaryUnit(Unit primary)
    {
        unhighlightPrimaryUnit();

        primaryUnit = primary;
        ui.setPrimary(primary == null ? "" : primary.name);
        ui.updateButtons(primary);

        highlightPrimaryUnit();
    }

    public void clearPrimaryUnit()
    {
        setPrimaryUnit(null);
        ui.setPrimary("");
    }

    public void highlightPrimaryUnit()
    {
        if (primaryUnit != null)
        {
            List<Tile> tiles = getMap().getTilesInRange(getMap().getTile(primaryUnit.tileX, primaryUnit.tileY), primaryUnit.RemainingMovement);
            foreach (Tile t in tiles)
                t.terrain_sprite.GetComponent<ClickableTile>().Highlight();
        }
    }

    public void unhighlightPrimaryUnit()
    {
        if (primaryUnit != null)
        {
            List<Tile> tiles = getMap().getTilesInRange(getMap().getTile(primaryUnit.tileX, primaryUnit.tileY), primaryUnit.RemainingMovement);
            foreach (Tile t in tiles)
                t.terrain_sprite.GetComponent<ClickableTile>().UnHighlight();
        }
    }

    public Unit getSecondaryUnit()
    {
        return secondaryUnit;
    }

    public void setSecondaryUnit(Unit secondary)
    {
        secondaryUnit = secondary;
        ui.setSecondary(secondary == null ? "" : secondary.name);
    }

    public void clearSecondaryUnit()
    {
        setSecondaryUnit(null);
        ui.setSecondary("");
    }

    //
    public void primarySpawnPawn()
    {
        if (primaryUnit != null && primaryUnit is Alpha)
        {
            ((Alpha)primaryUnit).createPawn();
        }
    }

    public void primaryBurrow()
    {
        if (primaryUnit is Alpha)
        {
			((Alpha)primaryUnit).burrow();
            ui.updateButtons(primaryUnit);
		}
	}

	public void primaryUnburrow()
	{
		if (primaryUnit is Alpha)
		{
			((Alpha)primaryUnit).unburrow();
            ui.updateButtons(primaryUnit);
        }
	}

	private void spawnBarbarians()
	{
		Vector2 testPos = Map.vectorize(8, 8);
		//Unit p = (Alpha)uManager.addUnit(new AlphaStats(testPos), mManager.map, UnitType.Alpha, false, 8, 8);
		//uManager.units.Add(p);
	}

	public void createPawn()
	{
		//Unit u = uManager.addUnit();
		//rManager.cost(u.getCost());
	}

	public static Vector2 vectorize(int a, int b) //?
	{
		return Map.vectorize(a, b);
	}

	private void checkWin()
	{
		if (uManager.playerUnits.Count == 0) {
			//player lost all units
			//ret = true;
			SceneManager.LoadScene ("EndingFailure");
		} else if (uManager.npcUnits.Count == 0) {
			//npc units gone
			//ret = true;
			SceneManager.LoadScene ("EndingConquer");
		} else if (turnCount > maxTurns) {
			if (uManager.playerUnits.Count > uManager.npcUnits.Count) {
				//player has more by end of game
				//ret = true;
				SceneManager.LoadScene ("EndingPopulation");
			} else {
				//npc has more
				//ret = true;
				SceneManager.LoadScene ("EndingFailure");
			}
		} else {
			//Debug.Log ("pUnits: " + uManager.playerUnits.Count);
			//Debug.Log ("nUnits: " + uManager.npcUnits.Count);
			//Debug.Log ("turns:  " + turnCount);
		}
		//return ret;
	}

	public void endTurn()
	{
		turnCount++;

		rManager.endTurn(uManager.units, mManager.map);
		uManager.endTurn();

        clearPrimaryUnit();
        clearSecondaryUnit();
        ui.setFoodStatus(rManager);

        checkWin();
	}

	public Map getMap()
	{
		return mManager.map;
	}

	public void checkUpgrade()
	{
		if (primaryUnit != null && !(primaryUnit is Alpha))
		{
            Debug.Log("Upgrading non-Alpha!");
			primaryUnit.Upgrade();
		}
	}

	//NOTICE: In this function both static Map methods are called as well as instanced Map method... this might pose a problem later on -Adam
	public void MoveSelectedUnitTo(int destX, int destY) {
		if (mManager.map.getTile(destX, destY).isEnterable() && primaryUnit != null) {
			//Unit u = primaryUnit.GetComponent<Unit>();
			//Debug.Log("Distance " + Map.getDistance(primaryUnit.tileX, primaryUnit.tileY, destX, destY));
			//Debug.Log("Traveled " + primaryUnit.TilesMoved);
			//Debug.Log(primaryUnit.Speed);
			if (primaryUnit.canMove(Map.getDistance(primaryUnit.tileX, primaryUnit.tileY, destX, destY)))
			{
                unhighlightPrimaryUnit();
				//Debug.Log("CANMOVE");
				List<Tile> range = mManager.map.getTilesInRange(mManager.map.getTile(primaryUnit.tileX, primaryUnit.tileY), primaryUnit.RemainingMovement);

                foreach (Tile t in range)
				{
					if (t.a == destX && t.b == destY)
					{
						//primaryUnit.setTilesMoved(Map.getDistance(primaryUnit.tileX, primaryUnit.tileY, destX, destY));

						/*mManager.map.getTile(primaryUnit.tileX, primaryUnit.tileY).unit = null;
                        mManager.map.getTile(destX, destY).unit = primaryUnit.gameObject;
                        int srcX = primaryUnit.tileX = destX;
                        int srcY = primaryUnit.tileY = destY;
                        primaryUnit.gameObject.transform.position = Map.vectorize(srcX, srcY);*/
						primaryUnit.moveUnit(t);
                    }
                }
                highlightPrimaryUnit();
			}
		}
	}
}
