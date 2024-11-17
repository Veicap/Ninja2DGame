using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateObject : MonoBehaviour
{
    [SerializeField] private GameObject Coin;
    [SerializeField] private GameObject KunaiGift;
    private enum Gift
    {
        Gold,
        Kunai
    }
    private readonly Gift[] listGift = { Gift.Gold, Gift.Kunai};
    private Gift gift;
    public void SpawnGift()
    {
        //gift = listGift[Random.Range(0, listGift.Length)];
        //Debug.Log(gift);
        gift = listGift[1];
        if(gift == Gift.Gold)
        {
            Instantiate(Coin, transform.position, Quaternion.identity);
        }
        else if (gift == Gift.Kunai)
        {
            Instantiate(KunaiGift, transform.position, Quaternion.identity);
        }
        OnDespawn();
    }
    private void OnDespawn()
    {
        Destroy(gameObject);
    }
}
