using System;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR;

namespace View.SteamVRXinnia
{
    public class SteamVRSingleConverter: MonoBehaviour
    {
        [Serializable]
        public class SingleEvent : UnityEvent<float>
        {
        }

        public SingleEvent converted;

        public void Receive(SteamVR_Behaviour_Single single, SteamVR_Input_Sources source, float newAxis, float newDelta)
        {
            converted.Invoke(newAxis);
        }
    }
}