using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public LevelHelper()
    {
        songLoader = new SongLoader();
        soundSections = songLoader.songData.sections;
        duration = songLoader.songData.track.duration;
        fadeOut = songLoader.songData.track.start_of_fade_out;
        levelLength = Mathf.CeilToInt(secondsToUnitsConversion * duration);

        Camera camera = Camera.main;
        float halfHeight = camera.orthographicSize;
        halfWidth = camera.aspect * halfHeight;
        startingPosition = -halfWidth;
    }

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
