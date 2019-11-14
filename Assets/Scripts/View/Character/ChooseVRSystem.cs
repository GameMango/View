using System;
using UnityEngine;
using UnityEngine.XR;

namespace View.Character
{
    public class ChooseVRSystem : MonoBehaviour
    {
        public enum VRSystem
        {
            Oculus,
            UnityXR
        }

        [Serializable]
        public struct VRSystemGameObjects
        {
            public VRSystem system;
            public GameObject[] gameObjects;
        }

        public VRSystemGameObjects[] systems;

        private void Awake()
        {
            VRSystem search = VRSystem.UnityXR;
            switch (XRSettings.loadedDeviceName)
            {
                case "Oculus":
                    search = VRSystem.Oculus;
                    break;
            }

            foreach (var systemGameObject in systems)
            {
                if (systemGameObject.system != search) continue;
                foreach (var o in systemGameObject.gameObjects)
                {
                    o.SetActive(true);
                }
                break;
            }
        }
    }
}