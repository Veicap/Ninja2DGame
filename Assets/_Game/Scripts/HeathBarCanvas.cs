using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeathBarCanvas : MonoBehaviour
{
    [SerializeField] private Character character;
    [SerializeField] private Image healthImage;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float timeToRest = 0.5f;
    private float counter;
    private void Awake()
    {
        counter = timeToRest;
    }
    private void Update()
    {
        if(!character.IsDeath)
        {
            healthImage.fillAmount = Mathf.Lerp(healthImage.fillAmount, character.HP / character.MaxHP, Time.deltaTime * 5f);
            transform.position = character.transform.position + offset;
        }
        else
        {
            if(character is Player)
            {
                counter -= Time.deltaTime;
                if(counter < 0)
                {
                    Hide();
                    counter = timeToRest;
                }
            }
            else
            {
                counter -= Time.deltaTime;
                if (counter < 0)
                {
                    OnDespawn();
                }
            }
        }
    }
    public void OnDespawn()
    {
        Destroy(gameObject);
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
