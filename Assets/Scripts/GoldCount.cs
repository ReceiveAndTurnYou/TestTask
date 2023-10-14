using UnityEngine;
using TMPro;
using Photon.Pun;
using System;

public class GoldCount : MonoBehaviour
{

    [SerializeField] private GameObject ui;
    [SerializeField] private TMP_Text text;
    [SerializeField] private PhotonView view;
    [SerializeField] private Rigidbody2D rigidBody;

    private const int GOLDVALUE = 1;
    private int currentGold = 0;

    public event EventHandler OnGoldPickUp;

    private void Start()
    {
        if (!view.IsMine)
        {
            Destroy(ui);
            Destroy(text);
        }
    }

    public void IncreaseGold(int increaseValue)
    {
        view.RPC("RPC_IncreaseGold", RpcTarget.All, increaseValue);
    }

    [PunRPC]
    private void RPC_IncreaseGold(int increaseValue)
    {
        if (!view.IsMine)
        {
            return;
        }

        currentGold += increaseValue;
        text.text = "Gold collected: " + currentGold.ToString();
        Debug.Log("IncreaseGold called");

        OnGoldPickUp?.Invoke(this, EventArgs.Empty);

        Debug.Log(currentGold);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(view.IsMine && other.gameObject.layer == LayerMask.NameToLayer("Gold"))
        {
            IncreaseGold(GOLDVALUE);
        }
    }

    /////
    ///

    public int GetCurrentGold()
    {
        return currentGold;
    }
}
