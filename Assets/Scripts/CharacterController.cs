using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TextCore;


public class CharacterController : MonoBehaviour
{
    
    [SerializeField] private int jumpHeight = 3;
    [SerializeField] private float moveTime = 0.3f;

    private GameObject gm;
    private Rigidbody2D rgbody;
    private float movementSpeed;
    public int horizontal;
    private float timeL;
    private float timeR;
    private bool isJump;
    public bool onGround;
    private bool waitingForInput;
    private int time = 0;
    

    
    public int GetTime()
    {
        return time;
    }
    
    private void Start()    
    {
        gm = gameObject;
        rgbody = GetComponent <Rigidbody2D>();
        movementSpeed = 400f;
        time = 0;
    }
    
    private void FixedUpdate()
    {
        CheckHorizontalMovement();
        CheckJumpMovement();
    }
    
    
    private void Update()
    {
        horizontal = (int)Input.GetAxisRaw("Horizontal");
        isJump = (Input.GetButtonDown("Jump")) ;
    }

    
    
    private void CheckHorizontalMovement()
    {
        if (horizontal == -1 && !waitingForInput)
        {
            waitingForInput = true;
            print("left");
 
            Move(Vector3.left);
            time--;
        }
        if(horizontal == 1 && !waitingForInput)
        {
            waitingForInput = true;
            Move(Vector3.right);
            time++;
        }

        
    }

    private void CheckJumpMovement()
    {
        // jump
        if (isJump && onGround)
        {
            Move(Vector3.up * jumpHeight);
            isJump = false;
        }
        
        //snappy gravity
        if (!onGround)
        {
            Move(Vector3.down);
        }
    }

    private void Move(Vector3 direction)
    {
        StartCoroutine(MoveCoroutine(direction));
    }
    
    IEnumerator MoveCoroutine(Vector3 direction)
    {
        Vector3 currentPosition = gm.transform.position;
        rgbody.velocity = direction * (movementSpeed * Time.deltaTime);
        while (true)
        {
            Vector3 newPosition = gm.transform.position;
            if (Vector3.Distance(currentPosition, newPosition) > Vector3.Magnitude(direction))
            {
                rgbody.velocity = Vector3.zero;
                waitingForInput = false;
                break;
            }

            yield return new WaitForSeconds(0.01f);
        }

    }

    
    private void OnTriggerEnter2D(Collision2D col)
    {
        onGround = true;
    }

    private void OnTriggerExit2D(Collision2D col)
    {
        onGround = false;
    }
}

