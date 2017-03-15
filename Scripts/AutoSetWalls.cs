using UnityEngine;
using System.Collections;

public class AutoSetWalls : MonoBehaviour
{
    Transform leftWall, rightWall, upWall;

    float screenRight;

    void Start()
    {
        rightWall = transform.GetChild(0);
        leftWall = transform.GetChild(1);
        upWall = transform.GetChild(2);

        screenRight = Camera.main.aspect * Camera.main.orthographicSize;

        //Changing scales
        leftWall.localScale = new Vector3(0.5f, Camera.main.orthographicSize * 2 + 2.5f, 1);
        rightWall.localScale = leftWall.localScale;
        upWall.localScale = new Vector3(screenRight * 2 + 0.5f, 0.5f, 1);

        //Setting positions
        leftWall.position = new Vector3(-screenRight - leftWall.localScale.x/2, -1);
        rightWall.position = new Vector3(screenRight + rightWall.localScale.x/2, -1);
        upWall.position = new Vector3(0, Camera.main.orthographicSize + upWall.localScale.y);
    }
}