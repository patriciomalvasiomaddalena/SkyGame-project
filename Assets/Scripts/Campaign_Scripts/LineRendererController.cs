using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererController : MonoBehaviour
{
    LineRenderer _LineRend;
    public Vector3[] Points;

    private void Awake()
    {
        _LineRend= GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (Points[0] == null && Points[1] == null) 
        {
            return;
        }

        for (int i = 0; i<Points.Length; i++)
        {
            _LineRend.SetPosition(i, Points[i]);
        }
    }


}
