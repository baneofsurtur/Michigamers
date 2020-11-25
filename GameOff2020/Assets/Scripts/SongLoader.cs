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

public class SongLoader : MonoBehaviour
{
    public TextAsset songDataTextFile;
    public SongData songData;

    private string songFileAsString;
    


    public SongLoader()
    {
        songDataTextFile = Resources.Load<TextAsset>("GhostFair");
        songFileAsString = this.songDataTextFile.text;
        
        songData =
            JsonUtility.FromJson<SongData>(this.songFileAsString);
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
