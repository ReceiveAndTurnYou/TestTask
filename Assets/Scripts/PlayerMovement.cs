using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{
    // old movement
    /*Vector2 inputVector = new Vector2(0, 0);
    private float movementSpeed = 12f;
    private Rigidbody2D rigidBody;
    private PhotonView view;

    private void Start()
    {
        view = GetComponent<PhotonView>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //input
        if (view.IsMine)
        {
            inputVector.x = Input.GetAxisRaw("Horizontal");
            inputVector.y = Input.GetAxisRaw("Vertical");

            //movement
            rigidBody.MovePosition(rigidBody.position + inputVector.normalized * movementSpeed * Time.deltaTime);
        }
    }*/

    //new input system movement
    private Rigidbody2D rigidBody;
    private float movementSpeed = 5f;
    private PhotonView view;
    private PlayerInputAction playerInputAction;
    private float rotationSpeed = 1000f;
    [SerializeField] private Transform shootingPoint;

    private void Awake()
    {
        view = GetComponent<PhotonView>();
        rigidBody = GetComponent<Rigidbody2D>();
        playerInputAction = new PlayerInputAction();
    }

    private void Update()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            Invoke(nameof(EnablePlayerMovement), 0.1f);
        }
    }

    private void FixedUpdate()
    {
        if (view.IsMine)
        {
            Vector2 inputVector = playerInputAction.Player.Movement.ReadValue<Vector2>();
            rigidBody.MovePosition(rigidBody.position + inputVector.normalized * movementSpeed * Time.deltaTime);
        }
    }

    private void EnablePlayerMovement()
    {
        playerInputAction.Player.Movement.Enable();
    }
}
