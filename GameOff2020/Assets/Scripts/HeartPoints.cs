/* Copyright Michigamers 11/29/2020.  All rights reserved */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPoints : MonoBehaviour
{
    public GameObject heartPointObject;
    public Stack<GameObject> heartPointsStack;

    private int maxHealth = 3;
    private LevelHelper levelHelper;
    

    // Start is called before the first frame update
    void Start()
    {
        levelHelper = LevelHelper.createLevelHelper(gameObject);

        GameObject heartPoint = Instantiate(
            heartPointObject,
            new Vector3(
                0,
                0,
                0
            ),
            Quaternion.identity
        );

        float heartPointWidth = heartPoint.GetComponent<Collider2D>().bounds.size.x;
        float heartPointHeight = heartPoint.GetComponent<Collider2D>().bounds.size.y;
        heartPoint.transform.position = new Vector2(
            -levelHelper.halfWidth + heartPointWidth,
            levelHelper.halfHeight - heartPointHeight
        );

        heartPointsStack = new Stack<GameObject>();
        heartPointsStack.Push(heartPoint);
        int addedHearts = 1;
        while (addedHearts < maxHealth)
        {
            GameObject newHeartPoint = Instantiate(
                heartPointObject,
                new Vector3(
                    -levelHelper.halfWidth + heartPointWidth + addedHearts,
                    levelHelper.halfHeight - heartPointHeight,
                    0
                ),
                Quaternion.identity
            );

            heartPointsStack.Push(newHeartPoint);
            addedHearts++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
