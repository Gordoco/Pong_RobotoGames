using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //EDITOR VALUES
    [SerializeField] GameObject PongBall;

    //PRIVATE VALUES
    private float paddleSpeed;

    // Start is called before the first frame update
    void Start()
    {
        if (!PongBall)
        {
            Destroy(this);
        }
        GameObject GM = GameObject.FindGameObjectWithTag("GameManager");
        paddleSpeed = GM.GetComponent<GameManager>().PaddleSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("UP"))
        {
            transform.position += Vector3.up * Time.deltaTime * paddleSpeed;
        }
        if (Input.GetButton("DOWN"))
        {
            transform.position -= Vector3.up * Time.deltaTime * paddleSpeed;
        }
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.88f, 3.88f), transform.position.z);
    }

}
