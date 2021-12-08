using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GamePlayManager : MonoBehaviour
{
    //This is the singleton of our gameplay manager.
    public static GamePlayManager instance;

    //This is the point where our firstplatform start to spawn.
    [SerializeField] private Transform tileStartPoint;

    //Number of tiles(for setting in options menu)
    [Range(0,10)]
    [SerializeField] private int numberOfTiles;


    [SerializeField] private GameObject tilePrefab;

    //We add all of our tiles to this list and change order.
    [SerializeField] private List<GameObject> tileArray = new List<GameObject>();

    private int currentCoinCount = 0;
    [Space]
    #region ObstacleSpawnControl

    [SerializeField] private ObstacleStateSO[] obstacleStates;

    [SerializeField] private ObstacleStateSO currentObstacleState;

    [SerializeField] private int timeLimit = 5;

    public GamePlayStates gamePlayState;



    #endregion


    [SerializeField] private TextMeshProUGUI scoreText;
    #region Gameplay Features
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartCoroutine(ChangeObstacleState(timeLimit));

        //we create each tiles.
        for(int i = 0; i < numberOfTiles; i++)
        {
            GameObject tileInstance = Instantiate(tilePrefab);

            tileInstance.name = tileArray.Count.ToString();
            
            //if we already have a tile in array we need to set remaining tiles to the end of this tiles.
            if (tileArray.Count > 0)
            {
                tileInstance.transform.position = tileArray[tileArray.Count - 1].GetComponent<TileController>().endPoint.position;
                tileInstance.transform.parent = tileArray[0].transform;
            }
            else
            {
                //if this is first tile we need to move it.
                tileInstance.transform.position = tileStartPoint.position;

                tileInstance.GetComponent<TileController>().InvokeRepeating("MoveTile", 0, 0.01f);
            }

            //We need to add all tiles in our array to control their order.
            tileArray.Add(tileInstance);
        }
    }

    


    //when tile reach the "teleport point" where swap happens
    public void ChangeParent()
    {
        //Set the reference to parent tile.
        GameObject temp = tileArray[0];

        //we remove parents of all tiles couze change the parent to second tile in array.
        foreach(GameObject tile in tileArray)
        {
            tile.transform.parent = null;
        }
        
        //stop the movement of parent.
        temp.GetComponent<TileController>().CancelInvoke("MoveTile");

        //set the previous parent to the end of the last tile in array.
        temp.transform.position = tileArray[tileArray.Count - 1].GetComponent<TileController>().endPoint.position;

        //for the first 10 seconds there will be no obstacle(cooldown state for beginning)
        if (currentObstacleState != null)
        {
            //change the obstacle state of tile.
            temp.GetComponent<TileController>().currentObstacleState = currentObstacleState;

            temp.GetComponent<TileController>().SpawnObstacleSequence();

            temp.GetComponent<TileController>().SpawnCollectible();
        }
        

        //we need to add previous parent to the end of array so we remove then add it.
        tileArray.Remove(temp);

        tileArray.Add(temp);

        //change the parent to second tile(or next parent)
        for(int i = 1; i < tileArray.Count; i++)
        {
            tileArray[i].transform.parent = tileArray[0].transform;
        }

        //and make this second or current parent move.
        tileArray[0].GetComponent<TileController>().InvokeRepeating("MoveTile", 0, 0.01f);
        
    }

    /// <summary>
    /// This method is used for changing different obstacle states like in temple run(walls, forest, and etc)
    /// </summary>
    /// <param name="randomTimer"></param>
    /// <returns></returns>
    IEnumerator  ChangeObstacleState(int randomTimer)
    {
        yield return new WaitForSecondsRealtime(randomTimer);

        int randomObstacleStateIndex = Random.Range(0, obstacleStates.Length);

        currentObstacleState = obstacleStates[randomObstacleStateIndex];

        timeLimit = Random.Range(5, 120);

        StartCoroutine(ChangeObstacleState(timeLimit));
    }

    #endregion

    public void IncreaseCoinCount()
    {
        currentCoinCount++;

        scoreText.text = currentCoinCount.ToString();
    }

    public int ReturnCoinCount()
    {
        return currentCoinCount;
    }
    public void onGameOver()
    {
        Time.timeScale = 0;
        gamePlayState = GamePlayStates.GameOver;
        GetComponentInChildren<GameCanvasController>().GameOver();
    }
}

public enum GamePlayStates { GameStart,GameOver,Gaming}