using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioSource clickAudio, attachAudio, detachAudio, mainGameBackgroundMusic, highScoreAudio;

    public GameObject muteGameobject;
    public GameObject unMuteGameobject;


    // Start is called before the first frame update
    void Start()
    {
        var audioSources = GetComponents<AudioSource>();
        clickAudio = audioSources[0];
        attachAudio = audioSources[1];
        detachAudio = audioSources[2];
        mainGameBackgroundMusic = audioSources[3];
        highScoreAudio = audioSources[4];
    }

    public void MuteBackground()
    {
        mainGameBackgroundMusic.volume = 0;

        muteGameobject.SetActive(true);
        unMuteGameobject.SetActive(false);
    }

    public void UnMuteBackground()
    {
        mainGameBackgroundMusic.volume = 0.5f;

        unMuteGameobject.SetActive(true);
        muteGameobject.SetActive(false);
    }
}
