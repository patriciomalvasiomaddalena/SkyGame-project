using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    UIManager Instance;

    public Canvas UICanvas;
    public TMPro.TextMeshProUGUI FuelTMP;

    [SerializedDictionary("TextMeshProID", "TextMeshPro")]
    [SerializeField] SerializedDictionary<string, TMPro.TextMeshProUGUI> TMProDicc = new SerializedDictionary<string, TMPro.TextMeshProUGUI>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    private void Start()
    {
        TMProDicc.Add("FuelID", FuelTMP);
    }

    public void SetTMP(string TMP_ID,string TMP_VAL)
    {
        if (TMProDicc.ContainsKey(TMP_ID))
        {
            TMProDicc[TMP_ID].text += TMP_VAL;
        }
        else
        {
            Debug.Log("SetTMP: Key not found: " + TMP_ID);
        }
    }

}
