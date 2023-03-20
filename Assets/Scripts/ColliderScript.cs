using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ColliderScript : MonoBehaviour
{
    
    public bool Collision { get; private set; }
    
    
    private void OnTriggerStay2D(Collider2D other)
    {
        Collision = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Collision = false;
    }
}
