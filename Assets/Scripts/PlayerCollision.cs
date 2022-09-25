using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public GameObject gameManagerObject;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = gameManagerObject.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Fish fish = other.gameObject.GetComponent<Fish>();
        if (!fish.FishBase.GetCanEatPlayer())
        {
            gameManager.AddGrowthPoints(fish.FishBase.PointValue);
            gameManager.DestroyFish(other.gameObject);
        } else
        {
            gameManager.LoseGame();
        }
    }
}
