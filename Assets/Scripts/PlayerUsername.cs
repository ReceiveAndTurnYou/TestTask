using UnityEngine;
using Photon.Pun;
using TMPro;

public class PlayerUsername : MonoBehaviour
{

    [SerializeField] private PhotonView view;
    [SerializeField] private TMP_Text text;

    private void Start()
    {
        text.text = view.Owner.NickName;
    }

}
