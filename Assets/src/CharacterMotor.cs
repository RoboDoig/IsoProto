using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CharacterMotor : MonoBehaviour
{
    public Vector3Int destinationCell;
    public float moveSpeed;

    Tilemap tilemap;
    Pathfinder pathfinder;
    public List<Pathfinder.Node> path;
    Vector3Int pathDestinationCell;

    // Start is called before the first frame update
    void Start()
    {
        tilemap = GameObject.FindGameObjectWithTag("BaseTilemap").GetComponent<Tilemap>();
        pathfinder = GameObject.FindGameObjectWithTag("Grid").GetComponent<Pathfinder>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (path != null && path.Count > 0)
        //{
        //    MoveOnPath();
        //}
    }

    public bool FindPath(Vector3Int _destinationCell)
    {
        path = pathfinder.FindPath(GetCurrentCell(), _destinationCell);

        if (path != null)
        {
            destinationCell = _destinationCell;
            pathDestinationCell = GetCurrentCell();
            return true;
        } else
        {
            return false;
        }

    }

    public bool MoveOnPath()
    {
        if (path.Count <= 0 || path == null)
        {
            path = null;
            return true;
        }

        if(MoveToCell(pathDestinationCell))
        {
            path.RemoveAt(0);
            pathDestinationCell = path[0].position;
        }

        return false;
    }


    bool MoveToCell(Vector3Int destinationCell)
    {
        Vector3 worldDestination = tilemap.CellToWorld(destinationCell);

        if((worldDestination - transform.position).magnitude > 0.1f)
        {
            Vector3 moveVector = (worldDestination - transform.position).normalized;
            transform.position += moveVector * Time.deltaTime * moveSpeed;

            return false;
        } else
        {
            return true;
        }
    }

    public Vector3Int GetCurrentCell()
    {
        return tilemap.WorldToCell(transform.position);
    }
}
