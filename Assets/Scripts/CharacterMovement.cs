using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float horizontalSpeed = 5f;
    [SerializeField] private float verticalSpeed = 5f;
    
    private Vector3 moveDirection;

    public void Update()
    {
        GetInput();
        Move();
    }

    public void GetInput()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        moveDirection = new Vector3(horizontalInput, verticalInput, 0);
    }
    
    public void Move()
    {

        transform.position += moveDirection * ( Time.deltaTime * horizontalSpeed );
    }
    
}
