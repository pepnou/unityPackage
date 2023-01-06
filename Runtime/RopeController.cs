using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeController : MonoBehaviour
{
    [SerializeField] int segmentNum = 10;
    [SerializeField] int iterations = 10;

    [SerializeField] float gravity = 10f;
    [SerializeField] float length_mult = 1f;

    Vector3[] newPos;
    Vector3[] oldPos;

    LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.positionCount = segmentNum;
        newPos = new Vector3[segmentNum];
        oldPos = new Vector3[segmentNum];

        for (int i = 0; i < segmentNum; i++)
        {
            oldPos[i] = transform.position + transform.rotation * Vector3.forward * 0.01f;
            newPos[i] = transform.position + transform.rotation * Vector3.forward * 0.01f;
        }

        lineRenderer.SetPositions(newPos);
    }

    // Update is called once per frame
    void Update()
    {
        newPos[0] = lineRenderer.GetPosition(0);
        newPos[segmentNum - 1] = lineRenderer.GetPosition(segmentNum - 1);

        float length = Vector3.Distance(newPos[0], newPos[segmentNum - 1]) * length_mult / segmentNum;

        //simulate
        for (int i = 0; i < segmentNum; i++)
        {
            if(i > 0 && i < segmentNum - 1)
            {
                Vector3 prev = newPos[i];

                newPos[i] += newPos[i] - oldPos[i];
                newPos[i] += Vector3.down * gravity * Time.deltaTime * Time.deltaTime;

                oldPos[i] = prev;
            }
        }

        //constraints
        for (int j = 0; j < iterations; j++)
        {
            for (int i = 0; i < segmentNum - 1; i++)
            {
                Vector3 center = (newPos[i] + newPos[i + 1]) / 2;
                Vector3 dir = (newPos[i] - newPos[i + 1]).normalized;

                if (i > 0)
                    newPos[i] = center + dir * length / 2;
                if (i < segmentNum - 2)
                    newPos[i + 1] = center - dir * length / 2;
            }
        }

        lineRenderer.SetPositions(newPos);
    }
}
