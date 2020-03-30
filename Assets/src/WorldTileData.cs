using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldTileData
{
    public Vector3Int position;

    public WorldTile floorTile;
    public WorldTile buildTile;

    public string floorType;
    public string buildType;

    public float floorNoiseValue;
    public float buildNoiseValue;

    public bool traversable;
    public bool openForPlacement;

    public List<GoapAction> actionList;

    public WorldTileData(Vector3Int _position, WorldTile _floorTile, WorldTile _buildTile, float _floorNoiseValue, float _buildNoiseValue)
    {
        position = _position;

        floorTile = _floorTile;
        buildTile = _buildTile;

        floorType = _floorTile.type;
        buildType = _buildTile.type;

        if (!_floorTile.traversable || !_buildTile.traversable)
        {
            traversable = false;
        } else
        {
            traversable = true;
        }

        if (!_floorTile.openForPlacement || !_buildTile.openForPlacement)
        {
            openForPlacement = false;
        }
        else
        {
            openForPlacement = true;
        }

        floorNoiseValue = _floorNoiseValue;
        buildNoiseValue = _buildNoiseValue;
    }
}
