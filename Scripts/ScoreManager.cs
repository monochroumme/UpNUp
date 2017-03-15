using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreNum;

    [HideInInspector]
    public float secondsAlive;

    [HideInInspector]
    public float coinsAmount;

    public float Score
    {
        get { return secondsAlive * 25 + coinsAmount * 100; }
    }

    void Start()
    {
        secondsAlive = 0f;

        scoreNum.text = secondsAlive.ToString();

        InvokeRepeating("UpdateScore", 1f, 1f);
    }

    void UpdateScore()
    {
        secondsAlive += 1f;
        scoreNum.text = secondsAlive.ToString();
    }

    public void CollectCoin()
    {
        coinsAmount++;
    }
}