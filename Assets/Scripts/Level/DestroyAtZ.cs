using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAtZ : MonoBehaviour
{
    public float posZ;

    void Update()
    {
        if(transform.position.z < posZ)
        {
            Destroy(gameObject);
        }
    }
}
