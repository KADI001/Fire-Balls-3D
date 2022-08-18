using UnityEngine;

public class Point : MonoBehaviour
{
    public Vector3 Position => transform.position;
    public Vector3 Rotation => transform.eulerAngles;
}