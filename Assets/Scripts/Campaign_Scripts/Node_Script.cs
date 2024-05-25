using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Node_Script : MonoBehaviour
{
    [SerializeField]PathFindingManager _PathManager;
    public List<Transform> _Neighbors= new List<Transform>();

    [SerializeField] LayerMask _Obstacles;
    [SerializeField] private float _RayLenght;
    [SerializeField] private Renderer _Renderer;


    [SerializeField] bool StartingNode, EndingNode;


    public Transform NodeTransform;
    public Collider2D Collider;
    public float _Weight;

    private void Awake()
    {
        _PathManager = FindObjectOfType<PathFindingManager>();
        if (!_PathManager._NodeList.Contains(this))
        {
            _PathManager._NodeList.Add(this);
        }
        NodeTransform = GetComponent<Transform>();
        _Renderer = GetComponent<Renderer>();
    }

    private void Start()
    {
        if(StartingNode == true)
        {
            SetNodeType("Start");
        }
        else if(EndingNode== true)
        {
            SetNodeType("End");
        }

        FindNeighbors();
    }

    private void FindNeighbors()
    {
        foreach (Node_Script _CurrentNode in _PathManager._NodeList) 
        {
            if(_CurrentNode.NodeTransform == null || _CurrentNode.NodeTransform.position == this.NodeTransform.position)
            {
                // el nodo analizado no existe o es el mismo que el ejecutor
                continue;
            }
            if(Vector3.Distance(_CurrentNode.NodeTransform.position,this.NodeTransform.position) > _RayLenght)
            {
                // esta muy lejos del nodo actual
                continue;
            }

            if(!InLOS(_CurrentNode.NodeTransform))
            {
                // hay un bloque en el medio
                continue;
            }
            else
            {
                // vecino
                if (!_Neighbors.Contains(_CurrentNode.NodeTransform))
                {
                    _Neighbors.Add(_CurrentNode.NodeTransform);
                }
            }
        }
    }
    private bool InLOS(Transform _currennode)
    {
        Vector3 dir = (_currennode.transform.position - this.NodeTransform.position);


        //calculo fisica si hay algo en el medio
        return !Physics2D.Raycast(this.NodeTransform.position, dir, dir.magnitude, _Obstacles );

    }
    public void SetNodeType(string NodeType)
    {
        if(NodeType == "Starting" || NodeType == "starting" || NodeType == "start" || NodeType =="Start")
        {
            StartingNode = true;
            _PathManager.StartNode = this;
            _Renderer.material.color = Color.blue;
        }
        else if(NodeType == "Ending" || NodeType == "ending" || NodeType == "end" || NodeType == "End")
        {
            EndingNode = true;
            _PathManager.EndNode = this;
            _Renderer.material.color = Color.red;
        }
        else if(NodeType == "Reset"|| NodeType == "reset")
        {
            StartingNode = false;
            EndingNode = false;
            _Renderer.material.color = Color.green;
        }
        else
        {
            Debug.Log("SetNodeType: invalid String, check the call");
        }
    }
}

