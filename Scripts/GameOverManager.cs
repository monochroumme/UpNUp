using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public Transform notDeadHUD;
    public Transform deadHUD;
    public Text finalSecondsAlive;
    public Text finalScore;
    public Text coinsCollected;
    public GameObject ground;

    ScoreManager scoreManager;
    GroundSpawer groundSpawner;
    CoinManager coinManager;

    void Start()
    {
        scoreManager = transform.GetComponent<ScoreManager>();
        groundSpawner = transform.GetComponent<GroundSpawer>();
        coinManager = transform.GetComponent<CoinManager>();
    }

    public void GameOver()
    {
        notDeadHUD.gameObject.SetActive(false);
        deadHUD.gameObject.SetActive(true);
        finalSecondsAlive.text = scoreManager.secondsAlive.ToString();
        finalScore.text = scoreManager.Score.ToString();
        coinsCollected.text = scoreManager.coinsAmount.ToString();
        ground.GetComponent<PlatformController>().speed = 0;
        groundSpawner.gameOver = true;
        coinManager.gameOver = true;
    }

    public void GoBackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }
}