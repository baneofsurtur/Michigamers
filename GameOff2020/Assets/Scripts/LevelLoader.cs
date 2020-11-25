using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    
    public GameObject groundObject;
    public GameObject playerObject;
    public GameObject obsticleObject;
    public Transform levelTransformer;
    private LevelHelper levelHelper;
    public float time = 0f;

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
        levelHelper = new LevelHelper();
        levelTransformer = GetComponent<Transform>();

        time = levelHelper.time;
        Bar[] soundBars = levelHelper.songLoader.songData.bars.ToArray();
        List<Section> soundSections = levelHelper.songLoader.songData.sections;

        int totalBarCount = soundBars.Length;
        int barCounter = 0;
        float position = levelHelper.startingPosition;

        while (position < levelHelper.levelLength - levelHelper.secondsToUnitsConversion)
        {
            GameObject initialGroundChunk = Instantiate(
                groundObject,
                new Vector3(position, -8, 0),
                Quaternion.identity
            );
            initialGroundChunk.transform.parent =
                              GameObject.Find("Level").transform;

            newXScale(initialGroundChunk, levelHelper.halfWidth * 2);

            position += levelHelper.halfWidth * 2;
        }

        Instantiate(
            playerObject,
            new Vector3(-levelHelper.halfWidth / 2, -1.5f, 0),
            Quaternion.identity
        );


        foreach (Section section in soundSections)
        {

            float sectionStart = section.start;
            float sectionEnd = sectionStart + section.duration;

            while (barCounter < totalBarCount)
            {
                Bar bar = (Bar)soundBars.GetValue(barCounter);
                
                if (bar.start <= sectionEnd)
                {
                    GameObject newObsticle = Instantiate(
                        obsticleObject,
                        new Vector3(bar.start * levelHelper.secondsToUnitsConversion, -1, 0),
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

    // Update is called once per frame
    void Update()
    {
        time = (float) Time.deltaTime / 1f;
        levelTransformer.position = new Vector2(
            this.levelTransformer.position.x - (time * levelHelper.secondsToUnitsConversion),
            this.levelTransformer.position.y
        );
    }
}
