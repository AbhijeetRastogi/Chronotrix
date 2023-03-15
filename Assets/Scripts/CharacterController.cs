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
    
    private int horizontal;
    private float timeL;
    private float timeR;
    private bool isJump;
    private bool onGround = true;
    

    private void Start()    
    {
        timeL = 0.0f;
        timeR = 0.0f;
        
    }


    private void Update()
    {
        horizontal = (int)Input.GetAxisRaw("Horizontal");
        isJump = (Input.GetButtonDown("Jump")) ? true : false;
    }

    private void FixedUpdate()
    {
        // horizontal 
        timeL += Time.deltaTime;
        timeR += Time.deltaTime;
        
        if (timeL > moveTime && horizontal == -1)
        {
            transform.Translate(Vector3.left);
            timeL = 0;
        }
        if(timeR > moveTime && horizontal == 1)
        {
            transform.Translate(Vector3.right);
            timeR = 0;
        }

        // jump
        if (onGround && isJump)
        {
            transform.Translate(Vector3.up * jumpHeight);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            onGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            onGround = false;
        }
    }
}

