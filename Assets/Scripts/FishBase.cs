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
    [SerializeField] bool canEatPlayer;
    [SerializeField] int maxCount;

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

    public int Size
    {
        get
        {
            return size;
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

    public bool CanEatPlayer
    {
        get
        {
            return canEatPlayer;
        }
    }

    public int MaxCount
    {
        get
        {
            return maxCount;
        }
    }

}
