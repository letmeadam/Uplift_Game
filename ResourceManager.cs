using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{

    /*
     * These arrays are indexed by the Faction enum
     * 
     */

    private static int[] factionResources = new int[4];
    private static int[] factionUpkeep = new int[4];
    private static Faction playerFaction;


    //public int currentFood;
    //public int lastCollection, lastUpkeep;

    public static int PlayerFood
    {
        get
        {
            return factionResources[(int)PlayerFaction];
        }
        set
        {
            factionResources[(int)PlayerFaction] = value;
        }
    }

    public static int FelineFood
    {
        get
        {
            return factionResources[(int)Faction.Feline];
        }

        set
        {
            factionResources[(int)Faction.Feline] = value;
        }
    }

    public static int CanineFood
    {
        get
        {
            return factionResources[(int)Faction.Canine];
        }

        set
        {
            factionResources[(int)Faction.Canine] = value;
        }
    }

    public static int AvianFood
    {
        get
        {
            return factionResources[(int)Faction.Avian];
        }

        set
        {
            factionResources[(int)Faction.Avian] = value;
        }
    }

    public static int AmphFood
    {
        get
        {
            return factionResources[(int)Faction.Amphibian];
        }

        set
        {
            factionResources[(int)Faction.Amphibian] = value;
        }
    }

    public static Faction PlayerFaction
    {
        get
        {
            return playerFaction;
        }

        set
        {
            playerFaction = value;
        }
    }

    public static int PlayerUpkeep
    {
        get
        {
            return factionUpkeep[(int)playerFaction];
        }
    }

    public static int getFactionResources(Faction f)
    {
        switch (f)
        {
            case Faction.Feline:
                return FelineFood;
            case Faction.Canine:
                return CanineFood;
            case Faction.Avian:
                return AvianFood;
            case Faction.Amphibian:
                return AmphFood;
            default:
                return 0;
        }
    }

    // Use this for initialization
    void Start()
    {

        //surplus = 0;

    }

    public void init()
    {
        playerFaction = Faction.Feline;
        for (int i = 0; i < 4; i++)
        {
            factionResources[i] = 150;
            factionUpkeep[i] = 0;
        }
    }


    // Update is called once per frame
    void Update()
    {

    }

    public static int getResourcesFromTile(List<Tile> tiles)
    {
        int val = 0;
        foreach (Tile t in tiles)
        {
            if (t.unit == null)
                val += t.getFood();
        }
        return val;
    }

    public int getResourcesFromTile(ArrayList tiles)
    {
        int val = 0;
        foreach (Tile t in tiles)
        {
            if (t.unit == null)
                val += t.getFood();
        }
        return val;
    }

    public void setFactionResources(Faction f, int val)
    {
        switch (f)
        {
            case Faction.Feline:
                FelineFood -= val;
                break;
            case Faction.Canine:
                CanineFood -= val;
                break;
            case Faction.Avian:
                AvianFood -= val;
                break;
            case Faction.Amphibian:
                AmphFood -= val;
                break;
            default:
                break;
        }
    }

    /*public int getCurrentFood()
    {
        return currentFood;
    }*/

    public void removeUpkeep(Faction f, int upkeep)
    {
        factionUpkeep[(int)f] -= upkeep;
    }

    public void addUpkeep(Faction f, int upkeep)
    {
        factionUpkeep[(int)f] += upkeep;
    }

    public int getUpkeep(ArrayList units)
    {
        int upkeep = 0;
        foreach (Unit u in units)
        {
            upkeep += u.Upkeep;
        }
        return upkeep;
    }

    public void endTurn(Alpha player)
    {
    }


    /******************************************
     * 
     *  This is the one that gets used...changes made here
     *  
     *****************************************************/
    public void endTurn(ArrayList units, Map map)
    {
        for (int i = 0; i < 4; i++)
        {
            //check each of the player's units
            foreach (Unit u in units)
            {
                if (u is Alpha)
                {
                    int collection = 0;
                    Alpha a = u as Alpha;

                    //resources collected this turn
                    collection += getResourcesFromTile(map.getTilesInRange(u.tileX, u.tileY, a.CollectionRadius));
                    //Debug.Log(a.Faction.ToString() + " Collected: " + collection);

                    //if THIS alpha is burrowed and has queued units
                    if (a.Burrowed && a.queueLength > 0)
                    {
                        //use collected food for personal surplus
                        //personal surplus determines when a pawn can be produced.
                        if (collection - a.Upkeep <= 0)
                        {
                            a.current = State.SEARCHING;
                            //a.Surplus = 0;
                            a.unburrow();
                        }
                        else
                        {
                            a.Surplus += collection - a.Upkeep;
                            if (a.Surplus >= 150)
                            {
                                a.CanProduce = true;
                                a.Surplus -= 150;
                            }
                        }
                        //Debug.Log("Producing Units -- " + a.Surplus);
                    }
                    else
                    {
                        //otherwise add food to global pool and subtract upkeep
                        factionResources[(int)a.Faction] += collection;
                        //factionUpkeep[(int)a.Faction] += a.Upkeep;

                    }

                }
                else
                {
                    u.collected += map.getResourcesFromTiles(map.getTilesInRange(u.tileX, u.tileY, 0));
                    //factionResources[(int)u.Faction] += u.collected;
                }


            }
        }

        for (int i = 0; i < 4; i++)
        {
            factionResources[i] -= factionUpkeep[i];
            factionResources[i] = factionResources[i] < 0 ? 0 : factionResources[i];

            //Debug.Log("Upkeep " + factionUpkeep[i] + " " + i);
            //Debug.Log("Current Food: " + factionResources[i]);
        }


    }
}
