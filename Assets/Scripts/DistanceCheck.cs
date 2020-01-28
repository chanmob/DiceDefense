using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceCheck : MonoBehaviour
{
    public Transform tr;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 offset = tr.position - transform.position;
        float sqrDistance = offset.sqrMagnitude;

        Debug.Log(sqrDistance);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
