using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseRadar : MonoBehaviour
{
    [SerializeField] private Transform pulseTransform;
    [SerializeField] private float range;
    [SerializeField] private float rangeMax;
    [SerializeField] private float rangeSpeed ;
    [SerializeField] private float fadeRange;
    private SpriteRenderer pulseSprite;
    private Color pulseColor;
    private List<Collider2D> pingedColliderCheck;

    [SerializeField] private Transform pinRadar;
    [SerializeField] LayerMask radarLayerMask;

    
    private void Awake()
    {
        pulseTransform = transform.Find("Circle");
        rangeMax =10f ;
        fadeRange=0;
        rangeSpeed = 7.6f;
        pingedColliderCheck = new List<Collider2D>();
      
    }
    void Update()
    {
       
        range += rangeSpeed * Time.deltaTime;
        if (range > rangeMax)
        {
            range = 0f;
            pingedColliderCheck.Clear();
        }
        pulseTransform.localScale = new Vector3(range, range);

        RaycastHit2D[] raycastHit2DArray = Physics2D.CircleCastAll(transform.position, range / 2f, Vector2.zero, 0f, radarLayerMask);
        foreach (RaycastHit2D raycastHit2D in raycastHit2DArray)
        {
            if (raycastHit2D.collider != null)
            {
                // golpea algo
                if (!pingedColliderCheck.Contains(raycastHit2D.collider))
                {
                    pingedColliderCheck.Add(raycastHit2D.collider);

                    Transform radarPingTransform = Instantiate(pinRadar, raycastHit2D.point, Quaternion.identity);
                    RadarPing radarPing = radarPingTransform.GetComponent<RadarPing>();
                    if (raycastHit2D.collider.gameObject.name == "Player_Fleet" )
                    {
                        // golpea una ciudad
                        radarPing.SetColor(new Color(0, .6f, 0.0408f));
                    } if (raycastHit2D.collider.gameObject.name == "Node" )
                    {
                        // golpea una ciudad
                        radarPing.SetColor(new Color(0.0546f, 0.8018f, 0.0408f));
                    }
                    if (raycastHit2D.collider.gameObject.name == "Enemy_Fleet")
                    {
                        // golpea un enemigo
                        radarPing.SetColor(new Color(0.8f, 0.97f, 0.0392f));
                    }
                    radarPing.SetDisappearTimer(rangeMax / rangeSpeed * 1.5f);
                }
            }
        }
    }
}
