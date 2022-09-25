using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fish",
                menuName = "Fish/Create New Fish")]
public class FishBase : ScriptableObject
{
    [SerializeField] string fishName;
    [SerializeField] Sprite sprite;
    [SerializeField] int size;
    [SerializeField] float speed;
    [SerializeField] int pointValue;
    private bool canEatPlayer;
    [SerializeField] int maxCount;

    public int count = 0;

    public void AddFish()
    {
        count++;
    }

    public void RemoveFish()
    {
        count--;
    }

    public int GetCount()
    {
        return count;
    }

    public void SetCount(int newCount)
    {
        count = newCount;
    }

    public int GetMaxCount()
    {
        return maxCount;
    }

    public int GetSize()
    {
        return size;
    }

    public bool GetCanEatPlayer()
    {
        return canEatPlayer;
    }

    public void SetCanEatPlayer(bool canEat)
    {
        canEatPlayer = canEat;
    }

    public string FishName
    {
        get
        {
            return fishName;
        }
    }

    public Sprite Sprite
    {
        get
        {
            return sprite;
        }
    }


    public float Speed
    {
        get
        {
            return speed;
        }
    }

    public int PointValue
    {
        get
        {
            return pointValue;
        }
    }


    
}
