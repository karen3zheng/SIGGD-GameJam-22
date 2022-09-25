using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static int GAME_HEIGHT = 3100;
    private static int GAME_WIDTH = 5500;

    private static int GROWTH_THRESHOLD_1 = 15;
    private static int GROWTH_THRESHOLD_2 = 100;
    private static int GROWTH_THRESHOLD_3 = 700;

    // References to fish prefabs
    [SerializeField] GameObject smallBlueFish;
    [SerializeField] GameObject smallGreenFish;
    [SerializeField] GameObject smallOrangeFish;
    [SerializeField] GameObject smallYellowFish;

    [SerializeField] GameObject medGrayFish;
    [SerializeField] GameObject medPinkFish;
    [SerializeField] GameObject medPurpleFish;

    [SerializeField] GameObject bigGreenFish;
    [SerializeField] GameObject bigRedFish;
    [SerializeField] GameObject bigSharkFish;

    // Array of all fish types
    GameObject[] fishArray;

    // References to game object
    public GameObject player;
    public Camera playerCamera;
    public GameObject fishManager;
    public GameObject display;

    // Game variables
    public int growthPoints;
    public int playerGrowthLevel = 1;

    // Start is called before the first frame update
    void Start()
    {
        fishArray = new GameObject[] { smallBlueFish, smallGreenFish, smallOrangeFish, smallYellowFish, 
                                       medGrayFish, medPinkFish, medPurpleFish,
                                       bigGreenFish, bigRedFish, bigSharkFish };
        for (int i = 0; i < fishArray.Length; i++)
        {
            Fish fish = fishArray[i].gameObject.GetComponent<Fish>();
            fish.FishBase.SetCount(0);
        }
        StartCoroutine(BoundsChecker());
        StartCoroutine(SpawnFishCoroutine(fishArray));
    }

    // Update is called once per frame
    void Update()
    {
        if (CanGrow())
        {
            playerGrowthLevel++;
            player.GetComponent<PlayerZoom>().ZoomOut((int) playerCamera.orthographicSize * 3);
        }

        foreach (Transform child in fishManager.transform) {
            Fish fish = child.gameObject.GetComponent<Fish>();
            if (fish.FishBase.GetSize() > growthPoints)
            {
                fish.FishBase.SetCanEatPlayer(true);
            }
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
        GameObject newFish = Instantiate(fishToSpawn, new Vector3(newFishX, newFishY, 1), Quaternion.identity, fishManager.transform);
        newFish.GetComponent<Fish>().FishBase.AddFish();

        if (direction.Equals("RIGHT"))
        {
            newFish.GetComponent<Fish>().setIsGoingRight(true);
        } else
        {
            newFish.GetComponent<Fish>().setIsGoingRight(false);
        }

    }

    private IEnumerator SpawnFishCoroutine(GameObject[] fishArray)
    {
        // While there are less fish than the max count, spawn a random fish from the array every second
        while (true)
        {
            int randomNum = Random.Range(0, fishArray.Length);
            Fish randomFish = fishArray[randomNum].gameObject.GetComponent<Fish>();
            if (randomFish.FishBase.GetCount() < randomFish.FishBase.GetMaxCount())
            {
                SpawnFish(fishArray[randomNum]);
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    // Flip fish when they get to the edge of the screen
    private IEnumerator BoundsChecker()
    {
        while (true)
        {
            foreach (Transform child in fishManager.transform)
            {
                GameObject fish = child.gameObject;
                Fish fishComponent = fish.GetComponent<Fish>();
                if (fishComponent.IsGoingRight && fish.transform.position.x > GAME_WIDTH / 2)
                {
                    fishComponent.setIsGoingRight(false);
                    fishComponent.flipSprite();
                }
                else if (fish.transform.position.x < -GAME_WIDTH / 2)
                {
                    fishComponent.setIsGoingRight(true);
                    fishComponent.flipSprite();
                }
            }

            yield return new WaitForFixedUpdate();
        }
    }

    public void DestroyFish(GameObject fish)
    {
        fish.GetComponent<Fish>().FishBase.RemoveFish();
        Destroy(fish);
    }

    public void AddGrowthPoints(int amount)
    {
        growthPoints += amount;
        display.GetComponent<GameDisplay>().DisplayScore(growthPoints);
        if (growthPoints > GROWTH_THRESHOLD_3)
        {
            WinGame();
        }
    }

    public bool CanGrow()
    {
        if (playerGrowthLevel == 1 && growthPoints >= GROWTH_THRESHOLD_1)
        {
            return true;
        } else if (playerGrowthLevel == 2 && growthPoints >= GROWTH_THRESHOLD_2)
        {
            return true;
        } else if (playerGrowthLevel == 3 && growthPoints >= GROWTH_THRESHOLD_3)
        {
            return true;
        }
        return false;
    }

    public void LoseGame()
    {
        Debug.Log("Game Over");
        player.SetActive(false);
    }

    public void WinGame()
    {
        Debug.Log("You win");
        player.SetActive(false);
    }
}
