using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBehavior : MonoBehaviour
{
    public static void ChangeScene(int value)
    {
        Debug.Log("switching scene");
        SceneManager.LoadScene(value);
    }
}
