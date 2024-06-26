using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopingBG : MonoBehaviour
{
    [SerializeField] private float backgroundSpeed = -10f;
    [SerializeField] private Transform background1;
    [SerializeField] private Transform background2;
    
    private float backgroundWidth;

    private void Start()
    {
        backgroundWidth = background1.GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void Update()
    {
        background1.position += Vector3.left * backgroundSpeed * Time.deltaTime;
        background2.position += Vector3.left * backgroundSpeed * Time.deltaTime;

        if (background1.position.x < -backgroundWidth)
        {
            background1.position = new Vector3(background2.position.x + backgroundWidth, background1.position.y, background1.position.z);
        }

        if (background2.position.x < -backgroundWidth)
        {
            background2.position = new Vector3(background1.position.x + backgroundWidth, background2.position.y, background2.position.z);
        }
    }
}
