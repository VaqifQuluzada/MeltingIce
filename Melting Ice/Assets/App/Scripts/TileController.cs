using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    //the end of tile where other tile joins
    public Transform endPoint;

    //movement speed of tile.
    public float speed;

    //the z position where tile will go when resetting
    public float resetPosition;

    //current obstacle state like city, ruins or etc
    public ObstacleStateSO currentObstacleState;

    //Current obstacle or tiles on the platform.
    [SerializeField] private List<GameObject> currentObstacles=new List<GameObject>();

    GameObject randomCollectible;

    [Space]

    #region CollectibleSpawnControl

    [SerializeField] private GameObject[] collectiblePrefabs;

    [SerializeField] private Vector2[] randomCollectibleSpawnPoints;

    #endregion
    private void MoveTile()
    {
        transform.position += transform.forward * Time.deltaTime * speed;

        if (transform.position.z >= resetPosition)
        {
            GamePlayManager.instance.ChangeParent();
        }
    }

    public void SpawnCollectible()
    {
        if (GamePlayManager.instance.gamePlayState != GamePlayStates.Gaming)
        {
            return;
        }

        if (randomCollectible != null)
        {
            Destroy(randomCollectible.gameObject);
        }

        //This variable is used to increase spawn randomness of obstacles.
        int randomnessGenerator = Random.Range(0, 11);

        //Debug.Log(randomnessGenerator);

        if (randomnessGenerator%2!=0)
        {
            return;
        }

        //This is the index of random collectible.
        int randomCollectibleIndex = Random.Range(0, collectiblePrefabs.Length);

        //and this is the index of random position of collectible.
        int randomCollectiblePosIndex = Random.Range(0, randomCollectibleSpawnPoints.Length);

        Vector3 randomPosition = randomCollectibleSpawnPoints[randomCollectiblePosIndex];

        //The length of our board is 10 unit.
        //randomPosition.z = Random.Range(-10, 0);

        randomCollectible = Instantiate(collectiblePrefabs[randomCollectibleIndex], transform);

        randomCollectible.transform.localPosition = randomPosition;

    }


    public void SpawnObstacleSequence()
    {
        if (GamePlayManager.instance.gamePlayState != GamePlayStates.Gaming)
        {
            return;
        }

        //if we are in the initial state we wont have currentobstaclestate for 5 seconds.
        if (currentObstacleState == null)
        {
            return;
        }
        
        //Before changing our current platform tile we need to destroy all walls and etc on it.
        if (currentObstacles.Count != 0)
        {
            for(int i = 0; i < currentObstacles.Count; i++)
            {
                Destroy(currentObstacles[i].gameObject);
            }

            
        }
        
        //This is for deleting all null fields.
        currentObstacles.Clear();

        StateArrayElement[] reference= currentObstacleState.stateArrayElements;

        //we need to make at least one tile without obstacle. And that notObstacle tiles will be chosen randomly from 
        //number of sides(up down left right)
        int randomEmptyTileIndex = Random.Range(0, reference.Length);

        //This is where we choose walls or side panels for scene. For example, we have ceiling left and 
        //right walls in our walls state.
        for (int i=0;i< reference.Length; i++)
        {
            int randomSpawnIndex = Random.Range(0, reference[i].stateArrayElement.Length);

            //The elements in 0th index is notObstacle Elements.
            if (i == randomEmptyTileIndex)
            {
                randomSpawnIndex = 0;
            }

            //We created the tile
            GameObject stateTileInstance = Instantiate(reference[i].stateArrayElement[randomSpawnIndex], this.transform);


            //and add it to obstacle list of platform for deleting when reset.
            currentObstacles.Add(stateTileInstance);
        }


    }
}
