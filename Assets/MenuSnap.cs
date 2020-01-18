using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MenuSnap : MonoBehaviour
{
    public string scene;
    public UnityEvent triggered = new UnityEvent();
    public GameObject placeholderStart;
    public Quaternion startRotation;
    public Vector3 startTransformation;
    public AudioSource audioSource;
    
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
            triggered.Invoke();
            //If the GameObject's name matches the one you suggest, output this message in the console
            //letter = collider.gameObject;
            collider.transform.position = startTransformation;
            collider.transform.rotation = startRotation;
            collider.attachedRigidbody.useGravity = false;
            collider.attachedRigidbody.constraints = RigidbodyConstraints.FreezeAll;
            collider.attachedRigidbody.velocity = Vector3.zero;
            audioSource.Play();
        }
    }

    public void SwitchScene()
    {
        SceneManager.LoadScene(scene);
    }
}
