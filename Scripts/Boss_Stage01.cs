using UnityEngine;
using System.Collections;

public class Boss_Stage01 : Boss {

    public bool m_bLaserShot = false;
    private Laser_Linear[] m_Laser;
    private Laser_Linear m_BigLaser;

    private bool m_Union = false;
    private bool m_bDestroyed = false;

    private bool m_bNextScene;

	// Use this for initialization
	void Start () 
    {
        m_Hp = m_MaxHp;
        GetComponent<Animator>().SetFloat("Hp", m_Hp);
        m_Pattern = 1;
        m_Laser = new Laser_Linear[7];
        gameObject.SetActive(false);
        
	}

    void OnEnable()
    {
        m_Hp = m_MaxHp;
        GetComponent<Animator>().SetFloat("Hp", m_Hp);
        m_Pattern = 1;

        m_Laser[0] = transform.Find("Weapon/BLL/Pattern_D").gameObject.GetComponent<Laser_Linear>();
        m_Laser[1] = transform.Find("Weapon/BRR/Pattern_D").gameObject.GetComponent<Laser_Linear>();
        m_Laser[2] = transform.Find("Weapon/FLL/Pattern_D").gameObject.GetComponent<Laser_Linear>();
        m_Laser[3] = transform.Find("Weapon/FL/Pattern_D").gameObject.GetComponent<Laser_Linear>();
        m_Laser[4] = transform.Find("Weapon/FC/Pattern_D").gameObject.GetComponent<Laser_Linear>();
        m_Laser[5] = transform.Find("Weapon/FR/Pattern_D").gameObject.GetComponent<Laser_Linear>();
        m_Laser[6] = transform.Find("Weapon/FRR/Pattern_D").gameObject.GetComponent<Laser_Linear>();
        m_BigLaser = transform.Find("Wing/Center/Pattern_F").gameObject.GetComponent<Laser_Linear>();

        m_bDestroyed = false;
    }

	void Update () 
    {
        Animator animator = GetComponent<Animator>();
        m_Hp = GetComponent<Animator>().GetFloat("Hp");

        if(animator.GetBool("Init"))
        {
            //transform.Find("Collider").gameObject.SetActive(true);
            animator.SetBool("Init", false);
        }

        int NextPattern = 0;

        if(animator.GetBool("PatternEnd"))
        {
            do {
                if(!m_Union) {
                    NextPattern = Random.Range(1, 4);
                }
                else {
                    NextPattern = Random.Range(4, 8);
                }
            }
            while(m_Pattern == NextPattern);
            

            animator.SetInteger("Pattern", NextPattern);
            m_Pattern = NextPattern;

            if(m_Hp <= (m_MaxHp * 0.65f) && !m_Union) {
                GetComponent<Animator>().SetBool("Union", true);
                m_Union = true;
            }

            animator.SetBool("PatternEnd", false);
        }

        if(m_Hp <= m_MaxHp * 0.3f) {
            transform.Find("FX").gameObject.SetActive(true);
        }

        if(m_Hp <= 0.0f && !m_bDestroyed) {
            animator.SetBool("Destroyed", true);
            ScoreManager.Instance.AddScore(m_Score);
            m_bDestroyed = true;
        }

        if(m_bLaserShot) {
            LaserShot();
            m_bLaserShot = false;
        }

        if(m_bNextScene) {
            GameObject.Find("/SceneFadeInOut").GetComponent<SceneFadeInOut>().EndScene("Level02");
        }

	}

    void LaserShot()
    {
        int Back = Random.Range(0, 2);
        int Front1 = Random.Range(0, 5) + 2;
        int Front2;

        do {
            Front2 = Random.Range(0, 5) + 2;
        }
        while(Front1 == Front2);

        foreach(Laser_Linear laser in m_Laser) {
            laser.m_Shot = true;
        }

        m_Laser[Back].m_Shot = false;
        m_Laser[Front1].m_Shot = false;
        m_Laser[Front2].m_Shot = false;

    }
    void BigLaser()
    {
        m_BigLaser.m_Shot = true;
    }

    void NextScene()
    {
        m_bNextScene = true;
    }
}
