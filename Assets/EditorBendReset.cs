#if UNITY_EDITOR
using UnityEditor;

using UnityEngine;

[ExecuteInEditMode]
public class EditorBendReset : MonoBehaviour
{
    void Update()
    {
        if(EditorApplication.isPlaying) return;
        Shader.SetGlobalFloat("_Curvature", 0f);
    }
}
#endif