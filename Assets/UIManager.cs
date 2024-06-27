using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Canvas UICanvas;
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
    public void SetTMP(string TMP_ID,string TMP_VAL)
    {
        if (TMProDicc.ContainsKey(TMP_ID))
        {
            TMProDicc[TMP_ID].text = TMP_VAL;
        }
        else
        {
            Debug.Log("SetTMP: Key not found: " + TMP_ID);
        }
    }

}
