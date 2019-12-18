using System;
using UnityEngine;

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
    private Color[] _lerpColors = new Color[6];

    private void Start()
    {
        for (int i = 0; i < changes.Length; i++)
        {
            if (changes[i].active)
                _lerpColors[i] = changes[i].with;
        }

        UpdateShaderGlobals();
    }

    private void UpdateShaderGlobals()
    {
        for (int i = 0; i < changes.Length; i++)
        {
            ColorChange change = changes[i];
            Shader.SetGlobalInt("_REPLACE_COLOR_" + (i + 1), 1); // change.active ? 1 : 0
            Shader.SetGlobalColor("_TO_REPLACE_COLOR_" + (i + 1), change.replace);
            Shader.SetGlobalColor("_REPLACE_WITH_COLOR_" + (i + 1), _lerpColors[i]);
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            UpdateShaderGlobals();
        }
    }

    private void Update()
    {
        for (int i = 0; i < changes.Length; i++)
        {
            if (changes[i].active)
            {
                _lerpColors[i] = changes[i].with;
            }
            else
            {
                _lerpColors[i] = Color.Lerp(_lerpColors[i], changes[i].replace, Time.deltaTime);
            }
        }
        UpdateShaderGlobals();
    }
}