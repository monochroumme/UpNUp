using UnityEngine;
using System.Collections.Generic;

public class CoinManager : MonoBehaviour
{
    public GameObject coin;
    public float speed;

    List<GameObject> coins;
    float screenUp;

    [HideInInspector]
    public bool gameOver;

    void Start()
    {
        gameOver = false;
        screenUp = Camera.main.orthographicSize;
        coins = new List<GameObject>();
        InvokeRepeating("GenerateCoin", Random.Range(10f, 20f), Random.Range(5f, 15f));
    }

    void Update()
    {
        foreach (GameObject _coin in coins)
        {
            if (_coin != null)
            {
                _coin.transform.Translate(Vector3.down * speed * Time.deltaTime);

                if (_coin.transform.position.y < -screenUp - coin.transform.localScale.y)
                {
                    Destroy(_coin);
                }
            }
        }
    }

    void GenerateCoin()
    {
        float screenRight = Camera.main.orthographicSize * Camera.main.aspect;

        if(!gameOver)
            coins.Add((GameObject)Instantiate(coin, new Vector3(Random.Range(-screenRight + coin.transform.localScale.x, screenRight - coin.transform.localScale.x), screenUp + coin.transform.localScale.y), Quaternion.identity));
    }
}