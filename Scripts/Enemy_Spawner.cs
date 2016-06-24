using UnityEngine;
using System.Collections;
using Google2u;

public class Enemy_Spawner : MonoBehaviour {

    public int m_Stage = 1;

    private float m_StartTime;
    private float m_ElapseTime;

    public Enemy[] m_Enemies;
    private Enemy[] m_EnemiesObject;
    public Boss m_Boss;
    private Boss m_BossObject;
	// Use this for initialization
	void Start ()
    {
        m_StartTime = Time.time;
        m_ElapseTime = Time.time - m_StartTime;

        int i = 0;
        m_EnemiesObject = new Enemy[m_Enemies.Length];

        foreach(Enemy _enemy in m_Enemies)
        {
            m_EnemiesObject[i] = Instantiate(_enemy, Vector3.zero, Quaternion.identity) as Enemy;

            i++;
        }

        m_BossObject = Instantiate(m_Boss, new Vector3(0.0f, 50.0f, 0.11f), Quaternion.identity) as Boss;
	}
	
    

	// Update is called once per frame
	void Update ()
    {
        CalculateTime();
        Spawn();
	}



    void CalculateTime() {
        m_ElapseTime = Time.time - m_StartTime;
    }

    void Spawn()
    {
        foreach(Enemy _enemy in m_EnemiesObject)
        {
            if(_enemy != null)
            {
                if(!_enemy.isActiveAndEnabled) {
                    if(m_ElapseTime >= _enemy.m_SpawnTime) {
                        CreateEnemy(_enemy);
                    }
                }
            }
        }

        if(!m_BossObject.isActiveAndEnabled)
        {
            if(m_ElapseTime >= m_Boss.m_SpawnTime) {
                CreateBoss(m_BossObject);
            }
        }
    }

    void CreateEnemy(Enemy _enemy)
    {
        _enemy.gameObject.SetActive(true);
    }

    void CreateBoss(Boss _boss)
    {
        _boss.gameObject.SetActive(true);
    }
}
