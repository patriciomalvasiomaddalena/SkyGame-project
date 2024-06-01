using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleLineDrawer : MonoBehaviour
{
    [SerializeField] LineRenderer _CircleRenderer;
    public float _Radius;
    [SerializeField] int _Subdivisions;

    private void Start()
    {
        _CircleRenderer = GetComponent<LineRenderer>();
    }

    public void DrawCircle()
    {
        float _angleStep = 2f * Mathf.PI / _Subdivisions;
        _CircleRenderer.positionCount = _Subdivisions;

        for(int i = 0; i < _Subdivisions; i++)
        {
            float Xposition = _Radius * Mathf.Cos(_angleStep * i);
            float Yposition = _Radius * Mathf.Sin(_angleStep * i);

            Vector3 PointPos = new Vector3(Xposition,Yposition, 0);

            _CircleRenderer.SetPosition(i,PointPos);
        }
    }



}
