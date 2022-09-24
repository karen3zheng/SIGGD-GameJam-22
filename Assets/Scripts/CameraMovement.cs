using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject player;
    public float speed;

    void Start()
    {
        StartCoroutine(FollowPlayer());
    }

    IEnumerator FollowPlayer()
    {
        Vector2 cameraPosition;
        Vector2 playerPosition;
        Vector3 distance;

        while (true)
        {
            cameraPosition = new Vector2(transform.position.x, transform.position.y);
            playerPosition = new Vector2(player.transform.position.x, player.transform.position.y);
            distance = new Vector3(playerPosition.x - cameraPosition.x, playerPosition.y - cameraPosition.y, 0f);

            transform.Translate(distance * speed * Time.deltaTime);

            yield return new WaitForFixedUpdate();
        }
    }
}
