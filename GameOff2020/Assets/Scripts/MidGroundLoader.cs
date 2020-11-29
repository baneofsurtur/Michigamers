using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Loads the midground level into the game and sets is scrolling soeed.
 */
public class MidGroundLoader : MonoBehaviour
{
    public GameObject midGroundObject;
    public Transform midGroundTransformer;
    private LevelHelper levelHelper;
    LevelLoader levelLoader;
    public float time = 0f;

    /*
    * Create midground chunks to account for the full legth of the level 
    * and load them in
    */
    public void loadMidGroundChunks()
    {
        float position = levelHelper.startingPosition;

        while (position <
            levelHelper.levelLength - levelHelper.secondsToUnitsConversion)
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

    // Start is called before the first frame update
    void Start()
    {
        levelLoader = LevelLoader.instance;
        levelHelper = LevelHelper.createLevelHelper(gameObject);
        midGroundTransformer = GetComponent<Transform>();
        time = levelHelper.time;

        loadMidGroundChunks();
    }

    /* Update is called once per frame.
     * Shift x position of midground chunks in the negative direction such that 
     * the midground moves to the left at a rate of 
     * levelHelper.secondsToUnitsConversion / 3 per second.  The division by 
     * three is to make the midground scroll slower than the main level.
     */
    void Update()
    {
        if (levelLoader.endReached)
        {
            time = 0f;
        }
        else
        {
            time = (float)Time.deltaTime / 1f;
        }

        midGroundTransformer.position = new Vector2(
            midGroundTransformer.position.x -
                (time * (levelHelper.secondsToUnitsConversion / 3)),
            midGroundTransformer.position.y
        );
    }
}
