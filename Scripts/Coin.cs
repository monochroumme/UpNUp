using UnityEngine;

public class Coin : MonoBehaviour
{
    ScoreManager sm;

    void Start()
    {
        sm = GameObject.Find("GameManager").GetComponent<ScoreManager>();
    }

    public void CollidedWithPlayer()
    {
        Destroy(gameObject);
        sm.CollectCoin();
    }
}