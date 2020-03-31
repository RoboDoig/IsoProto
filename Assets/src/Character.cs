using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Character : MonoBehaviour
{
    CharacterMotor charMotor;
    public delegate void UpdateAction();
    public UpdateAction updateAction;

    float idleTimer;

    private void Start()
    {
        charMotor = GetComponent<CharacterMotor>();
        idleTimer = Random.Range(0f, 10f);
        updateAction = Idle;
    }

    void Update()
    {
        updateAction();
    }

    public void FindPath(Vector3Int destinationCell)
    {
        charMotor.FindPath(destinationCell);
        updateAction = GoToDestination;
    }

    void Idle()
    {

    }

    void GoToDestination()
    {
        if (charMotor.path != null && charMotor.path.Count > 0)
        {
            if (charMotor.MoveOnPath())
            {
                updateAction = Idle;
            }
        }
        else
        {
            updateAction = Idle;
        }
    }
}
