using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using System;

public class PlayerHealthbar : MonoBehaviour
{
    private const float playerHealth = 100;
    private float currentHealth = playerHealth;
    private PhotonView view;
    private const int DAMAGE = 20;

    [SerializeField] private Rigidbody2D rigidBobdy;
    [SerializeField] private GameObject ui;
    [SerializeField] private Image healthbarImage;
    [SerializeField] private GameLogic gameLogic;

    public event EventHandler OnPlayerDestroyed;

    private void Start()
    {
        view = GetComponent<PhotonView>();

        if(!view.IsMine)
        {
            Destroy(ui);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {
            currentHealth -= 20;

            if(currentHealth <=20)
            {
                playerDestroy();
            }
        }
    }

    private void TakeDamage(int damage)
    {
        view.RPC("RPC_TakeDamage", RpcTarget.All, damage);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("WaterBall"))
        {
            TakeDamage(DAMAGE);
        }
    }

    [PunRPC]
    private void RPC_TakeDamage(int damage)
    {

        if(!view.IsMine)
        {
            return;
        }

        currentHealth -= damage;

        healthbarImage.fillAmount = currentHealth / playerHealth;

        if(currentHealth <= 0) 
        {
            playerDestroy();
        }
    }

    private void playerDestroy()
    {
        OnPlayerDestroyed?.Invoke(this, EventArgs.Empty);
        PhotonNetwork.Destroy(gameObject);
    }

}
