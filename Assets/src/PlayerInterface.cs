using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;

public class PlayerInterface : MonoBehaviour
{
    public Tilemap baseTilemap;
    public Tilemap selectorTilemap;
    public TileBase selectorTile;
    public WorldTile wallTile;
    public MapManager mapManager;
    public UIManager uiManager;

    private Vector3Int lastSelectedCell;

    private delegate void UpdateAction();
    private UpdateAction updateAction;

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
                Debug.Log("switching to tile selected");
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
}
