using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static int GAME_HEIGHT = 4320;
    private static int GAME_WIDTH = 7680;

    [SerializeField] GameObject smallOrangeFish;
    public GameObject player;
    public Camera playerCamera;

    // Start is called before the first frame update
    void Start()
    {
        SpawnFish(smallOrangeFish);
        SpawnFish(smallOrangeFish);
        SpawnFish(smallOrangeFish);
        SpawnFish(smallOrangeFish);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnFish(GameObject fishToSpawn)
    {
        int randomNum = Random.Range(0, 2);
        int newFishX;
        int newFishY;
        string direction;
        if (randomNum == 0)
        {
            direction = "LEFT";
            newFishX = GAME_WIDTH / 2;
        } else
        {
            direction = "RIGHT";
            newFishX = -GAME_WIDTH / 2;
        }
        newFishY = Random.Range(-GAME_HEIGHT / 2, GAME_HEIGHT / 2);

        GameObject newFish = Instantiate(fishToSpawn, new Vector3(newFishX, newFishY, 1), Quaternion.identity);
        if (direction.Equals("RIGHT"))
        {
            newFish.GetComponent<Fish>().setIsGoingRight(true);
        } else
        {
            newFish.GetComponent<Fish>().setIsGoingRight(false);
        }
    }
}
