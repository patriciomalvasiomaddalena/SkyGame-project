 using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Hull_Piece : MonoBehaviour
{
    [SerializeField] GameObject[] _NeighborSnapPoints = new GameObject[4];
    [SerializeField] List<Transform> _SnappedPoint = new List<Transform>();

    [SerializeField] private float _Health;

    private void Awake()
    {
        Debug.Log("yes");
    }

    private void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("triggered!");
        if (collision != null && collision.CompareTag("SnapPoint"))
        {
            _SnappedPoint.Add(collision.transform);
            this.transform.position = _SnappedPoint[0].transform.position;
        }
    }

    public void SetHealth(float NewHealth)
    {
        _Health = NewHealth;
    } 

    public float GetHealth()
    {
        return _Health;
    }


    public void TakeDamage(float DMG)
    {
        if (_Health - DMG > 0)
        {
            _Health -= DMG;
        }
        else
        {
            Death();
        }
    }

    private void Death()
    {

    }

    private void CheckConnected()
    {
        if(_SnappedPoint.Count <= 0)
        {
            Death();
        }
    }
}
