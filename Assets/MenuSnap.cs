using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MenuSnap : MonoBehaviour
{
    
    public UnityEvent triggered = new UnityEvent();
    public GameObject placeholderStart;
    public Quaternion startRotation;
    public Vector3 startTransformation;
    
    private void Start()
    {
        startTransformation = placeholderStart.transform.position;
        startRotation = placeholderStart.transform.rotation;
    }

    void OnTriggerEnter(Collider collider)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collider.gameObject.name == "LetterT")
        {
            //If the GameObject's name matches the one you suggest, output this message in the console
            Debug.Log("Start Menu");
            //letter = collider.gameObject;
            collider.transform.position = startTransformation;
            collider.transform.rotation = startRotation;
            collider.attachedRigidbody.useGravity = false;
            triggered.Invoke();
        }
    }
}
