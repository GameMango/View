using System;
using System.Collections;
using System.Collections.Generic;
using Boo.Lang;
using UnityEngine;
using View.Character;
using VRTK.Prefabs.Interactions.Interactors.ComponentTags;

public class AudioZone : MonoBehaviour
{
    public AudioSource ambient;
    private void Start()
    {
        ambient = GameObject.FindGameObjectWithTag("Ambient").GetComponent<AudioSource>();
    }
    
    private bool isInside = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<VRCharacterController>() != null)
        {
            ambient.mute = true;
            isInside = !isInside;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<VRCharacterController>() != null)
        {
            ambient.mute = false;
        }
        isInside = !isInside;
    }
}
