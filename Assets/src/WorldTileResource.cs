using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Resource Tile", menuName = "Tiles/Resource Tile")]
public class WorldTileResource : WorldTile
{
    public float growTime;
    public WorldTile growsInto;

    public override List<GoapAction> GenerateActionList()
    {
        List<GoapAction> actionList = new List<GoapAction>();
        actionList.Add(new GoapActionCollectResource("collect resource"));

        return actionList;
    }

    public override WorldTile OnUpdate(WorldTileData data)
    {
        if (data.timeAlive > growTime)
        {
            return growsInto;
        } else
        {
            return null;
        }
    }
}
