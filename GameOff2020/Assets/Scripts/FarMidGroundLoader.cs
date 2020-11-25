using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarMidGroundLoader : MonoBehaviour
{
    public GameObject farMidGroundObject;
    public Transform farMidGroundTransformer;
    private LevelHelper levelHelper;
    public float time = 0f;

    // Start is called before the first frame update
    void Start()
    {
        levelHelper = new LevelHelper();
        farMidGroundTransformer = GetComponent<Transform>();
        time = levelHelper.time;

        float position = levelHelper.startingPosition;
        while (position <
            levelHelper.levelLength - levelHelper.secondsToUnitsConversion)
        {


            GameObject farMidGround = Instantiate(
                farMidGroundObject,
                new Vector3(position, -.2f, 0),
                Quaternion.identity
            );
            farMidGround.transform.parent =
                              GameObject.Find("FarMidGround").transform;

            levelHelper.newXScale(farMidGround, levelHelper.halfWidth * 2);

            position += levelHelper.halfWidth * 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        time = (float)Time.deltaTime / 1f;
        farMidGroundTransformer.position = new Vector2(
           this.farMidGroundTransformer.position.x -
                (time * (levelHelper.secondsToUnitsConversion / 4)),
           this.farMidGroundTransformer.position.y
       );
    }
}
