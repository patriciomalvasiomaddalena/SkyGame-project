
using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    PoolManager Instance;
    [System.Serializable]
    public class PoolType
    {
        public string name;
        public GameObject ObjectBlueprint;
        public int SizeofPool;
    }

    private void Awake()
    {
        if(Instance== null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    
    }

    [SerializeField] GameObject PoolPrefab;
    public List<PoolType> poolTypeList = new List<PoolType>();
    [SerializedDictionary]
    public SerializedDictionary<string,PoolFabricator> PoolDictionary = new SerializedDictionary<string,PoolFabricator>();

    private void Start()
    {
        foreach(PoolType PoolType in poolTypeList)
        {
            GameObject CurrentPool = Instantiate(PoolPrefab);
            CurrentPool.SetActive(true); CurrentPool.transform.SetParent(this.transform);

            PoolFabricator CurrentPoolScript = CurrentPool.GetComponent<PoolFabricator>();
            CurrentPoolScript.ThisPoolPrefab = PoolType.ObjectBlueprint;
            CurrentPoolScript.StartPool(PoolType.SizeofPool);
            PoolDictionary.Add(PoolType.name, CurrentPoolScript);
        }
    }
}
