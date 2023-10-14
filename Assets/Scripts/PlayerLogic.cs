using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class PlayerLogic : MonoBehaviourPunCallbacks
{
    private ExitGames.Client.Photon.Hashtable playersAliveProperty = new ExitGames.Client.Photon.Hashtable();
    private PlayerHealthbar playerHealthbar;

    [SerializeField] private PhotonView view;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private TMP_Text playerNameText;
    [SerializeField] private TMP_Text goldCollectedByPlayerText;

    //test
    int temporary;
    [SerializeField] private GoldCount goldCount;

    private void Start()
    {
        if (view.IsMine)
        {
            playerHealthbar = GetComponent<PlayerHealthbar>();
            playerHealthbar.OnPlayerDestroyed += PlayerHealthbar_OnPlayerDestroyed;
            goldCount = GetComponent<GoldCount>();
            goldCount.OnGoldPickUp += GoldCount_OnGoldPickUp;
        }

        playersAliveProperty["PlayersAlive"] = PhotonNetwork.CurrentRoom.PlayerCount;
        PhotonNetwork.CurrentRoom.SetCustomProperties(playersAliveProperty);

        InvokeRepeating(nameof(CheckPlayersAlive), 5, 3);
    }

    private void GoldCount_OnGoldPickUp(object sender, System.EventArgs e)
    {
        if (view.IsMine)
        {
            temporary = goldCount.GetCurrentGold();
        }
    }

    private void PlayerHealthbar_OnPlayerDestroyed(object sender, System.EventArgs e)
    {
        view.RPC(nameof(RPC_SyncPlayerAliveCount), RpcTarget.AllBuffered);
    }

    private void CheckPlayersAlive()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount > 1 && 
            PhotonNetwork.CurrentRoom.CustomProperties.ContainsValue(1))
        {
            Debug.Log("One player alive");
            if(view.IsMine)
            {
                winScreen.SetActive(true);
                playerNameText.text = "Player name: " + view.Owner.NickName;
                goldCollectedByPlayerText.text = temporary.ToString() + " gold collected";
            }
        }
        else
        {
            Debug.Log("More than one player alive");
        }
    }

    [PunRPC]
    private void RPC_SyncPlayerAliveCount()
    {
        Debug.Log("SYNCPLAYERALIVECOUNT");
        int temp = (int)playersAliveProperty["PlayersAlive"] - 1;
        playersAliveProperty["PlayersAlive"] = temp;
        PhotonNetwork.CurrentRoom.SetCustomProperties(playersAliveProperty);
    }
}
