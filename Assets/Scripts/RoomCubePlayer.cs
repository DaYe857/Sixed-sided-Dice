using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomCubePlayer : NetworkBehaviour
{
    public CubeDirection currentDirection;
    public List<Transform> cubeDirections;
    public float gravity;
    private Rigidbody rb;
    private Vector3 centerPos;
    private DiceRoller diceRoller;
    private bool isInitialized = false;
    private CustomGravity _customGravity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        cubeDirections = new List<Transform>();
        diceRoller = GetComponent<DiceRoller>();
        _customGravity = GetComponent<CustomGravity>();
        rb.useGravity = false;
    }

    /*private void Update()
    {
        if (SceneManager.GetActiveScene().name == "GravityDemo"&&isInitialized==false)
        {
            rb.useGravity = true;
            cubeDirections.Add(GameObject.Find("Plane").transform);
            cubeDirections.Add(GameObject.Find("Plane (1)").transform);
            cubeDirections.Add(GameObject.Find("Plane (2)").transform);
            cubeDirections.Add(GameObject.Find("Plane (3)").transform);
            cubeDirections.Add(GameObject.Find("Plane (4)").transform);
            cubeDirections.Add(GameObject.Find("Plane (5)").transform);
            isInitialized = true;
        }
        if (isServer)
        {
            /*if (Input.GetKeyDown(KeyCode.Space))
            {
                diceRoller.RollDice();
                RollCurrentDirection();
                Debug.Log(1);
            }#1#
        }

        if (cubeDirections.Count == 6)
        {
            switch (currentDirection)
            {
                case CubeDirection.UP:
                    centerPos = cubeDirections[0].position;
                    break;
                case CubeDirection.DOWN:
                    centerPos = cubeDirections[1].position;
                    break;
                case CubeDirection.LEFT:
                    centerPos = cubeDirections[2].position;
                    break;
                case CubeDirection.RIGHT:
                    centerPos = cubeDirections[3].position;
                    break;
                case CubeDirection.FORWARD:
                    centerPos = cubeDirections[4].position;
                    break;
                case CubeDirection.BACKWARD:
                    centerPos = cubeDirections[5].position;
                    break;
            }
        }
    }*/

    private void RollCurrentDirection()
    {
        switch (diceRoller.GetCurrentNum())
        {
            case 1:
                currentDirection = CubeDirection.UP;
                break;
            case 2:
                currentDirection = CubeDirection.DOWN;
                break;
            case 3:
                currentDirection = CubeDirection.LEFT;
                break;
            case 4:
                currentDirection = CubeDirection.RIGHT;
                break;
            case 5:
                currentDirection = CubeDirection.FORWARD;
                break;
            case 6:
                currentDirection = CubeDirection.BACKWARD;
                break;
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(_customGravity.GetGravity(centerPos,transform.position,gravity));
    }

    public void SetDirection(CubeDirection dir) => currentDirection = dir;
}
