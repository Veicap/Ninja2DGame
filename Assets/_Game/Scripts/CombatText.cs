using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CombatText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    public void OnInit(int damage)
    {
        text.SetText(damage.ToString());
        Invoke(nameof(OnDespawn), 1f);

    }
    public void OnDespawn()
    {
        Destroy(gameObject);
    }
}
