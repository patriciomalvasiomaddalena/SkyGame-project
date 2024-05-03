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

    private void ExpandPool(int CountToExpand)
    {
        for(int i = 0; i < CountToExpand;i++)
        {
            GameObject obj = Instantiate(ThisPoolPrefab);
            obj.transform.parent = this.transform;
            //obj.SetActive(false);
            Pool.Add(obj);
        }
    }

    public GameObject GrabPooledItem()
    {
        GameObject ChosenGrab;
        if(Pool.Count <= 0 )
        {
            ExpandPool(1);
            ChosenGrab = Pool[Pool.Count - 1];
        }
        else
        {
            ChosenGrab = Pool[0];
        }
        ChosenGrab.SetActive(true);
        return ChosenGrab;
    }

    public void AddObjToPool(GameObject Obj)
    {
        if(!Pool.Contains(Obj))
        {
            Pool.Add(Obj);
            Obj.SetActive(false);
        }
    }

    public void RemoveObjFromPool(GameObject Obj)
    {
        if (Pool.Contains(Obj))
        {
            Pool.Remove(Obj);
            Obj.SetActive(true);
        }
    }

}
