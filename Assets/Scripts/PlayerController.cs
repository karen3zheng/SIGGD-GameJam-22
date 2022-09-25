using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(Movement());
    }

    IEnumerator Movement()
    {
        float moveSpeed = 10f;
        int frameCount = 0;
        float oldHorizontal = 0f;
        float oldVertical = 0f;
        float smooth = 5f;

        while (true)
        {
            
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            int moveCheck;
            float angle;

            frameCount++;

            //Checks if there's any user input
            if (horizontalInput != 0 || verticalInput != 0)
            {
                moveCheck = 1;
            }
            else
            {
                moveCheck = 0;
            }


            if (moveCheck == 1)
            {
                //Pulsing motion, uses frame count to check
                if (frameCount % 80 != 0)
                {
                    moveSpeed *= 0.9885f;
                }
                //Speed boost
                else
                {
                    moveSpeed = 750f;
                    frameCount = 0;
                }
            }
            //Slowing down and maintaining direction
            else
            {
                moveSpeed *= 0.9999999f;
                horizontalInput = oldHorizontal;
                verticalInput = oldVertical;
            }
            
            //Calculates angle from positive y axis
            if (horizontalInput < 0f)
            {
                angle = Vector2.Angle(new Vector2(horizontalInput, verticalInput), new Vector2(0f, 1f));
            }
            else
            {
                angle = -Vector2.Angle(new Vector2(-horizontalInput, verticalInput), new Vector2(0f, 1f));
            }

            //Saves current input for use in next loop
            oldHorizontal = horizontalInput;
            oldVertical = verticalInput;

            //Calculates, then interpolates rotation
            Quaternion target = Quaternion.Euler(0, 0, angle);
            transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);

            //Sets direction vector, either directly up or nothing
            Vector3 direction = new Vector3(0, moveCheck, 0);
            
            //Sets velocity
            transform.Translate(direction * moveSpeed * Time.deltaTime);

            animator.SetFloat("moveSpeed", moveSpeed);
            
            //Waits to run until the next fixed update
            yield return new WaitForFixedUpdate();
        }
    }  
}
