using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using Cinemachine;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{

    [SerializeField] private Transform top;
    [SerializeField] private Transform bottom;
    [SerializeField] private int jumpHeight = 3;
    [SerializeField] private float moveTime = 0.3f;
    
    private int horizontal;
    private float atRest;
    private float timeL;
    private float timeR;
    private float time;
    private bool isJump;
    private bool onGround;

    private void Start()
    {
        timeL = 0.0f;
        timeR = 0.0f;
        time = 0.0f;
    }

    // bug - if a person left tap and 2*right tap he can go faster 
    private void Update()
    {
        horizontal = (int) Input.GetAxisRaw("Horizontal");
        isJump = (Input.GetButtonDown("Jump")) ? true : false;
        onGround = Physics2D.OverlapCircle(bottom.position,0.2f,0);
        
        // horizontal 
        timeL += Time.deltaTime;
        timeR += Time.deltaTime;
        time += Time.deltaTime;

        if (timeL > moveTime && horizontal == -1)
        {
            transform.position += new Vector3(horizontal, 0, 0);
            timeL = 0;
        }
        if(timeR > moveTime && horizontal == 1)
        {
            transform.position += new Vector3(horizontal, 0, 0);
            timeR = 0;
        }
            
        // jump
        if (onGround && isJump)
        {
            transform.position += new Vector3(0, 1, 0);
        }
        
        // gravity
        if (!onGround && time > moveTime)
        {
            transform.position += new Vector3(0, -1, 0);
            Debug.Log(onGround);
            
            time = 0;
        }
            
    }

    IEnumerator test()
    {
        yield return new WaitForSeconds(0.1f);
        transform.position += new Vector3(0, -1, 0);
    }

    private void FixedUpdate()
    {

    }
    
}
