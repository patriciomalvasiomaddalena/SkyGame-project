using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolFabricator : MonoBehaviour
{
    public GameObject ThisPoolPrefab;

    public List<GameObject> Pool= new List<GameObject>();

    public void StartPool(int SizeOfPool) 
    {
        for (int i = 0; i < SizeOfPool; i++)
        {
            GameObject obj = Instantiate(ThisPoolPrefab);
            obj.transform.parent = this.transform;
            obj.SetActive(false);
            Pool.Add(obj);
        }
    }
        
}
