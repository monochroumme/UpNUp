using UnityEngine;

public class GroundSpawer : MonoBehaviour
{
    static public Transform playerS;

    public Transform player;
    public GameObject ground;
    public float groundSpeed;
    public int maxLevels;
    public int maxGrounds;

    [HideInInspector]
    public bool gameOver;

    GameObject[,] level;
    GameObject highestlevel;

    void Start()
    {
        gameOver = false;
        ground.GetComponent<PlatformController>().speed = groundSpeed;

        level = new GameObject[maxLevels, maxGrounds];

        playerS = player;

        InstantiateGrounds();

        Invoke("IncreaseSpeed", 15f);
    }

    void Update()
    {
        if (!gameOver)
        {
            for (int l = 0; l < level.GetLength(0); l++)
            {
                if (!level[l, 0].GetComponent<PlatformController>().move)
                {
                    ChangeScales(l);
                    TeleportUp(l);
                    ChangePositions(l);
                    for (int g = 0; g < maxGrounds; g++)
                    {
                        level[l, g].GetComponent<PlatformController>().move = true;
                        level[l, g].GetComponent<PlatformController>().CalculateRaySpacing();
                    }
                    highestlevel = level[l, 0];
                }
            }
        }
    }

    void InstantiateGrounds()
    {
        for (int l = 0; l < level.GetLength(0); l++)
        {
            for (int g = 0; g < level.GetLength(1); g++)
            {
                if (l == 0 && g == 0)
                {
                    InstantiateAllGroundsInLevel(l, new Vector3(0, ScreenSides.down));
                    ChangeScales(l);
                    ChangePositions(l);
                }
                else if (g == 0)
                {
                    InstantiateAllGroundsInLevel(l, new Vector3(0, level[l - 1, g].transform.position.y + Random.Range(Gaps.minY, Gaps.maxY) + ScalesOfGrounds.averageY));
                    ChangeScales(l);
                    ChangePositions(l);
                }

                level[l, g].transform.SetParent(GameObject.Find("Grounds").transform);
                highestlevel = level[l, 0];
            }
        }
    }

    void InstantiateAllGroundsInLevel(int _level, Vector3 place)
    {
        for(int i = 0; i < maxGrounds; i++)
        {
            level[_level, i] = (GameObject)Instantiate(ground, place, Quaternion.identity);
        }
    }

    void ChangeScales(int _level)
    {
        for (int i = 0; i < maxGrounds; i++)
        {
            level[_level, i].transform.localScale = new Vector3(Random.Range(ScalesOfGrounds.minScaleX, ScalesOfGrounds.maxScaleX), Random.Range(ScalesOfGrounds.minScaleY, ScalesOfGrounds.maxScaleY), 1);
        }
    }



    void ChangePositions(int _level)
    {
        for (int i = 0; i < maxGrounds; i++)
        {
            if(i == 0)
                level[_level, i].transform.position = new Vector3(Random.Range(ScreenSides.left - (ScalesOfGrounds.maxScaleX + ScalesOfGrounds.minScaleX) / 2f, ScreenSides.left + (ScalesOfGrounds.maxScaleX + ScalesOfGrounds.minScaleX) / 2f), level[_level, i].transform.position.y);
            else
                level[_level, i].transform.position = new Vector3(level[_level, i-1].transform.position.x + level[_level, i-1].transform.localScale.x / 2f + Random.Range(Gaps.minX, Gaps.maxX) + level[_level, i].transform.localScale.x / 2f, level[_level, i].transform.position.y);
        }
    }

    void TeleportUp(int _level)
    {
        for (int i = 0; i < maxGrounds; i++)
        {
            if (i == 0)
                level[_level, i].transform.position = new Vector3(0, highestlevel.transform.position.y + highestlevel.transform.localScale.y / 2 + Random.Range(Gaps.minY, Gaps.maxY) + level[_level, i].transform.localScale.y / 2);
            else
                level[_level, i].transform.position = new Vector3(0, level[_level, 0].transform.position.y);
        }
    }

    void IncreaseSpeed()
    {
        if (!gameOver)
        {
            if (groundSpeed < 2f)
            {
                groundSpeed += 0.5f;
                Invoke("IncreaseSpeed", 15f);
            }
            else if (groundSpeed == 2f)
            {
                groundSpeed += 0.5f;
                Invoke("IncreaseSpeed", 30f);
            }
            else if (groundSpeed == 2.5f)
            {
                groundSpeed += 0.25f;
                Invoke("IncreaseSpeed", 45f);
            }
            else if (groundSpeed == 2.75f)
            {
                groundSpeed += 0.25f;
                Invoke("IncreaseSpeed", 60f);
            }


            for (int l = 0; l < level.GetLength(0); l++)
            {
                for (int g = 0; g < level.GetLength(1); g++)
                {
                    level[l, g].GetComponent<PlatformController>().speed = groundSpeed;
                }
            }
        }
    }

    static class ScreenSides
    {
        static public float right = Camera.main.orthographicSize * Camera.main.aspect;
        static public float up = Camera.main.orthographicSize;
        static public float left = -right;
        static public float down = -up;
    }

    static class ScalesOfGrounds
    {
        static public float minScaleX = playerS.localScale.x * 2f;
        static public float maxScaleX = playerS.localScale.x * 5f;
        static public float minScaleY = playerS.localScale.y / 3f;
        static public float maxScaleY = playerS.localScale.y / 2f;

        static public float averageX = (minScaleX + maxScaleX) / 2f;
        static public float averageY = (minScaleY + maxScaleY) / 2f;
    }

    static class Gaps
    {
        static public float minX = playerS.localScale.x * 2.1f;
        static public float maxX = playerS.localScale.x * 2.8f;
        static public float minY = playerS.localScale.y * 3f;
        static public float maxY = playerS.localScale.y * 3.9f;
    }
}