using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Loads the main level into the game and sets is scrolling soeed.
 * Loads the player prefab into the game and sets its starting position
 * Loads the obsticle prefabs into the game and sets their starting position
 */
public class LevelLoader : MonoBehaviour
{
    
    public GameObject groundObject;
    public GameObject playerObject;
    public GameObject obsticleObject;
    public GameObject moonBaseObject;
    public Transform levelTransformer;
    public float time = 0f;
    public static LevelLoader instance;

    private LevelHelper levelHelper;
    private float moonBaseStart;
    public bool endReached = false;
    public GameObject player;
    private GameObject moonBase;
    private float levelEndPoint;

    /*
     * Create level chunks to account for the full legth of the level 
     * and load them in
     */
    public void loadLevelChunks()
    {
        float position = levelHelper.startingPosition;

        while (position <
            levelHelper.levelLength - levelHelper.secondsToUnitsConversion)
        {
            GameObject initialGroundChunk = Instantiate(
                groundObject,
                new Vector3(position, -8, 0),
                Quaternion.identity
            );
            initialGroundChunk.transform.parent =
                              GameObject.Find("Level").transform;

            levelHelper.newXScale(initialGroundChunk, levelHelper.halfWidth * 2);

            position += levelHelper.halfWidth * 2;
        }
    }

    /*Load obsticle prefab objects into the level. The start position of 
     * the obsticles is based off of the start time of the bar relative 
     * to the overall song. Bars are loaded via song sections so that if 
     * different sections have different tempos, then the bars will adjust 
     * accordingly
     */
    public void loadObsticles()
    {
        Bar[] soundBars = levelHelper.songLoader.songData.bars.ToArray();
        List<Section> soundSections = levelHelper.songLoader.songData.sections;

        int totalBarCount = soundBars.Length;
        int barCounter = 0;

        foreach (Section section in soundSections)
        {

            float sectionStart = section.start;
            float sectionEnd = sectionStart + section.duration;

            while (barCounter < totalBarCount)
            {
                Bar bar = (Bar)soundBars.GetValue(barCounter);

                if (bar.start > levelHelper.songLoader.songData.track.start_of_fade_out)
                {
                    break;
                }

                Debug.Log("Bar  start: " + bar.start);
                if (bar.start <= sectionEnd)
                {
                    GameObject newObsticle = Instantiate(
                        obsticleObject,
                        new Vector3(
                            bar.start * levelHelper.secondsToUnitsConversion,
                            -1,
                            0
                        ),
                        Quaternion.identity
                    );

                    newObsticle.transform.parent =
                          GameObject.Find("Level").transform;
                    barCounter++;
                }
                else
                {
                    break;
                }
            }
        }
    }

    void Awake() // note it's now Awake instead of Start. That's important.
    {
        instance = this; // hold a reference to the (last) instance of this class
    }

    // Start is called before the first frame update
    void Start()
    {
        levelHelper = LevelHelper.createLevelHelper(gameObject);
        levelTransformer = GetComponent<Transform>();

        time = levelHelper.time;

        loadLevelChunks();

        //Create player prefab object and load it into the proper position
        player = Instantiate(
            playerObject,
            new Vector3(-levelHelper.halfWidth / 2, -1.5f, 0),
            Quaternion.identity
        );

       
        loadObsticles();

        
        moonBase = Instantiate(
                moonBaseObject,
                new Vector3(0, 8.2f, 0),
                Quaternion.identity
        );
        moonBase.transform.parent = GameObject.Find("Level").transform;

        float moonBaseWidth = moonBase.GetComponent<Collider2D>().bounds.size.x;
        float moonBaseRadius = moonBaseWidth / 2f;
        moonBaseStart = levelHelper.secondsToUnitsConversion * levelHelper.songLoader.songData.track.start_of_fade_out + moonBaseRadius;
        moonBase.transform.position = new Vector2(moonBaseStart, moonBase.transform.position.y);

        levelEndPoint = GameObject.Find("ExplodingShip").transform.position.x + 2;
    }

    /* Update is called once per frame.
     * Shift x position of level chunks in the negative direction such that 
     * the level moves to the left  at a rate of 
     * levelHelper.secondsToUnitsConversion per second.
     */
    void Update()
    {
        levelEndPoint -= time * levelHelper.secondsToUnitsConversion;
        
        if (player.transform.position.x >= levelEndPoint)
        {
            endReached = true;
        }

        if (endReached)
        {
            time = 0f;
        }
        else
        {
            time = (float)Time.deltaTime / 1f;
        }
        
        levelTransformer.position = new Vector2(
            levelTransformer.position.x - (time * levelHelper.secondsToUnitsConversion),
            levelTransformer.position.y
        );
    }
}
