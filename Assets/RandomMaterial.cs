using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMaterial : MonoBehaviour
{
    public Material[] materials;

    void Start()
    {
        MeshRenderer mr = GetComponent<MeshRenderer>();
        mr.sharedMaterial = materials.Choose();
    }
}
