using UnityEngine;
using System.Collections;

public class Boss_Stage02 : Boss
{
    public GameObject m_DestroyEffect;

    private bool m_Union = false;
    private bool m_bDestroyed = false;

    void Start()
    {
        m_Hp = m_MaxHp;
        GetComponent<Animator>().SetFloat("Hp", m_Hp);
        m_Pattern = 1;
        //m_Laser = new Laser_Linear[7];
        gameObject.SetActive(false);

    }

    void OnEnable()
    {
        m_Hp = m_MaxHp;
        GetComponent<Animator>().SetFloat("Hp", m_Hp);
        m_Pattern = 1;

        m_bDestroyed = false;
    }

    void Update()
    {
        Animator animator = GetComponent<Animator>();
        m_Hp = GetComponent<Animator>().GetFloat("Hp");

        if(animator.GetBool("Init")) {
            animator.SetBool("Init", false);
        }

        int NextPattern = 0;

        if(animator.GetBool("PatternEnd")) {
            do {
                if(!m_Union) {
                    NextPattern = Random.Range(1, 5);
                }
            }
            while(m_Pattern == NextPattern);

            if(m_Union) {
                NextPattern = Random.Range(5, 7);
            }

            animator.SetInteger("Pattern", NextPattern);
            m_Pattern = NextPattern;

            if(m_Hp <= (m_MaxHp * 0.65f) && !m_Union) {
                GetComponent<Animator>().SetBool("Union", true);
                m_Union = true;
            }

            animator.SetBool("PatternEnd", false);
        }
        /*
        if(m_Hp <= m_MaxHp * 0.3f) {
            transform.Find("FX").gameObject.SetActive(true);
        }
        */
        if(m_Hp <= 0.0f && !m_bDestroyed) {
            animator.SetBool("Destroyed", true);
            ScoreManager.Instance.AddScore(m_Score);
            m_bDestroyed = true;
        }
    }

    public void ToPhase2()
    {
        Instantiate(m_DestroyEffect, transform.Find("Body/Rail_Left").position, Quaternion.identity);
        Instantiate(m_DestroyEffect, transform.Find("Body/Rail_Right").position, Quaternion.identity);
    }
}
