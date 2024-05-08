using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField]TestBulletFactory TestBulletFactory;

    public Dictionary<string, GameObject> FactoryDictionary = new Dictionary<string, GameObject>();

    private void Awake()
    {
        if (Instance)
        {
            Destroy(this);
        }
        Instance = this;
    }

    private void Start()
    {
    }

    public void CreateFactory(string FactoryId, int bulletcount)
    {
        if (FactoryId == "TestPool")
        {
            TestBulletFactory.CreatePool(bulletcount);
        }
    }
}
