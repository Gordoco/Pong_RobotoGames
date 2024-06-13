using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimicPaddles : MonoBehaviour
{
    //EDITOR VALUES
    [SerializeField] private GameObject WorldLeftPaddle;
    [SerializeField] private GameObject WorldRightPaddle;

    [SerializeField] private GameObject LocalLeftPaddle;
    [SerializeField] private GameObject LocalRightPaddle;
    [SerializeField] private GameObject LocalBall;

    [SerializeField] float WORLD_SPACE_RANGE = 3.88f;
    [SerializeField] float LOCAL_SPACE_RANGE = 0.5f;

    // Start is called before the first frame update
    void Awake()
    {
        if (!WorldLeftPaddle || !WorldRightPaddle || !LocalLeftPaddle || !LocalRightPaddle)
        {
            Debug.Log("[ERROR: MimicPaddles.cs] - Paddle Not Assigned in Inspector");
        }
    }

    /// <summary>
    /// Conversion from world space to relative space within the PongBall prefab
    /// </summary>
    /// <param name="WorldLocation"></param>
    /// <returns></returns>
    private Vector3 ConvertWorldLocationToBallLocation(GameObject Obj, Vector3 WorldLocation)
    {
        float oldRange = (WORLD_SPACE_RANGE - (-WORLD_SPACE_RANGE));
        float newRange = (LOCAL_SPACE_RANGE - (-LOCAL_SPACE_RANGE));
        float newYValue = (((WorldLocation.y - (-WORLD_SPACE_RANGE)) * newRange) / oldRange) + (-LOCAL_SPACE_RANGE);
        float newXValue = (((WorldLocation.x - (-WORLD_SPACE_RANGE)) * newRange) / oldRange) + (-LOCAL_SPACE_RANGE);
        //Z-Value omitted due to nature of the plane

        return new Vector3(newXValue, newYValue, 0);
    }

    // Update is called once per frame
    void Update()
    {
        LocalLeftPaddle.transform.localPosition = ConvertWorldLocationToBallLocation(LocalLeftPaddle, WorldLeftPaddle.transform.position);
        LocalRightPaddle.transform.localPosition = ConvertWorldLocationToBallLocation(LocalRightPaddle, WorldRightPaddle.transform.position);
        LocalBall.transform.localPosition = ConvertWorldLocationToBallLocation(LocalBall, transform.position);
    }
}
