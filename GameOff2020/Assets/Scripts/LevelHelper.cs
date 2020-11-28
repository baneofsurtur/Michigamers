using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * A helper class containing shared objects and methods used by 
 * mutiple level related scripts.
 */
public class LevelHelper : MonoBehaviour
{
    public float secondsToUnitsConversion = 10f;
    public SongLoader songLoader;
    public float time = 0f;
    public float duration = 0f;
    public float fadeOut = 0f;
    public List<Section> soundSections;
    public float startingPosition;
    public float halfWidth;
    public int levelLength = 0;

    /*
     * Create a new instance of LevelHelper and attach it to the 
     * provided game object.
     */
    public static LevelHelper createLevelHelper(GameObject gameObject)
    {
        LevelHelper levelHelper = gameObject.AddComponent<LevelHelper>();
        levelHelper.songLoader = SongLoader.createSongLoader(gameObject);

        levelHelper.soundSections = levelHelper.songLoader.songData.sections;
        levelHelper.duration = levelHelper.songLoader.songData.track.duration;
        levelHelper.fadeOut = levelHelper.songLoader.songData.track.start_of_fade_out;
        levelHelper.levelLength = Mathf.CeilToInt(
            levelHelper.secondsToUnitsConversion * levelHelper.duration
        );

        Camera camera = Camera.main;
        float halfHeight = camera.orthographicSize;
        levelHelper.halfWidth = camera.aspect * halfHeight;
        levelHelper.startingPosition = -levelHelper.halfWidth;

        return levelHelper;
    }

    /*
     * Takes in a game object and a size in units and resizes the game objects 
     * horizontal length to the provided unit size.
     */
    public void newXScale(GameObject theGameObject, float newSize)
    {

        float size = theGameObject.GetComponent<Renderer>().bounds.size.x;

        Vector3 rescale = theGameObject.transform.localScale;

        rescale.x = newSize * rescale.x / size;

        theGameObject.transform.localScale = rescale;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
