using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialDuplicator : MonoBehaviour
{
    void Awake()
    {
        MeshRenderer renderer = gameObject.GetComponent<MeshRenderer>();
        renderer.material = new Material(renderer.material);
    }
}