using UnityEngine;
using System;

public class Button : MonoBehaviour
{

    [SerializeField] private Animator[] m_anim; //ボタンのアニメーションを格納する変数 


    // Update is called once per frame
    void Update()
    {
        //Dキー
        if (Input.GetKeyDown(KeyCode.D))
        {
          
            m_anim[0].SetTrigger("isButtonDTrigger");
        }
            //Fキー
        if (Input.GetKeyDown(KeyCode.F))
        {
          
            m_anim[1].SetTrigger("isButtonFTrigger");
        }

            //Jキー
        if (Input.GetKeyDown(KeyCode.J))
        {
         
            m_anim[2].SetTrigger("isButtonJTrigger");
        }

            //Kキー
        if (Input.GetKeyDown(KeyCode.K))
        {
         
            m_anim[3].SetTrigger("isButtonKTrigger");
        }
        
    
    }
}
