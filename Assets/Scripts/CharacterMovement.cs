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
    private float TempTimeLeft = 0;
    private float TempTimeRight = 0;
    
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
        
        TempTimeLeft += Time.deltaTime;
        TempTimeRight += Time.deltaTime;
        verticalTempTime += Time.deltaTime;

        bool canMoveLeft =  (TempTimeLeft >= horizontalTime & horizontalInput == -1);
        bool canMoveRight =  (TempTimeRight >= horizontalTime & horizontalInput == 1);
        bool canMoveVertical = (verticalTempTime >= verticalTime & verticalInput != 0);

        if((canMoveLeft) | (canMoveRight))
        {
            if (canMoveLeft)
                TempTimeLeft = 0;
            
            if (canMoveRight)
                TempTimeRight = 0;
            

            moveDirection = new Vector3(horizontalInput, 0, 0);
            Move();
        }
        else if(canMoveVertical)
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
