using UnityEngine;
using System.Collections;

public class PlayerManager {
    private static PlayerManager instance;

    public int m_Character = 0;
    public int m_ModuleTop = 0;
    public int m_ModuleMid = 0;
    public int m_ModuleBot = 0;

    public int m_NumOfDead = 0;
    public bool m_bDead = false;
    private float m_DeadTime = 0.0f;


    public void Init()
    {

    }

    public static PlayerManager Instance
    {
        get
        {
            if(instance == null) {
                instance = new PlayerManager();
                instance.Init();
            }
            return instance;
        }
    }

    public float DeadTime
    {
        get
        {
            float factor = 3.0f;
            if(m_Character == 3) {
                factor *= 0.8f;
            }
            m_DeadTime = (float)m_NumOfDead * 1.0f;
            if(m_DeadTime > factor) {
                m_DeadTime = factor;
            }
            return m_DeadTime;
        }
    }
}
