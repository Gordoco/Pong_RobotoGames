using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
//Helper Enum to make team differentiation easy
public enum Team
{
    Player,
    AI
}

[RequireComponent(typeof(Collider))]
public class GoalCollider : MonoBehaviour
{
    //EDITOR VALUES
    [SerializeField] private Team team;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            GameObject Ball = other.gameObject;
            Ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
            Ball.transform.position = new Vector3(0, 0, Ball.transform.position.z);
            GameManager GM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
            GM.TriggerScore(team);
        }
    }
}
