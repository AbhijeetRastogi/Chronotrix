using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float horizontalTime = 0.5f;
    [SerializeField] private float verticalTime = 0.5f;
    
    private Vector3 moveDirection;
    private int horizontalInput;
    private int verticalInput;
    private float horizontalTempTime =0;
    private float verticalTempTime =0;
    
    public void Update()
    {
        GetInput();
        Moving();
    }

    public void GetInput()
    {
        horizontalInput = (int)Input.GetAxisRaw("Horizontal");
        verticalInput = (int)Input.GetAxisRaw("Vertical");
    }
    
    public void Moving()
    {
        
        horizontalTempTime += Time.deltaTime;
        verticalTempTime += Time.deltaTime;
        
        if(horizontalTempTime >= horizontalTime & horizontalInput != 0)
        {
            horizontalTempTime = 0;
            moveDirection = new Vector3(horizontalInput, 0, 0);
            Move();
        }
        else if(verticalTempTime >= verticalTime & verticalInput != 0)
        {
            verticalTempTime = 0;
            moveDirection = new Vector3(0, verticalInput, 0);
            Move();
        }
    }
    public void Move()
    {
        transform.position += moveDirection;
    }
    
}
