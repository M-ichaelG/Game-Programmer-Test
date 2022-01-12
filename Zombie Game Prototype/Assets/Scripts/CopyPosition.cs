using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyPosition : MonoBehaviour
{
    public Transform trCopyFrom;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = trCopyFrom.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = trCopyFrom.position;
    }
}
