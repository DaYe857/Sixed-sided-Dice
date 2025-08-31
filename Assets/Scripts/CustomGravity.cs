using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGravity : MonoBehaviour
{
    public Vector3 GetGravity(Vector3 center,Vector3 position,float gravity)
    {
        return (center - position).normalized * gravity;
    }

    public Vector3 GetDirection(Vector3 center, Vector3 position)
    {
        return (center - position).normalized;
    }
}
