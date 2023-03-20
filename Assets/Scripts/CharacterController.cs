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

    [SerializeField] private GameObject colliderChecker;
    [SerializeField] private int jumpHeight = 3;
    [SerializeField] private float moveTime = 0.3f;

    private GameObject gm;
    private Rigidbody2D rgbody;
    private ColliderScript colliderScript;
    private float movementSpeed;
    private int horizontal;
    private float timeL;
    private float timeR;
    private bool isJump;
    private bool onGround;
    public bool isWaiting;


    public int Time { get; private set; } = 0;

    private void Start()
    {
        gm = gameObject;
        rgbody = colliderChecker.GetComponent<Rigidbody2D>();
        movementSpeed = 40f;
        Time = 0;

        colliderScript = (ColliderScript)colliderChecker.GetComponent(typeof(ColliderScript));
    }

    private void LateUpdate()
    {
        HorizontalMovement();
        //JumpMovement();
    }


    private void Update()
    {
        horizontal = (int)Input.GetAxisRaw("Horizontal");
        isJump = (Input.GetButtonDown("Jump"));
    }



    private void HorizontalMovement()
    {
        if (horizontal == -1 && !isWaiting)
        {
            isWaiting = true;
            Move(Vector3.left);
            Time--;
        }

        if (horizontal == 1 && !isWaiting)
        {
            isWaiting = true;
            Move(Vector3.right);
            Time++;
        }

        
        rgbody.MovePosition(transform.position + Vector3.down);
        onGround = !CheckIfCanMove();

        // jump
        if (isJump && onGround && !isWaiting)
        {
            isWaiting = true;
            Move(Vector3.up * jumpHeight);
            isJump = false;
        }

        //snappy gravity
        else if(!  && !isWaiting)
        {
            isWaiting = true;
            Move(Vector3.down);
        }
    }

    private void Move(Vector3 direction)
    {
        StartCoroutine(MoveCoroutine2(direction));
    }

    private bool CheckIfCanMove()
    {
        return (colliderScript.Collision) ? false : true;
    }

    IEnumerator MoveCoroutine2(Vector3 direction)
    {
        
        rgbody.MovePosition(transform.position + direction);
        print(CheckIfCanMove());
        if (CheckIfCanMove())
        {
            transform.position += direction;
            print("is moving");
        }

        yield return new WaitForSeconds(1f);
        isWaiting = false;

    }

}

