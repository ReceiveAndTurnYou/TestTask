using UnityEngine;
using Photon.Pun;

public class WaterBallLogic : MonoBehaviour
{
    [SerializeField] private float waterBallMovementSpeed = 30f;
    [SerializeField] private Rigidbody2D rigidBody;

    private void Start()
    {
        //sent waterball flying
        rigidBody.velocity = transform.right * waterBallMovementSpeed;
    }

    private void Update()
    {
        if (transform.position.x > -15 && transform.position.x < 10)
        {
            //in boundaries
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Destroy(gameObject);
        }
    }
}
