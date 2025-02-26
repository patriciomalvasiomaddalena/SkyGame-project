using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class TestBulletFactory : MonoBehaviour
{
    [SerializedDictionary("TestBullet", "FactoryBullet")]
    public static Dictionary<TestBullet,TestBulletFactory> InstanceDictionary = new Dictionary<TestBullet, TestBulletFactory>();

    public TestBullet _bulletPrefab;
    public GameObject _FactoryPrefab;
    [SerializeField] private int initialAmount = 0;

    private Pool<TestBullet> _Pool;

    void Awake()
    {
        if(InstanceDictionary.ContainsKey(_bulletPrefab))
        {
            Destroy(this);
        }
        else
        {
            InstanceDictionary.Add(_bulletPrefab, this);
        }


        //GameManager.Instance.FactoryDictionary.Add(FactoryID, this.gameObject);
        _Pool = new Pool<TestBullet>(CreateObject, TestBullet.TurnOn, TestBullet.TurnOff, initialAmount);
        //this.gameObject.SetActive(false);
    }

    TestBullet CreateObject()
    {
        var TestBullet = Instantiate(_bulletPrefab, this.transform);
        TestBullet.transform.SetParent(this.transform);
        TestBullet._ThisFactory = this;
        return TestBullet;
    }

    public TestBullet GetObjectFromPool()
    {
        return _Pool.GetObject();
    }
    

    //Overloading
    public TestBullet GetObjectFromPool(Vector3 Position)
    {
        TestBullet obj = _Pool.GetObject();
        obj.transform.position = Position;
        obj.transform.SetParent(this.transform);
        return obj;
    }

    public TestBullet GetObjectFromPool(Vector3 Position, Quaternion Rotation)
    {
        TestBullet obj = _Pool.GetObject();
        obj.transform.position = Position;
        obj.transform.rotation = Rotation;
        obj.transform.SetParent(this.transform);
        return obj;
    }

    public GameObject GetGameObjectFromPool()
    {
        GameObject TestBulletGOBJ = _Pool.GetObject().gameObject;
        TestBulletGOBJ.transform.SetParent(this.transform);
        return TestBulletGOBJ;
    }

    public GameObject GetGameObjectFromPool(Vector3 Position)
    {
        GameObject TestBulletGOBJ = _Pool.GetObject().gameObject;
        TestBulletGOBJ.transform.position = Position;
        TestBulletGOBJ.transform.SetParent(this.transform);
        return TestBulletGOBJ;
    }
    public GameObject GetGameObjectFromPool(Vector3 Position, quaternion Rotation)
    {
        GameObject TestBulletGOBJ = _Pool.GetObject().gameObject;
        TestBulletGOBJ.transform.position = Position;
        TestBulletGOBJ.transform.rotation =Rotation;
        TestBulletGOBJ.transform.SetParent(this.transform);
        return TestBulletGOBJ;
    }

    public void ReturnObjectToPool(TestBullet obj)
    {
        _Pool.ReturnObjectToPool(obj);
    }

    public void CreatePool(int NewBulletCount)
    {

    }
}

