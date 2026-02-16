using UnityEngine;

//ノーツ
public class Notes : MonoBehaviour
{
    private float m_speed = 10.0f; //ノーツのスピード
    bool isStart;

    private void Start()
    {
        m_speed = GManager.instance.noteSpeed * 2; //ノーツスピード
    }

    // Update is called once per frame
    void Update()
    {

        //ポーズ中は動かさない
        if (GManager.instance.isPause) return;

        if (!GManager.instance.start) return;

         //前進
         this.transform.position -= this.transform.forward * Time.deltaTime * m_speed;
    }
}
