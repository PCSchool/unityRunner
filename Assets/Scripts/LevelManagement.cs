using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagement : MonoBehaviour {

    public GameObject Player;
    public GameObject Banana, Pineapple, Watermelon;
    public GameObject Fence, Barrel;
    public GameObject SpeedUp, SpeedDown;
    public GameObject Terrain, Road, TerrainLeft, TerrainRight, Turn;
    private float x, y, z, timer;
    public bool gameOver = false;
    public int scoreAmount;
    private bool terrainUp = true;
    private float timeSpeed, counter;
    private bool speedUpAdded, speedDownAdded;
    private GameObject lastTerrain;

    // Use this for initialization
    void Start() {
        x = 0f;
        y = 0f;
        z = 16f;
        counter = 1f;
        timer = 20f;
        generateNewPart();
        generateNewPart();
        generateNewPart();
        generateNewPart();
        generateNewPart();
        generateNewPart();
    }

    // Update is called once per frame

    public void endGame()
    {
        if (!gameOver)
        {
            gameOver = true;
        }
    }

    public void updateScore(int score)
    {
        scoreAmount += score;
    }

    public void generateNewPart()
    {
        generateEnvironment();
        generateItems();
    }

    private void generateEnvironment()
    {
        if(lastTerrain == null) { lastTerrain = Terrain; }
        Vector3 newPosition = new Vector3(lastTerrain.transform.position.x, lastTerrain.transform.position.y, (lastTerrain.transform.position.z  + 15.8f));
        transform.rotation = Quaternion.Euler(0, 0, 0);
        GameObject environment = Instantiate(Terrain, newPosition, transform.rotation);
        Destroy(environment, 20f);
        lastTerrain = environment;

        z = lastTerrain.transform.position.z;
        x = lastTerrain.transform.position.x;
        y = lastTerrain.transform.position.y;
        
    }

    private void generateItems()
    {
        Vector3 newPosition = new Vector3(x, y, z);

        int max = Random.Range(8, 12);
        int obstacles = 0;
        for (int row = 0; row < max; row = row + 2)
        {
            int random = Random.Range(0, 1000);
            Debug.Log(random);
            if (random <= 500)
            {
                newPosition = new Vector3(Random.Range(-1, 2), y, z + row);
                generateObstacle(newPosition);
                obstacles++;
                //Debug.Log("GENERATE Obstacle. T x = " + x + ", y = " + y + ", z = " + z + " COUNTER:  " + counter);
            }
            else if (random >= 501)
            {
                //Debug.Log("GENERATE Fruit. T x = " + x + ", y = " + y + ", z = " + z + " COUNTER:  " + counter);
                newPosition = new Vector3(Random.Range(-1, 2), y + 1f, z + row);
                generateFruit(newPosition);
            }

        }
    }

    private void generateFruit(Vector3 newPosition)
    {
        GameObject newFruit;
        switch (Random.Range(0, 3)){
            case 0:
                newFruit = Instantiate(Banana, newPosition, transform.rotation);
                break;
            case 1:
                newFruit = Instantiate(Watermelon, newPosition, transform.rotation);
                break;
            default:
                newFruit = Instantiate(Pineapple, newPosition, transform.rotation);
                break;
        }
        newFruit.transform.parent = lastTerrain.transform;
        Destroy(newFruit, timer);
    }

    private void generateSpeedUp(Vector3 newPosition)
    {
        GameObject speedUp = Instantiate(SpeedUp, newPosition, transform.rotation);
        speedUp.transform.parent = lastTerrain.transform;
        Destroy(speedUp, timer);
    }

    private void generateSpeedDown(Vector3 newPosition)
    {
        GameObject speedDown = Instantiate(SpeedDown, newPosition, transform.rotation);
        speedDown.transform.parent = lastTerrain.transform;
        Destroy(speedDown, timer);
    }

    private void generateObstacle(Vector3 newPosition)
    {
        GameObject newObstacle;
        switch (Random.Range(0, 3))
        {
            case 0:
                newObstacle = Instantiate(Fence, newPosition, transform.rotation);
                break;
            case 1:
                newObstacle = Instantiate(Barrel, newPosition, transform.rotation);
                break;
            default:
                newObstacle = Instantiate(Fence, newPosition, transform.rotation);
                break;
        }
        newObstacle.transform.parent = lastTerrain.transform;
        Destroy(newObstacle, timer);
    }
}
