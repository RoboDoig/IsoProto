using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    CharacterMotor charMotor;
    public delegate void UpdateAction();
    public UpdateAction updateAction;

    float idleTimer;

    private void Start()
    {
        charMotor = GetComponent<CharacterMotor>();
        idleTimer = Random.Range(20f, 100f);
        updateAction = Idle;
    }

    void Update()
    {
        updateAction();
    }

    public void FindPath(Vector3Int destinationCell)
    {
        charMotor.FindPath(destinationCell);
    }

    void Idle()
    {
        idleTimer -= Time.deltaTime;
        if (idleTimer <= 0f)
        {
            idleTimer = Random.Range(20f, 100f);

            if (charMotor.FindPath(charMotor.GetCurrentCell() + new Vector3Int(Random.Range(-50, 50), Random.Range(-50, 50), 0)))
            {
                updateAction = GoToRandomSpot;
            }
        }
    }

    void GoToRandomSpot()
    {
        if (charMotor.MoveOnPath())
        {
            updateAction = Idle;
        }
    }
}
