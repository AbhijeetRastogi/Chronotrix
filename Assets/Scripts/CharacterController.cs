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
    private int horizontal;
    private float timeL;
    private float timeR;
    private bool isJump;
    private bool onGround;
    private int time = 0;
    

    
    public int GetTime()
    {
        return time;
    }
    
    private void Start()    
    {
        gm = gameObject;
        rgbody = GetComponent <Rigidbody2D>();
        timeL = 0.0f;
        timeR = 0.0f;
        movementSpeed = 10f;
        time = 0;
    }
    

    private void Update()
    {
        horizontal = (int)Input.GetAxisRaw("Horizontal");
        isJump = (Input.GetButtonDown("Jump") && onGround) ? true : isJump;
    }

    private void FixedUpdate()
    {
        CheckHorizontalMovement();
        CheckJumpMovement();
    }
    
    private void CheckHorizontalMovement()
    {

        // horizontal 
        timeL += Time.deltaTime;
        timeR += Time.deltaTime;
        
        if (timeL > moveTime && horizontal == -1)
        {
            Move(Vector3.left * movementSpeed);
            time--;
            timeL = 0;
        }
        if(timeR > moveTime && horizontal == 1)
        {
            Move(Vector3.right * movementSpeed);
            time++;
            timeR = 0;
        }

        
    }

    private void CheckJumpMovement()
    {
        // jump
        if (isJump)
        {
            Move(Vector3.up * jumpHeight);
            isJump = false;
        }
        
        //snappy gravity
        if (!onGround)
        {
            Move(Vector3.down );
        }
    }

    private void Move(Vector3 direction)
    {
        StartCoroutine(MoveCoroutine(direction));
    }
    
    IEnumerator MoveCoroutine(Vector3 direction)
    {
        print("moving");
        Vector3 currentPosition = gm.transform.position;
        rgbody.velocity = direction;
        while (true)
        {
            Vector3 newPosition = gm.transform.position;
            if (Vector3.Distance(currentPosition, newPosition) > Vector3.Magnitude(direction))
            {
                rgbody.velocity = Vector3.zero;
            }
            yield return new WaitForSeconds(0.02f);
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

