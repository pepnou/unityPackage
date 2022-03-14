using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    [SerializeField]
    private float fov;
    [SerializeField]
    private float distance;

    private float ratio;

    private Camera mainCamera;
    private Vector3 initialPos;

    

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        initialPos = mainCamera.transform.position;

        //GameObject parent = new GameObject("parent");
        //transform.parent = parent.transform;

        ratio = ComputeSize(fov, distance) / ComputeSize(fov, 1f);
        transform.localScale /= ratio;

        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dep = mainCamera.transform.position - initialPos;
        Vector3 newPos = dep - dep / ratio;
        transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);
    }

    private float ComputeSize(float fov, float dst)
    {
        return 2 * Mathf.Tan(Mathf.Deg2Rad * fov / 2) * dst;
    }
}
