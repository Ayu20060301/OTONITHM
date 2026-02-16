using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering.PostProcessing;

public class SettingManager : MonoBehaviour
{
    //Sliderの参照
    [SerializeField] private Slider m_notesSpeedSlider;
    [SerializeField] private Slider m_offsetSlider;

 
    
    //テキストの参照
    [SerializeField] private TextMeshProUGUI m_noteSpeedText;
    [SerializeField] private TextMeshProUGUI m_offsetText;
    [SerializeField] private Text m_lightText;

    private bool m_lightEnabled;
   
    private void Start()
    {

        m_notesSpeedSlider.onValueChanged.AddListener(OnNoteSpeedChanged);
        m_offsetSlider.onValueChanged.AddListener(OnOffsetChanged);
       

        //初期値の読み込み
        float speed = PlayerPrefs.GetFloat("NoteSpeed", 40.0f);
        float offset = PlayerPrefs.GetFloat("Offset", 0.0f);
       

        m_notesSpeedSlider.value = speed;
        m_offsetSlider.value = offset;

        //テキストの更新
        UpdateNoteSpeedText(speed);
        UpdateOffsetText(offset);
      

    }

   

    //スライダーが変化したときの処理
    private void OnNoteSpeedChanged(float value)
    { 
        m_notesSpeedSlider.value = value;
        UpdateNoteSpeedText(value);
    }

   
    private void OnOffsetChanged(float value)
    {
        m_offsetSlider.value = value;
        UpdateOffsetText(value);
    }

    //ノーツスピードのテキストの更新
    private void UpdateNoteSpeedText(float value)
    {
        m_noteSpeedText.text = $"{value:F1}";
    }

    //オフセットのテキストの更新
    private void UpdateOffsetText(float value)
    {
        m_offsetText.text = $"{value:F1}";
    }

  
    //セーブ
    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("NoteSpeed", m_notesSpeedSlider.value);
        PlayerPrefs.SetFloat("Offset", m_offsetSlider.value);
        PlayerPrefs.SetInt("PostProcess", m_lightEnabled ? 1 : 0);
        PlayerPrefs.Save();

        //GManagerに反映
        GManager.instance.noteSpeed = m_notesSpeedSlider.value;
        GManager.instance.timingOffset = m_offsetSlider.value;
        GManager.instance.postProcessingEnabled = m_lightEnabled;
    }
}
