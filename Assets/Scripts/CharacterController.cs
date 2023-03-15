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
    public static event Action<int> OnGround;
    
    [SerializeField] private GameObject top;
    [SerializeField] private GameObject bottom;
    [SerializeField] private int jumpHeight = 3;
    [SerializeField] private float moveTime = 0.3f;
    
    private int horizontal;
    private float atRest;
    private float timeL;
    private float timeR;
    private float time;
    private bool isJump;
    private bool onGround = true;
    private bool isRoof;
    private int currentHeight;


    // private void OnEnable()
    // {
    //     Collision.OnGround += OnGround(false);
    // }
    // private void OnDisable()
    // {
    //     Collision.OnGround -= OnGround(true);
    // }

    private void Start()
    {
        timeL = 0.0f;
        timeR = 0.0f;
        time = 0.0f;
    }
    
    
    private void FixedUpdate()
    {
        horizontal = (int) Input.GetAxisRaw("Horizontal");
        isJump = (Input.GetButtonDown("Jump")) ? true : false;
        
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
        if (onGround && isJump && !isRoof && currentHeight < jumpHeight)
        {
            currentHeight++;
            transform.position += new Vector3(0, 1, 0);
            
        }
        // gravity
        if (!onGround && time > moveTime )
        {
            transform.position += Vector3.down * 1;
            
            time = 0;
        }
         
    }

    public void OnChildTriggerEnter(int whichCollider, Vector3 position)
    {
        if(whichCollider==0)
            onGround = true;
        else
            isRoof = true;
    }

    public void OnChildTriggerExit(int whichCollider, Vector3 position)
    {
        if(whichCollider==0)
            onGround = false;
        else
            isRoof = false;
    }


}

