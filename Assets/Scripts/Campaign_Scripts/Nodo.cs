using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nodo : MonoBehaviour
{
    public List<Transform> _neighbors = new List<Transform>();
    Node_Script _NodeScript;
    int _cost = 1;
    public int Cost { get { return _cost; } }
    public LayerMask obstacleMask;

    private void Awake()
    {
        _NodeScript= GetComponent<Node_Script>();
    }

    private void Start()
    {
        foreach(Transform nodo in _NodeScript._Neighbors)
        {
            _neighbors.Add(nodo);
        }
    }
    private void OnDrawGizmos()
    {
        foreach (var element in _neighbors)
        {
            Vector3 dir = element.transform.position - transform.position;
            Gizmos.color = Color.white;

            Gizmos.DrawLine(transform.position, element.transform.position);

        }

    }
}
