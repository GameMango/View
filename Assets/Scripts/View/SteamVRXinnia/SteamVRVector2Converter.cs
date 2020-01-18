using System;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR;

namespace View.SteamVRXinnia
{
    public class SteamVRVector2Converter : MonoBehaviour
    {
        [Serializable]
        public class Vector2Event : UnityEvent<Vector2>
        {
        }

        [Serializable]
        public class SingleEvent : UnityEvent<float>
        {
        }

        public Vector2Event converted;
        public SingleEvent convertedXAxis;
        public SingleEvent convertedYAxis;

        public void Receive(SteamVR_Behaviour_Vector2 from, SteamVR_Input_Sources source, Vector2 vector, Vector2 delta)
        {
            converted.Invoke(vector);
            convertedXAxis.Invoke(vector.x);
            convertedYAxis.Invoke(vector.y);
        }
    }
}