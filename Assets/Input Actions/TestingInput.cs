using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestingInput : MonoBehaviour
{
    //input
    private Rigidbody2D rigidBody;
    private float movementSpeed = 5f;

    //shooting
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private GameObject waterBallPrefab;
    private PlayerInputAction playerInputAction;
    private bool isCooldown = false;
    private PhotonView view;

    private void Awake()
    {
        view = GetComponent<PhotonView>();
        rigidBody = GetComponent<Rigidbody2D>();
        playerInputAction = new PlayerInputAction();
        playerInputAction.Player.Enable();
        playerInputAction.Player.Shooting.performed += Shoot;
    }

    //shooting

    public void Shoot(InputAction.CallbackContext context)
    {
        if(view.IsMine && context.performed)
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


    // input
    
    private void FixedUpdate()
    {
        Vector2 inputVector = playerInputAction.Player.Movement.ReadValue<Vector2>();
        rigidBody.MovePosition(rigidBody.position + inputVector.normalized * movementSpeed * Time.deltaTime);
    }
}
