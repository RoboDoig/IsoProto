using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;

public class PlayerInterface : MonoBehaviour
{
    public Tilemap baseTilemap;
    public Tilemap buildTilemap;
    public Tilemap selectorTilemap;
    public TileBase selectorTile;
    
    public MapManager mapManager;
    public UIManager uiManager;

    private Vector3Int lastSelectedCell;

    private delegate void UpdateAction();
    private UpdateAction updateAction;

    // Build mode
    private WorldTile currentBuildPlaceTile;

    // Start is called before the first frame update
    void Start()
    {
        lastSelectedCell = new Vector3Int(0, 0, 0);
        updateAction = Default;
    }

    // Update is called once per frame
    void Update()
    {
        updateAction();
    }

    public void EnterBuildMode(WorldTile tile)
    {
        currentBuildPlaceTile = tile;
        updateAction = BuildMode;
    }

    // Update Modes
    void Default()
    {
        selectorTilemap.SetTile(lastSelectedCell, null);

        // Get location
        Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Get tile at that point
        Vector3Int selectedCell = baseTilemap.WorldToCell(point);
        lastSelectedCell = selectedCell;

        // Update UI
        if (selectedCell.x >= 0 && selectedCell.y >= 0)
            uiManager.UpdateTextInfo(mapManager.GetWorldTileData()[selectedCell.x, selectedCell.y]);

        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                // Selection
                WorldTileData selectedTile = mapManager.GetWorldTileData()[selectedCell.x, selectedCell.y];
                uiManager.UpdateActionInfo(selectedTile.actionList);

                updateAction = TileSelected;
            }
        }

        selectorTilemap.SetTile(selectedCell, selectorTile);
    }

    void TileSelected()
    {
        if (Input.GetMouseButtonDown(1))
        {
            uiManager.ClearActionInfo();
            updateAction = Default;
        }
    }

    void BuildMode()
    {
        selectorTilemap.SetTile(lastSelectedCell, null);

        // Get location
        Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Get tile at that point
        Vector3Int selectedCell = baseTilemap.WorldToCell(point);
        lastSelectedCell = selectedCell;

        // If in world bounds
        if (selectedCell.x >= 0 && selectedCell.y >= 0)
        {
            // Update UI
            uiManager.UpdateTextInfo(mapManager.GetWorldTileData()[selectedCell.x, selectedCell.y]);

            // Preview selection
            if (mapManager.GetWorldTileData()[selectedCell.x, selectedCell.y].openForPlacement)
            {
                selectorTilemap.SetTile(selectedCell, currentBuildPlaceTile);
            }
            else
            {
                selectorTilemap.SetTile(selectedCell, null);
            }

            // Placement
            if (Input.GetMouseButtonDown(0) && mapManager.GetWorldTileData()[selectedCell.x, selectedCell.y].openForPlacement)
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    mapManager.PlaceTile(currentBuildPlaceTile, selectedCell);
                }
            }

            // Exit
            if (Input.GetMouseButtonDown(1))
            {
                updateAction = Default;
            }
        }
    }
}
