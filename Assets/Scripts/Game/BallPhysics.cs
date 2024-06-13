using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
public class BallPhysics : MonoBehaviour
{
    //EDITOR VALUES
    [SerializeField] private float BallSpeed = 10.0f;

    //PRIVATE VALUES
    private Rigidbody rb;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Generates a random initial direction preventing horizontal, vertical, or diagonal trajectories.
    /// </summary>
    private void AssignRandomInitialVelocity()
    {
        //Weighted random value prefering left <-> right movement
        Vector2 RandomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-0.6f, 0.6f));
        RandomDirection.Normalize();
        rb.AddForce(RandomDirection * BallSpeed);
    }

    private void Update()
    {
        //Ensures velocity will never be so steep that the ball takes forever to traverse the play area
        float speed = rb.velocity.magnitude;
        Vector3 direction = rb.velocity;
        direction.Normalize();
        if (Mathf.Abs(direction.y) > 0.5f)
        {
            rb.velocity = (new Vector3(Mathf.Sign(direction.x) * 0.5f, Mathf.Sign(direction.y) * 0.5f, direction.z)).normalized * speed;
        }
    }

    /// <summary>
    /// Public accessor method for allowing the Game Manager to reset the ball's direction
    /// </summary>
    public void RandomizeDirection()
    {
        AssignRandomInitialVelocity();
    }

    private void Start()
    {
        StartCoroutine(StartDelay());
    }

    private IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(1.5f);
        AssignRandomInitialVelocity();
    }
}
