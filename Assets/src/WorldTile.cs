using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New World Tile", menuName = "Tiles/World Tile")]
public class WorldTile : Tile
{
    public string type;
    public string description;

    public bool traversable;
    public bool openForPlacement;

    public List<WorldItem> preconditions;
    public List<WorldItem> effects;

    public virtual List<GoapAction> GenerateActionList()
    {
        List<GoapAction> actionList = new List<GoapAction>();
        //actionList.Add(new GoapAction("default action"));

        return actionList;
    }
}
