using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    [SerializeField] private TextMeshProUGUI totalCoinText;

    private void Awake()
    {
        Instance = this;    
    }

    public void SetCoinText(string totalCoinText)
    {
        this.totalCoinText.SetText(totalCoinText);
    }
}
