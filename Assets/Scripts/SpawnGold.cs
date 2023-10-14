using UnityEngine;
using Photon.Pun;

public class SpawnGold : MonoBehaviour
{
    [SerializeField] private GameObject goldPrefab;

    private float minX = -7;
    private float maxX = 7;
    private float minY = -2;
    private float maxY = 2;

    private void Awake()
    {
        int howManyPlayers = PhotonNetwork.CurrentRoom.PlayerCount;

        if(howManyPlayers == 2)
        {
            SpawnGoldOnMap();
        }
    }

    private void SpawnGoldOnMap()
    {
        for (int i = 0; i < 5; i++)
        {
            Vector2 randomPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
            PhotonNetwork.Instantiate(goldPrefab.name, randomPosition, Quaternion.identity);
        }
    }
}
