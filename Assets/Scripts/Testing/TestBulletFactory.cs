using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class TestBulletFactory : MonoBehaviour
{
    public static TestBulletFactory Instance { get; private set; }

    [SerializeField] private TestBullet _bulletPrefab;
    [SerializeField] private GameObject _FactoryPrefab;
    [SerializeField] private int initialAmount = 0;

    private Pool<TestBullet> _Pool;

    void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        //GameManager.Instance.FactoryDictionary.Add(FactoryID, this.gameObject);
        _Pool = new Pool<TestBullet>(CreateObject, TestBullet.TurnOn, TestBullet.TurnOff, initialAmount);
        //this.gameObject.SetActive(false);
    }

    TestBullet CreateObject()
    {
        var TestBullet = Instantiate(_bulletPrefab, this.transform);
        TestBullet.transform.SetParent(this.transform);
        return Instantiate(_bulletPrefab);
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
        return _Pool.GetObject();
    }

    public TestBullet GetObjectFromPool(Vector3 Position, Quaternion Rotation)
    {
        TestBullet obj = _Pool.GetObject();
        obj.transform.position = Position;
        obj.transform.rotation = Rotation;
        return _Pool.GetObject();
    }

    public GameObject GetGameObbjectFromPool()
    {
        GameObject TestBulletGOBJ = _Pool.GetObject().gameObject;
        return TestBulletGOBJ;
    }

    public GameObject GetGameObbjectFromPool(Vector3 Position)
    {
        GameObject TestBulletGOBJ = _Pool.GetObject().gameObject;
        TestBulletGOBJ.transform.position = Position;
        return TestBulletGOBJ;
    }
    public GameObject GetGameObbjectFromPool(Vector3 Position, quaternion Rotation)
    {
        GameObject TestBulletGOBJ = _Pool.GetObject().gameObject;
        TestBulletGOBJ.transform.position = Position;
        TestBulletGOBJ.transform.rotation =Rotation;
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

