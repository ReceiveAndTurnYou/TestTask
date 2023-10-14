using UnityEngine;
using Photon.Pun;

public class PlayerJoystickUI : MonoBehaviour
{
    [SerializeField] private PhotonView view;
    [SerializeField] private GameObject joystickUI;

    private void Awake()
    {
        if(!view.IsMine)
        {
            Destroy(joystickUI);
        }
    }

}
