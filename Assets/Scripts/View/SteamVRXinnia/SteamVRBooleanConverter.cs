using System;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR;

namespace View.SteamVRXinnia
{
    public class SteamVRBooleanConverter : MonoBehaviour
    {
        [Serializable]
        public class BooleanEvent : UnityEvent<bool>
        {
        }

        public BooleanEvent converted;

        public void Receive(SteamVR_Behaviour_Boolean behaviour, SteamVR_Input_Sources source, bool boolean)
        {
            converted.Invoke(boolean);
        }
    }
}