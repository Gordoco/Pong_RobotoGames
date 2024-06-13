using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyAIController : AIController
{

    /// <summary>
    /// Implementation of paddle ball following AI
    /// </summary>
    protected override void PaddleAI()
    {
        if (!GM || !GM.GetComponent<GameManager>()) return;
        float directionY = Ball.transform.position.y - transform.position.y;
        Vector3 dir = new Vector3(0, directionY, 0);

        //Smooth AI movement to prevent jitters
        if (dir.magnitude > 0.05f)
        {
            //Allows for easier time defeating the AI
            float paddleSpeedMultplier = 0.6f;
            transform.position += dir.normalized * Time.deltaTime * (paddleSpeed * paddleSpeedMultplier);
        }
    }
}
