/* Copyright Michigamers 11/29/2020.  All rights reserved */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Bar
{
    public float start;
    public float duration;
    public float confidence;
}

[System.Serializable]
public class Beat
{
    public float start;
    public float duration;
    public float confidence;
}

[System.Serializable]
public class Track
{
    public int num_samples;
    public float duration;
    public float tempo;
    public int time_signature;
    public float end_of_fade_in;
    public float start_of_fade_out;
}

[System.Serializable]
public class Section
{
    public float start;
    public float duration;
    public float tempo;
}


[System.Serializable]
public class SongData
{
    public Track track;
    public List<Bar> bars;
    public List<Beat> beats;
    public List<Section> sections;

}

/*
 * A class responsible for loading song data from a spotify analysis file
 * into the appropriate classes
 */
public class SongLoader : MonoBehaviour
{
    public TextAsset songDataTextFile;
    public SongData songData;

    private string songFileAsString;

    /*
     * Create a new instance of SongLoader and attach it to the 
     * provided game object.
     */
    public static SongLoader createSongLoader(GameObject gameObject)
    {
        SongLoader songLoaderObject = gameObject.AddComponent<SongLoader>();
        songLoaderObject.songDataTextFile = Resources.Load<TextAsset>("GhostFair");
        songLoaderObject.songFileAsString = songLoaderObject.songDataTextFile.text;

        songLoaderObject.songData =
            JsonUtility.FromJson<SongData>(songLoaderObject.songFileAsString);
        return songLoaderObject;
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
