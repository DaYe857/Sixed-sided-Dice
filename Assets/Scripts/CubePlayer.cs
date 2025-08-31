using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using Unity.VisualScripting;
using UnityEngine;

public class CubePlayer : NetworkBehaviour
{
    public CubeDirection currentDirection;
    private List<Transform> cubeDirections;
    public float forceFactor = 0.5f;
    private Rigidbody rb;
    private Vector3 centerPos;
    private DiceRoller diceRoller;
    private CustomGravity _customGravity;
    private CameraController cameraController;
    private Transform mainCameraTransform;
    public float moveSpeed = 5f; // 移动速度（可在Inspector中调整）
    public float cameraDistance = 2f;
    [SyncVar]
    public float endDistance;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        cubeDirections = new List<Transform>();
        diceRoller = GetComponent<DiceRoller>();
        _customGravity = GetComponent<CustomGravity>();
        mainCameraTransform = Camera.main.transform;
    }
    
    public override void OnStartLocalPlayer()
    {
        Camera.main.transform.SetParent(transform);
        Camera.main.transform.localPosition = Vector3.zero;
        Camera.main.transform.rotation = Quaternion.identity;
        cubeDirections.Add(GameObject.Find("Plane").transform);
        cubeDirections.Add(GameObject.Find("Plane (1)").transform);
        cubeDirections.Add(GameObject.Find("Plane (2)").transform);
        cubeDirections.Add(GameObject.Find("Plane (3)").transform);
        cubeDirections.Add(GameObject.Find("Plane (4)").transform);
        cubeDirections.Add(GameObject.Find("Plane (5)").transform);
        //RollCurrentDirection();
    }
    

    private void Update()
    {
        if (isLocalPlayer)
        {
            transform.rotation = Quaternion.identity;
            Move();
            UpdateCurrentDirection();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                RollCurrentDirection();
            }
            if (Input.GetKeyDown(KeyCode.J))
            {
                forceFactor *= 2;
            }
        }
        UpdateEndDistance();
    }

    private void Move()
    {
        Vector3 moveDirection = Vector3.zero;

        // 基于物体自身坐标系计算移动方向
        if (Input.GetKey(KeyCode.W)) // 前移（物体自身Z轴正方向）
            moveDirection += mainCameraTransform.up;
        if (Input.GetKey(KeyCode.S)) // 后移（物体自身Z轴负方向）
            moveDirection -= mainCameraTransform.up;;
        if (Input.GetKey(KeyCode.A)) // 左移（物体自身X轴负方向）
            moveDirection -= mainCameraTransform.right;
        if (Input.GetKey(KeyCode.D)) // 右移（物体自身X轴正方向）
            moveDirection += mainCameraTransform.right;

        // 标准化方向向量 + 帧率无关的位移计算
        if (moveDirection != Vector3.zero)
        {
            moveDirection.Normalize(); // 避免斜向移动更快
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
        }
    }

    private void UpdateEndDistance()
    {
        if (cubeDirections.Count == 6)
        {
            switch (currentDirection)
            {
                case CubeDirection.UP:
                    endDistance = transform.position.y - cubeDirections[0].position.y;
                    break;
                case CubeDirection.DOWN:
                    endDistance = cubeDirections[2].position.y - transform.position.y;
                    break;
                case CubeDirection.LEFT:
                    endDistance = cubeDirections[1].position.z - transform.position.z;
                    break;
                case CubeDirection.RIGHT:
                    endDistance = transform.position.z - cubeDirections[3].position.z;
                    break;
                case CubeDirection.FORWARD:
                    endDistance = cubeDirections[4].position.x - transform.position.x;
                    break;
                case CubeDirection.BACKWARD:
                    endDistance = transform.position.x - cubeDirections[5].position.x;
                    break;
            }
        }
        /*float distance1 = transform.position.y - cubeDirections[0].position.y;
        float distance2 = cubeDirections[1].position.z - transform.position.z;
        float distance3 = cubeDirections[2].position.y - transform.position.y;
        float distance4 = transform.position.z - cubeDirections[3].position.z;
        float distance5 = cubeDirections[4].position.x - transform.position.x;
        float distance6 = transform.position.x - cubeDirections[5].position.x;
        endDistance = Mathf.Min(distance1, distance2, distance3, distance4, distance5, distance6);*/
    }

    private void UpdateCurrentDirection()
    {
        if (cubeDirections.Count == 6)
        {
            switch (currentDirection)
            {
                case CubeDirection.UP:
                    centerPos = -cubeDirections[2].up;
                    Camera.main.transform.forward=-cubeDirections[2].up;
                    Camera.main.transform.localPosition = Vector3.Lerp(Camera.main.transform.localPosition, new Vector3(0,-cameraDistance,0), 0.5f);
                    break;
                case CubeDirection.DOWN:
                    centerPos = -cubeDirections[0].up;
                    Camera.main.transform.forward=-cubeDirections[0].up;
                    Camera.main.transform.localPosition = Vector3.Lerp(Camera.main.transform.localPosition, new Vector3(0,cameraDistance,0), 0.5f);
                    break;
                case CubeDirection.LEFT:
                    centerPos = -cubeDirections[3].up;
                    Camera.main.transform.forward=-cubeDirections[3].up;
                    Camera.main.transform.localPosition = Vector3.Lerp(Camera.main.transform.localPosition, new Vector3(0,0,cameraDistance), 0.5f);
                    break;
                case CubeDirection.RIGHT:
                    centerPos = -cubeDirections[1].up;
                    Camera.main.transform.forward=-cubeDirections[1].up;
                    Camera.main.transform.localPosition = Vector3.Lerp(Camera.main.transform.localPosition, new Vector3(0,0,-cameraDistance), 0.5f);
                    break;
                case CubeDirection.FORWARD:
                    centerPos = -cubeDirections[5].up;
                    Camera.main.transform.forward=-cubeDirections[5].up;
                    Camera.main.transform.localPosition = Vector3.Lerp(Camera.main.transform.localPosition, new Vector3(cameraDistance,0,0), 0.5f);
                    break;
                case CubeDirection.BACKWARD:
                    centerPos = -cubeDirections[4].up;
                    Camera.main.transform.forward=-cubeDirections[4].up;
                    Camera.main.transform.localPosition = Vector3.Lerp(Camera.main.transform.localPosition, new Vector3(-cameraDistance,0,0), 0.5f);
                    break;
            }
        }
    }

    public void RollCurrentDirection()
    {
        Debug.Log("随机方向");
        diceRoller.RollDice();
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
        Debug.Log(currentDirection.ToString());
    }

    public void RollCurrentGravity()
    {
        Debug.Log("随机重力");
        diceRoller.RollDice();
        switch (diceRoller.GetCurrentNum())
        {
            case 1:
                forceFactor *= 0.25f;
                break;
            case 2:
                forceFactor *= 0.5f;
                break;
            case 3:
                forceFactor *= 1f;
                break;
            case 4:
                forceFactor *= 2f;
                break;
            case 5:
                forceFactor *= 4f;
                break;
            case 6:
                forceFactor *= 8f;
                break;
        }
    }

    public void RollExchangeState()
    {
        Debug.Log("交换状态");
        diceRoller.RollDice();
        switch (diceRoller.GetCurrentNum())
        {
            case 1:
                float temp1 = forceFactor;
                CubeDirection tempDirection1 = currentDirection;
                forceFactor = FindObjectOfType<PlayersManager>().GetPlayer(0).GetForceFactor();
                FindObjectOfType<PlayersManager>().GetPlayer(0).SetForceFactor(temp1);
                currentDirection = FindObjectOfType<PlayersManager>().GetPlayer(0).GetDirection();
                FindObjectOfType<PlayersManager>().GetPlayer(0).SetDirection(tempDirection1);
                break;
            case 2:
                float temp2 = forceFactor;
                CubeDirection tempDirection2 = currentDirection;
                forceFactor = FindObjectOfType<PlayersManager>().GetPlayer(1).GetForceFactor();
                FindObjectOfType<PlayersManager>().GetPlayer(1).SetForceFactor(temp2);
                currentDirection = FindObjectOfType<PlayersManager>().GetPlayer(1).GetDirection();
                FindObjectOfType<PlayersManager>().GetPlayer(1).SetDirection(tempDirection2);
                break;
            case 3:
                float temp3 = forceFactor;
                CubeDirection tempDirection3 = currentDirection;
                forceFactor = FindObjectOfType<PlayersManager>().GetPlayer(2).GetForceFactor();
                FindObjectOfType<PlayersManager>().GetPlayer(2).SetForceFactor(temp3);
                currentDirection = FindObjectOfType<PlayersManager>().GetPlayer(2).GetDirection();
                FindObjectOfType<PlayersManager>().GetPlayer(2).SetDirection(tempDirection3);
                break;
            case 4:
                float temp4 = forceFactor;
                CubeDirection tempDirection4 = currentDirection;
                forceFactor = FindObjectOfType<PlayersManager>().GetPlayer(0).GetForceFactor();
                FindObjectOfType<PlayersManager>().GetPlayer(0).SetForceFactor(temp4);
                currentDirection = FindObjectOfType<PlayersManager>().GetPlayer(0).GetDirection();
                FindObjectOfType<PlayersManager>().GetPlayer(0).SetDirection(tempDirection4);
                break;
            case 5:
                float temp5 = forceFactor;
                CubeDirection tempDirection5 = currentDirection;
                forceFactor = FindObjectOfType<PlayersManager>().GetPlayer(1).GetForceFactor();
                FindObjectOfType<PlayersManager>().GetPlayer(1).SetForceFactor(temp5);
                currentDirection = FindObjectOfType<PlayersManager>().GetPlayer(1).GetDirection();
                FindObjectOfType<PlayersManager>().GetPlayer(1).SetDirection(tempDirection5);
                break;
            case 6:
                float temp6 = forceFactor;
                CubeDirection tempDirection6 = currentDirection;
                forceFactor = FindObjectOfType<PlayersManager>().GetPlayer(2).GetForceFactor();
                FindObjectOfType<PlayersManager>().GetPlayer(2).SetForceFactor(temp6);
                currentDirection = FindObjectOfType<PlayersManager>().GetPlayer(2).GetDirection();
                FindObjectOfType<PlayersManager>().GetPlayer(2).SetDirection(tempDirection6);
                break;
        }
    }

    public void WinGame()
    {
        if (isLocalPlayer)
        {
            FindObjectOfType<UIManager>().ShowWinPanel();
        }
    }

    private void FixedUpdate()
    {
        //rb.AddForce(_customGravity.GetGravity(centerPos,transform.position,gravity));
        rb.AddForce(centerPos*forceFactor);
    }

    public void SetDirection(CubeDirection dir) => currentDirection = dir;
    public CubeDirection GetDirection() => currentDirection;
    
    public float GetForceFactor() => forceFactor;
    public void SetForceFactor(float forceFactor) => this.forceFactor = forceFactor;
    public float GetEndDistance() => endDistance;
}
