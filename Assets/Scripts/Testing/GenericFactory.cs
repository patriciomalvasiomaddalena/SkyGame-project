using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class GenericFactory<T> : MonoBehaviour where T : MonoBehaviour, Ipoolable<T> 
{
    public static GenericFactory<T> Instance;
    public static GameObject Blueprint;

    public T genericScript;
    public GameObject _PoolPrefab;
    public int _PoolCount;
    private bool Awakened = false;

    private GenericPool<T> _Pool;

    private void Start()
    {
        Awaken();
    }
    public void Awaken()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        if(Awakened == true)
        {
            return;
        }
        Instance = this;

        _Pool = new GenericPool<T>(CreateNewObject,genericScript.TurnOn,genericScript.TurnOff,_PoolCount);
        Awakened = true;
    }

    private T CreateNewObject()
    {
        var TestBullet = Instantiate(_PoolPrefab, this.transform);
        TestBullet.transform.SetParent(this.transform);
        if(TestBullet.TryGetComponent<T>(out T GenericScript))
        { 
                return Instantiate(GenericScript);

        }
        else
        {
            Debug.LogError("Generic factory: CreateNewObject has null reference" + GenericScript);
            return null;
        }
    }
    public T GetObjectFromPool()
    {
        return _Pool.GetObject();
    }


    //Overloading
    public T GetObjectFromPool(Vector3 Position)
    {
        T obj = _Pool.GetObject();
        obj.transform.position = Position;
        return _Pool.GetObject();
    }

    public T GetObjectFromPool(Vector3 Position, Quaternion Rotation)
    {
        T obj = _Pool.GetObject();
        obj.transform.position = Position;
        obj.transform.rotation = Rotation;
        return _Pool.GetObject();
    }

    public GameObject GetGameObbjectFromPool()
    {
        GameObject TestBulletGOBJ = _Pool.GetObject().gameObject;
        if(TestBulletGOBJ != null)
        {
            return TestBulletGOBJ;
        }
        else
        {
            Debug.LogError("Generic list GetGameObject null" + _Pool);
            return null;
        }

    }

    public GameObject GetGameObbjectFromPool(Vector3 Position)
    {
        GameObject TestBulletGOBJ = _Pool.GetObject().gameObject;
        if (TestBulletGOBJ != null)
        {
            TestBulletGOBJ.transform.position = Position;
            return TestBulletGOBJ;
        }
        else
        {
            Debug.LogError("Generic list GetGameObject null" + _Pool);
            return null;
        }
    }
    public GameObject GetGameObbjectFromPool(Vector3 Position, Quaternion Rotation)
    {
        GameObject TestBulletGOBJ = _Pool.GetObject().gameObject;
        TestBulletGOBJ.transform.position = Position; if (TestBulletGOBJ != null)
        {
            TestBulletGOBJ.transform.position = Position;
            TestBulletGOBJ.transform.rotation = Rotation;
            return TestBulletGOBJ;
        }
        else
        {
            Debug.LogError("Generic list GetGameObject null" + _Pool);
            return null;
        }
    }

    public void ReturnObjectToPool(T obj)
    {
        _Pool.ReturnObjectToPool(obj);
    }
}

public class BulletAFactory : GenericFactory<BulletA>
{
    
}

public class BulletA : MonoBehaviour, Ipoolable<BulletA>
{
    public void TurnOn(BulletA GenericScript)
    {
        throw new NotImplementedException();
    }

    public void TurnOff(BulletA GenericScript)
    {
        throw new NotImplementedException();
    }
}