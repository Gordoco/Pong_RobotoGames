using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    //PUBLIC VALUES
    public static LoadLevel instance; //singleton

    //PRIVATE VALUES
    private bool bPanning = false;
    private bool bFinal = false;
    private bool bChangedLevel = false;

    //Saves reset positions between scene changes
    private Vector3 PreviousPlayerPosition = Vector3.zero;
    private Vector3 PreviousEnemyPosition = Vector3.zero;
    private Vector3 PreviousBallPosition = Vector3.zero;

    //For use within Update to smooth camera pan movement
    private Vector3 initPosition;
    private GameObject positionObj;
    private string levelName;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
        instance = this;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.isLoaded && bChangedLevel && !bFinal)
        {
            StartCoroutine(DelayedObjectPlacement());
        }
    }

    /// <summary>
    /// Simple coroutine to delay the replacement of objects until the first frame of the level
    /// </summary>
    /// <returns></returns>
    private IEnumerator DelayedObjectPlacement()
    {
        yield return new WaitForEndOfFrame();
        GameObject mockLargeBall = GameObject.FindGameObjectWithTag("MockLargeBall");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
        GameObject ball = GameObject.FindGameObjectWithTag("Ball");

        player.transform.position = PreviousPlayerPosition;
        enemy.transform.position = PreviousEnemyPosition;
        ball.transform.position = PreviousBallPosition;

        bChangedLevel = false;
    }

    /// <summary>
    /// Loads the requested level either through a pan down one level of recursion (ie. into the ball), or up (out of the ball)
    /// </summary>
    /// <param name="levelName"></param>
    /// <param name="down"></param>
    public void LoadNewLevel(string levelName, bool final = false)
    {
        if (bPanning) return;

        bFinal = final; //Marks this for deletion before complete game state reset

        GameObject panLocation = null;
        panLocation = GameObject.FindGameObjectWithTag("CameraPanSpot");
        if (panLocation) {
            initPosition = Camera.main.transform.position;
            positionObj = panLocation;
            this.levelName = levelName;
            bPanning = true;
        }
        else { Debug.Log("[ERROR: PlayerController.cs] - Camera Pan Location not Found."); }
    }

    //CameraPan variables for convienience
    float PanInterval = 0.75f;
    float t = 0;

    /// <summary>
    /// Handles the logic for panning the camera into a recursive Pong level within Update
    /// </summary>
    private void CameraPan()
    {
        Camera.main.transform.position = Vector3.Lerp(initPosition, positionObj.transform.position, t);
        t += PanInterval * Time.deltaTime;
        if (t >= 1)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
            GameObject ball = GameObject.FindGameObjectWithTag("Ball");

            PreviousBallPosition = ball.transform.position;
            PreviousPlayerPosition = player.transform.position;
            PreviousEnemyPosition = enemy.transform.position;

            if (!bFinal) bChangedLevel = true;

            SceneManager.LoadScene(levelName);
            bPanning = false;
        }
    }

    private void Update()
    {
        if (bPanning)
        {
            CameraPan();
        }
        else t = 0;
    }
}
