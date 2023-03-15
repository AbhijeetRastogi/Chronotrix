using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Collision : MonoBehaviour
{
    [SerializeField] private int type;
    
    private CharacterController characterController;
    
    private void Start()
    {
        characterController = transform.parent.gameObject.GetComponent<CharacterController>();
    }

    void OnTriggerEnter(Collider other) {
        Debug.Log("enter collision");
        characterController.OnChildTriggerEnter(type, transform.position);
    }
    void OnTriggerExit(Collider other) {
        characterController.OnChildTriggerExit(type, transform.position);
    }
}
