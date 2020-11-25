using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidGroundLoader : MonoBehaviour
{
    public GameObject midGroundObject;
    public Transform midGroundTransformer;
    private LevelHelper levelHelper;
    public float time = 0f;

    // Start is called before the first frame update
    void Start()
    {
        levelHelper = new LevelHelper();
        midGroundTransformer = GetComponent<Transform>();
        time = levelHelper.time;

        float position = levelHelper.startingPosition;

        while (position < levelHelper.levelLength - levelHelper.secondsToUnitsConversion)
        {
            
            GameObject midGround = Instantiate(
                midGroundObject,
                new Vector3(position, -.8f, 0),
                Quaternion.identity
            );
            midGround.transform.parent =
                               GameObject.Find("MidGround").transform;

            levelHelper.newXScale(midGround, levelHelper.halfWidth * 2);

            position += levelHelper.halfWidth * 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        time = (float)Time.deltaTime / 1f;
        midGroundTransformer.position = new Vector2(
            this.midGroundTransformer.position.x -
                (time * (levelHelper.secondsToUnitsConversion / 3)),
            this.midGroundTransformer.position.y
        );
    }
}
