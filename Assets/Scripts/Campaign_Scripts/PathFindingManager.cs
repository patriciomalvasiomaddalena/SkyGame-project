using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFindingManager : MonoBehaviour
{

    [Header("Variables")]

    public List<Node_Script> _NodeList = new List<Node_Script>();
    //public List<Tp2_Sentinel> _SentinelList = new List<Tp2_Sentinel>();

    public Node_Script StartNode, EndNode;

    public List<Transform> _Path = new List<Transform>();

    public GameObject _Player;

    public Node_Script _NearestPlayerNode;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && StartNode != null && EndNode != null)
        {
            PathFinding(_Path, StartNode, EndNode);
        }

    }
    public List<Transform> PathFinding(List<Transform> _IaPath, Node_Script NodeStart, Node_Script NodeEnd)
    {
        Node_Script CurrentNode = NodeStart;
        Node_Script CameFromNode = NodeStart;

        if (NodeStart == null || NodeEnd == null)
        {
            Debug.Log("Error, NodeEnd o NodeStart son nulos");
            return null;
        }

        if (CurrentNode == NodeStart)
        {
            _IaPath.Add(CurrentNode.transform);
        }

        Transform winner = null;

        print("Pathfinding...");

        while (CurrentNode != NodeEnd)
        {
            float MinDistance = float.MaxValue;
            foreach (Transform _neighbor in CurrentNode._Neighbors)
            {
                if (CurrentNode._Neighbors == null)
                {
                    Debug.Log("se trato de hacer un pathfinding con un nodo que no posee neighbors, el nodo es: " + CurrentNode.name);
                    break;
                }
                if (CameFromNode.transform == _neighbor.transform)
                {
                    continue;
                }

                float currentDistance = Vector2.Distance(NodeEnd.transform.position, _neighbor.transform.position) * _neighbor.GetComponent<Node_Script>()._Weight;
                if (currentDistance < MinDistance)
                {
                    print("wawa");
                    winner = _neighbor;
                    MinDistance = currentDistance;
                    winner.GetComponent<Renderer>().material.color = Color.cyan;
                }
            }

            _IaPath.Add(winner);
            CameFromNode = CurrentNode;
            CurrentNode = winner.GetComponent<Node_Script>();
        }

        NodeEnd.GetComponent<Renderer>().material.color = Color.red;
        return _IaPath;
    }

    /*public void RaiseAlarm(Tp2_Sentinel Caller)
    {
        foreach(Tp2_Sentinel Guard in _SentinelList)
        {
            if(Guard == Caller)
            {
                continue;
            }
            else
            {
                Guard._Alarmed = true;
            }
        }


    }
    */



}
