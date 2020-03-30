using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{

    public Tilemap floorTilemap;
    public Tilemap buildTilemap;

    public Vector2Int worldSize;

    [Header("Floor Noise")]
    public int seed;
    public float scale;
    public int octaves;
    public float persistence;
    public float lacunarity;
    public Vector2 offset;

    [Header("Build Noise")]
    public int buildSeed;
    public float buildScale;
    public int buildOctaves;
    public float buildPersistence;
    public float buildLacunarity;
    public Vector2 buildOffset;

    [Header("Base Tiles")]
    public WorldTile grassTile;
    public WorldTile waterTile;

    [Header("Level 1 Tiles")]
    public WorldTile defaultOpenTile;
    public WorldTile treeTile;
    public WorldTile foodTile;
    public WorldTile stoneTile;

    [Header("Agents")]
    public int nAgentsToPlace;
    public GameObject basicCharacter;

    //public Dictionary<string, Tile> tileDictionary = new Dictionary<string, Tile>();

    private WorldTileData[,] worldTileData;

    void Awake()
    {
        //tileDictionary.Add("grass", grassTile);
        //tileDictionary.Add("water", waterTile);

        //tileDictionary.Add("default", defaultOpenTile);
        //tileDictionary.Add("tree", treeTile);

        GenerateMapData(worldSize.x, worldSize.y);
        GenerateActionList();
        RenderMap();
        PlaceAgents();
    }

    void GenerateMapData(int width, int height)
    {
        worldTileData = new WorldTileData[width, height];
        float[,] floorNoiseMap = Noise.GenerateNoiseMap(width, height, seed, scale, octaves, persistence, lacunarity, offset);
        float[,] buildNoiseMap = Noise.GenerateNoiseMap(width, height, buildSeed, buildScale, buildOctaves, buildPersistence, buildLacunarity, buildOffset);
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (floorNoiseMap[x,y] > 0.5)
                {
                    if (buildNoiseMap[x, y] > 0.6)
                    {
                        if (buildNoiseMap[x, y] > 0.7)
                        {
                            worldTileData[x, y] = new WorldTileData(new Vector3Int(x, y, 0), grassTile, treeTile, floorNoiseMap[x, y], buildNoiseMap[x, y]);
                        } else if (buildNoiseMap[x, y] > 0.65)
                        {
                            worldTileData[x, y] = new WorldTileData(new Vector3Int(x, y, 0), grassTile, foodTile, floorNoiseMap[x, y], buildNoiseMap[x, y]);
                        } else
                        {
                            worldTileData[x, y] = new WorldTileData(new Vector3Int(x, y, 0), grassTile, stoneTile, floorNoiseMap[x, y], buildNoiseMap[x, y]);
                        }
                    } else
                    {
                        worldTileData[x, y] = new WorldTileData(new Vector3Int(x, y, 0), grassTile, defaultOpenTile, floorNoiseMap[x, y], buildNoiseMap[x, y]);
                    }
                } else
                {
                    worldTileData[x, y] = new WorldTileData(new Vector3Int(x, y, 0), waterTile, defaultOpenTile, floorNoiseMap[x,y], buildNoiseMap[x,y]);
                }
            }
        }
    }

    void GenerateActionList()
    {
        for (int x = 0; x < worldSize.x; x++)
        {
            for (int y = 0; y < worldSize.y; y++)
            {
                worldTileData[x, y].actionList = worldTileData[x, y].buildTile.GenerateActionList();
            }
        }
    }

    void RenderMap()
    {
        floorTilemap.ClearAllTiles();
        buildTilemap.ClearAllTiles();

        for (int x = 0; x < worldTileData.GetLength(0); x++)
        {
            for (int y = 0; y<worldTileData.GetLength(1); y++)
            {
                //floorTilemap.SetTile(new Vector3Int(x, y, 0), tileDictionary[worldTileData[x, y].floorType]);
                //buildTilemap.SetTile(new Vector3Int(x, y, 0), tileDictionary[worldTileData[x, y].buildType]);
                floorTilemap.SetTile(new Vector3Int(x, y, 0), worldTileData[x, y].floorTile);
                buildTilemap.SetTile(new Vector3Int(x, y, 0), worldTileData[x, y].buildTile);
            }
        }
    }

    void PlaceAgents()
    {
        int nAgentsPlaced = 0;

        while(nAgentsPlaced < nAgentsToPlace)
        {
            Vector2Int placePosition = new Vector2Int(Random.Range(0, worldSize.x), Random.Range(0, worldSize.y));

            if (worldTileData[placePosition.x, placePosition.y].traversable)
            {
                PlaceAgent(basicCharacter, new Vector3Int(placePosition.x, placePosition.y, 0));
                nAgentsPlaced += 1;
            }
        }
    }

    public void PlaceTile(WorldTile tile, Vector3Int position)
    {
        // if base tile open for placement etc...

        // update tile data
        worldTileData[position.x, position.y].traversable = tile.traversable;
        worldTileData[position.x, position.y].openForPlacement = false;

        // render the change
        buildTilemap.SetTile(position, tile);
    }

    public void PlaceAgent(GameObject agent, Vector3Int position)
    {
        GameObject placedAgent = Instantiate(agent);

        Vector3 placePosition = floorTilemap.CellToWorld(position);

        placedAgent.transform.position = placePosition;
    }

    public WorldTileData[,] GetWorldTileData()
    {
        return worldTileData;
    }

    public Vector2Int GetWorldSize()
    {
        return new Vector2Int(worldTileData.GetLength(0), worldTileData.GetLength(1));
    }
}
