using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenMap : MonoBehaviour
{
    [SerializeField] private GameObject hiddenMap;
    private void Start()
    {
        Show();
    }
    public void Show()
    {
        hiddenMap.SetActive(true);
    }
    public void Hide()
    {
        hiddenMap.SetActive(false);
    }
}
