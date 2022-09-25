using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    [SerializeField] FishBase fishBase;
    private SpriteRenderer spriteRenderer;
    private bool isGoingRight = true;

    public FishBase FishBase
    {
        get
        {
            return fishBase;
        }
    }

    public bool IsGoingRight
    {
        get
        {
            return isGoingRight;
        }
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = fishBase.Sprite;
        if (!isGoingRight)
        {
            spriteRenderer.flipX = true;
        }
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (isGoingRight)
        {
            transform.position = new Vector3(transform.position.x + fishBase.Speed * Time.deltaTime, transform.position.y);
        }
        else
        {
            transform.position = new Vector3(transform.position.x - fishBase.Speed * Time.deltaTime, transform.position.y);
        }
    }

    public void setIsGoingRight(bool isGoingRight)
    {
        this.isGoingRight = isGoingRight;
    }

    public void flipSprite()
    {
        if (spriteRenderer.flipX)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }
}