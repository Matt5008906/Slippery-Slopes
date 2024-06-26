using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust this speed as needed

    void Update()
    {
        // Move the object horizontally
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }
}
