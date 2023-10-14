using UnityEngine;
using Photon.Pun;

public class GameLogic : MonoBehaviour
{
    [SerializeField] private GameObject waitingForOtherPlayersTitle;

    private void Update()
    {
        int howManyPlayers = PhotonNetwork.CurrentRoom.PlayerCount;
        if (howManyPlayers >= 2)
        {
            Invoke(nameof(TurnOffTitle), 0.1f);
        }
    }

    private void TurnOffTitle()
    {
        waitingForOtherPlayersTitle.SetActive(false);
    }
}
