using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New World Tile", menuName = "Tiles/Resource Tile")]
public class WorldTileResource : WorldTile
{

    public override List<GoapAction> GenerateActionList()
    {
        List<GoapAction> actionList = new List<GoapAction>();
        actionList.Add(new GoapActionCollectResource("collect resource"));

        return actionList;
    }

}
