using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorComp : MonoBehaviour
{
    [SerializeField] SpriteRenderer _SpriteRend;

    private void Awake()
    {
        _SpriteRend = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        movement._movementDelegate += ChangeColor;
        LifeComp._TotalPlayerDeath += ChangeColor;
        LifeComp._TakeDamageEvent += ChangeColor;
    }

    private void ChangeColor(Color NewColor)
    {
        _SpriteRend.color= NewColor;
    }

}
