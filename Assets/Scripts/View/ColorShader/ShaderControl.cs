using System;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

namespace View.ColorShader
{
    public class ShaderControl : MonoBehaviour
    {
        [Serializable]
        public struct ColorChange
        {
            public bool active;
            public Color replace;
            public Color with;
        }

        public ColorChange[] changes = new ColorChange[6];

        private void Start()
        {
            UpdateShaderGlobals();
        }

        private void UpdateShaderGlobals()
        {
            for (var i = 0; i < changes.Length; i++)
            {
                var change = changes[i];
                Shader.SetGlobalInt("_REPLACE_COLOR_" + (i + 1), change.active ? 1 : 0);
                Shader.SetGlobalColor("_TO_REPLACE_COLOR_" + (i + 1), change.replace);
                Shader.SetGlobalColor("_REPLACE_WITH_COLOR_" + (i + 1), change.with);
            }
        }

        public void Enable(int ind)
        {
            changes[ind].active = true;
            UpdateShaderGlobals();
        }

        public void Disable(int ind)
        {
            changes[ind].active = false;
            UpdateShaderGlobals();
        }
    }
}