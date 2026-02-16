using UnityEngine;

public class MusicManager : MonoBehaviour
{

    AudioSource audio;
    AudioClip clip;
    string songName; //ファイル名
    bool isPlayed;


    void Start()
    {
        GManager.instance.start = false;
        songName = "FREEDOM-DiVE↓"; //ファイル名
        audio = GetComponent<AudioSource>();
        clip = (AudioClip)Resources.Load("Musics/" + songName);
        isPlayed = false;
        GManager.instance.isPause = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (!GManager.instance.start) return;

        if(!isPlayed)
        {
            GManager.instance.start = true;
            GManager.instance.startTime = Time.time;
            isPlayed = true;
            audio.PlayOneShot(clip);
        }

        //ポーズ中の処理
        if (GManager.instance.isPause)
        {
            if (audio.isPlaying)
            {
                audio.Pause();
            }
        }
        else
        {
            if(isPlayed && !audio.isPlaying)
            {
                audio.UnPause();
            }
        }
    }
}
