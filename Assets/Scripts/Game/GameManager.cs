using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //EDITOR VALUES
    [SerializeField] private GameObject LoadLevelPrefab;
    [SerializeField] private TMP_Text LivesLabel;

    [SerializeField] private string[] Levels;
    [SerializeField] private float BallResetTime = 1.5f;

    //PUBLIC VALUES
    public float PaddleSpeed = 10f;

    //PRIVATE VALUES
    private int LevelCount = 0;
    private int Lives = 5;

    public int GetLevelCount() { return LevelCount; } 

    /// <summary>
    /// Handles "scoring" of a point from traditional Pong based on who scored
    /// </summary>
    /// <param name="team"></param>
    public void TriggerScore(Team team)
    {
        if (team == Team.AI)
        {
            if (LevelCount >= Levels.Length) Win();
            else
            {
                LoadLevel.instance.LoadNewLevel(Levels[LevelCount]);
                LevelCount++;
            }
        }
        else
        {
            Lives--;
            LivesLabel.text = "LIVES: " + Lives;

            if (Lives == 0) Lose();
            else StartCoroutine(ResetBall());
        }
    }

    /// <summary>
    /// Loads the win screen and resets game state
    /// </summary>
    private void Win()
    {
        LoadLevel.instance.LoadNewLevel("WinScreen", true);
        Destroy(gameObject);
    }

    /// <summary>
    /// Loads the lose screen and resets game state
    /// </summary>
    private void Lose()
    {
        LoadLevel.instance.LoadNewLevel("LoseScreen", true);
        Destroy(gameObject);
    }

    /// <summary>
    /// Applies a randomized force to the ball to restart play
    /// </summary>
    /// <returns></returns>
    private IEnumerator ResetBall()
    {
        yield return new WaitForSeconds(BallResetTime);
        GameObject Ball = GameObject.FindGameObjectWithTag("Ball");
        Ball.GetComponent<BallPhysics>().RandomizeDirection();
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject LevelLoader = GameObject.FindGameObjectWithTag("LoadLevel");
        if (LevelLoader == null)
        {
            Instantiate(LoadLevelPrefab);
        }
    }

}
