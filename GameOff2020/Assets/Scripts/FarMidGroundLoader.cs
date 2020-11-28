using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarMidGroundLoader : MonoBehaviour
{
    public GameObject farMidGroundObject;
    public Transform farMidGroundTransformer;
    private LevelHelper levelHelper;
    public float time = 0f;

    /*
    * Create far-midground chunks to account for the full legth of the level 
    * and load them in
    */
    public void loadFarMidGroundChunks()
    {
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

    // Start is called before the first frame update
    void Start()
    {
        levelHelper = LevelHelper.createLevelHelper(gameObject);
        farMidGroundTransformer = GetComponent<Transform>();
        time = levelHelper.time;

        loadFarMidGroundChunks();
    }

   /* Update is called once per frame.
    * Shift x position of far-midground chunks in the negative direction 
    * such that the midground moves to the left at a rate of 
    * levelHelper.secondsToUnitsConversion / 4 per second.  The division by 
    * four is to make the far-midground scroll slower than the 
    * main level and midground.
    */
    void Update()
    {
        time = (float)Time.deltaTime / 1f;
        farMidGroundTransformer.position = new Vector2(
           farMidGroundTransformer.position.x -
                (time * (levelHelper.secondsToUnitsConversion / 4)),
           farMidGroundTransformer.position.y
       );
    }
}
