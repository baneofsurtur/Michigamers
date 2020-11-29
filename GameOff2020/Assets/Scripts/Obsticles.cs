using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obsticles : MonoBehaviour
{
    public HeartPoints heartPoints;

    private LevelHelper levelHelper;

    void Start()
    {
        levelHelper = LevelHelper.createLevelHelper(gameObject);
        heartPoints = FindObjectOfType(typeof(HeartPoints)) as HeartPoints;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject heartPoint = heartPoints.heartPointsStack.Pop();
        Destroy(heartPoint);

        if (heartPoints.heartPointsStack.Count == 0)
        {
            PlayerInput.gameOver = true;
            //collision.gameObject.GetComponent<PlayerInput>().gameOver = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

    }
}
