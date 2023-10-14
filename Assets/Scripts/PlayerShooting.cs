using UnityEngine;
using Photon.Pun;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    // old player shooting
    /* [SerializeField] private Transform shootingPoint;
     [SerializeField] private GameObject waterBallPrefab;
     private bool isCooldown = false;
     private PhotonView view;

     private void Start()
     {
         view = GetComponent<PhotonView>();
     }

     private void Update()
     {
         if (view.IsMine)
         {
             //get input for shooting
             if (Input.GetKeyDown(KeyCode.Space))
             {
                 if (!isCooldown)
                 {
                     Shoot();
                     Invoke("ResetCooldown", 1.0f);
                     isCooldown = true;
                 }
             }
         }
     }

     private void Shoot()
     {
         //photon instantiate 
         PhotonNetwork.Instantiate(waterBallPrefab.name, shootingPoint.position, shootingPoint.rotation);
     }

     private void ResetCooldown()
     {
         isCooldown = false;
     }*/


    // new input system shooting
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private GameObject waterBallPrefab;
    private PlayerInputAction playerInputAction;
    private bool isCooldown = false;
    private PhotonView view;

    private void Awake()
    {
        view = GetComponent<PhotonView>();
        playerInputAction = new PlayerInputAction();
        playerInputAction.Player.Shooting.Enable();
        playerInputAction.Player.Shooting.performed += Shoot;
    }

    public void Shoot(InputAction.CallbackContext context)
    {
        if (view.IsMine && context.performed)
        {
            if (!isCooldown)
            {
                PhotonNetwork.Instantiate(waterBallPrefab.name, shootingPoint.position, shootingPoint.rotation);
                Invoke("ResetCooldown", 1.0f);
                isCooldown = true;
            }
        }
    }

    private void ResetCooldown()
    {
        isCooldown = false;
    }

}
