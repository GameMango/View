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
        public Color[] current;

        private void Start()
        {
            current = new Color[changes.Length];
            for (int i = 0; i < changes.Length; i++)
            {
                current[i] = changes[i].with;
            }
            UpdateShaderGlobals();
        }

        private void UpdateShaderGlobals(float delta = 0)
        {
            for (var i = 0; i < changes.Length; i++)
            {
                var change = changes[i];
                if (!change.active)
                    current[i] = Color.Lerp(current[i], change.replace, delta);
                else
                    current[i] = change.with;
//                Shader.SetGlobalInt("_REPLACE_COLOR_" + (i + 1), change.active ? 1 : 0);
                Shader.SetGlobalInt("_REPLACE_COLOR_" + (i + 1), 1);
                Shader.SetGlobalColor("_TO_REPLACE_COLOR_" + (i + 1), change.replace);
                Shader.SetGlobalColor("_REPLACE_WITH_COLOR_" + (i + 1),
                    change.active ? change.with : current[i]);
            }
        }

        private void Update()
        {
            UpdateShaderGlobals(Time.deltaTime);
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