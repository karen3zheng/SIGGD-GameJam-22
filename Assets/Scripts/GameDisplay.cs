using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDisplay : MonoBehaviour
{
    public GameObject timeDigit1;
    public GameObject timeDigit2;
    public GameObject timeDigit3;
    public GameObject timeDigit4;

    public GameObject scoreDigit1;
    public GameObject scoreDigit2;
    public GameObject scoreDigit3;

    public Sprite oneDigitRed;
    public Sprite twoDigitRed;
    public Sprite threeDigitRed;
    public Sprite fourDigitRed;
    public Sprite fiveDigitRed;
    public Sprite sixDigitRed;
    public Sprite sevenDigitRed;
    public Sprite eightDigitRed;
    public Sprite nineDigitRed;
    public Sprite zeroDigitRed;
    public Sprite colonDigitRed;

    public Sprite oneDigitWhite;
    public Sprite twoDigitWhite;
    public Sprite threeDigitWhite;
    public Sprite fourDigitWhite;
    public Sprite fiveDigitWhite;
    public Sprite sixDigitWhite;
    public Sprite sevenDigitWhite;
    public Sprite eightDigitWhite;
    public Sprite nineDigitWhite;
    public Sprite zeroDigitWhite;
    public Sprite colonDigitWhite;

    private Coroutine gameTimer;
    int urgentThreshold = 10;

    public GameObject gameManagerObject;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = gameManagerObject.GetComponent<GameManager>();
        DisplayScore(0);
        if (gameTimer == null)
        {
            gameTimer = StartCoroutine(TimeCoroutine(30));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator TimeCoroutine(int seconds)
    {
        while (seconds >= 0)
        {
            DisplayTime(seconds);
            seconds--;
            yield return new WaitForSeconds(1f);
        }
        gameManager.LoseGame();
        gameTimer = null;
    }

    private void DisplayTime(int seconds)
    {
        int extraSeconds = seconds % 60;
        int minutes = seconds / 60;
        bool isUrgent = seconds <= urgentThreshold;

        SetDigit(timeDigit1, minutes, isUrgent);
        SetDigit(timeDigit2, -1, isUrgent);
        SetDigit(timeDigit3, extraSeconds / 10, isUrgent);
        SetDigit(timeDigit4, extraSeconds % 10, isUrgent);
    }

    public void DisplayScore(int score)
    {
        SetDigit(scoreDigit1, score / 100, false);
        SetDigit(scoreDigit2, (score % 100) / 10, false);
        SetDigit(scoreDigit3, (score % 100) % 10, false);
    }

    private void SetDigit(GameObject digitObject, int digit, bool isRed)
    {
        switch (digit)
        {
            case 1:
                digitObject.GetComponent<Image>().sprite = isRed ? oneDigitRed : oneDigitWhite;
                break;
            case 2:
                digitObject.GetComponent<Image>().sprite = isRed ? twoDigitRed : twoDigitWhite;
                break;
            case 3:
                digitObject.GetComponent<Image>().sprite = isRed ? threeDigitRed : threeDigitWhite;
                break;
            case 4:
                digitObject.GetComponent<Image>().sprite = isRed ? fourDigitRed : fourDigitWhite;
                break;
            case 5:
                digitObject.GetComponent<Image>().sprite = isRed ? fiveDigitRed : fiveDigitWhite;
                break;
            case 6:
                digitObject.GetComponent<Image>().sprite = isRed ? sixDigitRed : sixDigitWhite;
                break;
            case 7:
                digitObject.GetComponent<Image>().sprite = isRed ? sevenDigitRed : sevenDigitWhite;
                break;
            case 8:
                digitObject.GetComponent<Image>().sprite = isRed ? eightDigitRed : eightDigitWhite;
                break;
            case 9:
                digitObject.GetComponent<Image>().sprite = isRed ? nineDigitRed : nineDigitWhite;
                break;
            case 0:
                digitObject.GetComponent<Image>().sprite = isRed ? zeroDigitRed : zeroDigitWhite;
                break;
            case -1:
                digitObject.GetComponent<Image>().sprite = isRed ? colonDigitRed : colonDigitWhite;
                break;
            default:
                digitObject.GetComponent<Image>().sprite = null;
                break;
        }
    }

}
