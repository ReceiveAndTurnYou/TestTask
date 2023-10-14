using UnityEngine;
using Photon.Pun;

public class Gold : MonoBehaviour
{

    [SerializeField] private PhotonView goldView;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            GoldTaken();
        }
    }

    private void GoldTaken()
    {
        goldView.RPC("RPC_GoldTaken", RpcTarget.All);
    }

    [PunRPC]
    private void RPC_GoldTaken()
    {
        if(!goldView.IsMine)
        {
            return;
        }

        GoldDestroy();

    }

    private void GoldDestroy()
    {
        PhotonNetwork.Destroy(gameObject);
    }
}
