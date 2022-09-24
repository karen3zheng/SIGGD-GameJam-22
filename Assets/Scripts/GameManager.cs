using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static int GAME_HEIGHT = 4320;
    private static int GAME_WIDTH = 7680;

    // References to fish prefabs
    [SerializeField] GameObject smallBlueFish;
    [SerializeField] GameObject smallGreenFish;
    [SerializeField] GameObject smallOrangeFish;
    [SerializeField] GameObject smallYellowFish;

    // Array of all small fish prefabs
    GameObject[] smallFishArray;

    // List to hold all created fish objects (???)
    ArrayList allFishList = new ArrayList();

    // References to player and camera
    public GameObject player;
    public Camera playerCamera;

    private Coroutine fishSpawner;
    int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        smallFishArray = new GameObject[] { smallBlueFish, smallGreenFish, smallOrangeFish, smallYellowFish };
    }

    // Update is called once per frame
    void Update()
    {
        // Start fish spawner if it is not already spawning
        if (fishSpawner == null)
        {
            fishSpawner = StartCoroutine(SpawnFishCoroutine(smallFishArray));
        }
    }

    public void SpawnFish(GameObject fishToSpawn)
    {
        // Choose a random direction and position to spawn the fish
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

        // Instantiate fish object
        GameObject newFish = Instantiate(fishToSpawn, new Vector3(newFishX, newFishY, 1), Quaternion.identity);
        if (direction.Equals("RIGHT"))
        {
            newFish.GetComponent<Fish>().setIsGoingRight(true);
        } else
        {
            newFish.GetComponent<Fish>().setIsGoingRight(false);
        }

        // Add instantiated fish object to list
        allFishList.Add(newFish);
    }

    private IEnumerator SpawnFishCoroutine(GameObject[] fishArray)
    {
        // While there are less fish than the max count, spawn a random fish from the array every second
        while (count < fishArray[0].gameObject.GetComponent<Fish>().FishBase.MaxCount)
        {
            int randomNum = Random.Range(0, fishArray.Length);
            SpawnFish(fishArray[randomNum]);
            count++;
            yield return new WaitForSeconds(1f);
        }
        fishSpawner = null;
    }

    // Remove fish that go off the screen
    private void CleanFish()
    {
        foreach (GameObject fish in allFishList) {
            if (fish.GetComponent<Fish>().IsGoingRight && fish.transform.position.x > GAME_WIDTH / 2)
            {
                allFishList.Remove(fish);
                Destroy(fish);
            } else if (fish.transform.position.x < -GAME_WIDTH / 2)
            {
                allFishList.Remove(fish);
                Destroy(fish);
            }
        }
    }
}
