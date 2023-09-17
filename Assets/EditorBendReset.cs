#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class EditorBendReset : MonoBehaviour
{
    void Update()
    {
        Shader.SetGlobalFloat("_Curvature", 0f);
    }
}
#endif