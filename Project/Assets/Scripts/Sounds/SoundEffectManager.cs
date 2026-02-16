using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : SingletonMonoBehaviour<SoundEffectManager>
{
    [SerializeField] private AudioSource seSource;
    [SerializeField] private AudioClip[] clip;

    //効果音の種類
    public enum SoundType
    {
        HIT, //ヒット
        DECISION, //決定  
        SOUND_RANK_S, //ランクSの時にだす効果音
        SOUND_RANK_A, //ランクAの時にだす効果音
        SOUND_RANK_B, //ランクBの時にだす効果音
        SOUND_RANK_C, //ランクCの時にだす効果音
        SOUND_RANK_D, //ランクDの時にだす効果音
        MISS, //ミス
        SELECT, //選択
        NONE = 0
    }

    private Dictionary<SoundType, AudioClip> seDict = new Dictionary<SoundType, AudioClip>();

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);

            if(clip == null || clip.Length < 9)
            {
                Debug.LogError("SoundEffectManager: AudioClipが足りません。インスペクターで設定してください");
                return;
            }

            //enumとclipを紐づけ
            seDict[SoundType.HIT] = clip[0];
            seDict[SoundType.DECISION] = clip[1];
            seDict[SoundType.SOUND_RANK_S] = clip[2];
            seDict[SoundType.SOUND_RANK_A] = clip[3];
            seDict[SoundType.SOUND_RANK_B] = clip[4];
            seDict[SoundType.SOUND_RANK_C] = clip[5];
            seDict[SoundType.SOUND_RANK_D] = clip[6];
            seDict[SoundType.MISS] = clip[7];
            seDict[SoundType.SELECT] = clip[8];
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    //再生
    public void PlaySE(SoundType type)
    {
        if (seDict.TryGetValue(type, out var clip))
        {
            seSource.PlayOneShot(clip);
        }
    }
}
