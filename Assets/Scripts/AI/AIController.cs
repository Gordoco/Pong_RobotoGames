using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIController : MonoBehaviour
{
    protected GameObject Ball;
    protected float paddleSpeed;
    protected GameObject GM;

    // Start is called before the first frame update
    void Start()
    {
        Ball = GameObject.FindGameObjectWithTag("Ball");
        GM = GameObject.FindGameObjectWithTag("GameManager");

        if (!Ball || !GM) Destroy(this);
        else paddleSpeed = GM.GetComponent<GameManager>().PaddleSpeed;
    }

    protected abstract void PaddleAI();

    // Update is called once per frame
    void Update()
    {
        PaddleAI();
    }
}
