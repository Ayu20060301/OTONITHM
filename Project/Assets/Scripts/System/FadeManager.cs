using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;

public class FadeManager : SingletonMonoBehaviour<FadeManager>
{

    private float m_fadeSpeed = 1.0f; //フェードスピード
    float red, green, blue, alfa;

    bool Out = false;
    bool In = false;

    [SerializeField]
    private Image m_fadeImage;

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    void Start()
    {
        m_fadeImage = GetComponent<Image>();
        red = m_fadeImage.color.r;
        green = m_fadeImage.color.g;
        blue = m_fadeImage.color.b;
        alfa = m_fadeImage.color.a;
    }

    void Update()
    {
        if (In)
        {
            FadeIn();
        }

        if (Out)
        {
            FadeOut();
        }
    }

    void FadeIn()
    {
        alfa -= m_fadeSpeed * Time.deltaTime;
        Alpha();
        if (alfa <= 0)
        {
            In = false;
            m_fadeImage.enabled = false;
        }
    }

    void FadeOut()
    {
        m_fadeImage.enabled = true;
        alfa += m_fadeSpeed * Time.deltaTime;
        Alpha();
        if (alfa >= 1)
        {
            Out = false;
        }
    }

    void Alpha()
    {
        m_fadeImage.color = new Color(red, green, blue, alfa);
    }


}
