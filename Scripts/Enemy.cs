using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Google2u;

public class Enemy : MonoBehaviour {

    public string m_Id;

    private int m_Stage;
    private float m_Hp;
    private float m_Speed;
    private Vector3[] m_Path;
    private int m_Item;
    private int m_Score;

    private Enemies m_EnemyStats;

    private bool m_bExplosion = false;
    private bool m_bSuicide = false;

    public string m_DataTableName;

    public Sprite m_Sprite;
    public Material m_Material;
    public float m_SpawnTime;
    public float m_ColliderRadius = 1.0f;
    public GameObject m_Explosion;
    private ParticleSystem[] m_Weapon;
    private GameObject m_WeaponObject;

	void Start ()
    {
        gameObject.SetActive(false);

        GameObject _stats = GameObject.Find(m_DataTableName);
        if(_stats != null) {
            m_EnemyStats = _stats.GetComponent<Enemies>();
        }

        m_SpawnTime = m_EnemyStats.GetRow(m_Id)._Spawn_Time;
        m_Weapon = transform.FindChild("Weapon").gameObject.GetComponentsInChildren<ParticleSystem>();
        

	}

    void OnEnable()
    {
        GameObject _stats = GameObject.Find(m_DataTableName);

        if(_stats != null) {
            m_EnemyStats = _stats.GetComponent<Enemies>();
        }

        EnemiesRow row = m_EnemyStats.GetRow(m_Id);
        m_Stage = row._Stage;
        m_Hp = row._Hp;
        GetComponent<Animator>().SetFloat("Hp", row._Hp);
        m_Speed = row._Speed;
        m_Path = row._Path.ToArray();
        transform.position = row._Spawn_Point;

        for(int i = 0; i < m_Path.Length; i++ ) {
            m_Path[i] += row._Spawn_Point;
        }

        iTween.MoveTo(gameObject, iTween.Hash("path", m_Path, "speed", row._Speed, "orienttopath", true, "looktime", 0.0f,  "easetype", "linear", "oncomplete", "FinishTween"));
        
        GetComponentInChildren<SpriteRenderer>().sprite = m_Sprite;
        GetComponentInChildren<SpriteRenderer>().material = m_Material;
        
        m_Item = row._Item;
        m_Score = row._Score;
        m_Weapon = transform.FindChild("Weapon").gameObject.GetComponentsInChildren<ParticleSystem>();

        GetComponentInChildren<CircleCollider2D>().radius = m_ColliderRadius;
    }

    void OnDrawGizmos()
    {
        if(this.isActiveAndEnabled) {
            if(m_Path != null) {
                if(m_Path.Length > 0) {
                    iTween.DrawPath(m_Path);
                }
            }
        }
    }

	void Update ()
    {
        StatusUpdate();

        if(!m_bExplosion) {
            if(m_Hp <= 0.0f) {
                Explosion();
            }
        }
        if(m_bSuicide) {
            StartCoroutine("Suicide");
        }
	}

    void StatusUpdate()
    {
        m_Hp = GetComponent<Animator>().GetFloat("Hp");
    }

    void Explosion()
    {
        m_bExplosion = true;
        Instantiate(m_Explosion, transform.position, Quaternion.identity);
        
        if(m_Item == 1) {
            Vector3 pos = new Vector3(transform.position.x, transform.position.y, 0.0f);
            Instantiate(GameObject.Find("/Pool/Power"), pos, Quaternion.identity);
        }

        if(GetComponentInChildren<SpriteRenderer>().gameObject != null) {
            GetComponentInChildren<SpriteRenderer>().gameObject.SetActive(false);
        }

        if(GetComponentInChildren<CircleCollider2D>().gameObject != null) {
            GetComponentInChildren<CircleCollider2D>().gameObject.SetActive(false);
        }

        if(GetComponentInChildren<TrailRenderer>().gameObject != null) {
            GetComponentInChildren<TrailRenderer>().gameObject.SetActive(false);
        }

        ScoreManager.Instance.AddScore(m_Score);
        

        foreach(ParticleSystem obj in m_Weapon) {
            obj.emissionRate = 0.0f;
        }

        
    }

    void FinishTween()
    {
        if(!m_bExplosion)
        {
            if(GetComponentInChildren<SpriteRenderer>().gameObject != null) {
                GetComponentInChildren<SpriteRenderer>().gameObject.SetActive(false);
            }

            if(GetComponentInChildren<CircleCollider2D>().gameObject != null) {
                GetComponentInChildren<CircleCollider2D>().gameObject.SetActive(false);
            }

            if(GetComponentInChildren<TrailRenderer>().gameObject != null) {
                GetComponentInChildren<TrailRenderer>().gameObject.SetActive(false);
            }
        }

        foreach(ParticleSystem obj in m_Weapon) {
            obj.emissionRate = 0.0f;
        }
        m_bSuicide = true;
    }

    IEnumerator Suicide()
    {
        yield return new WaitForSeconds(5.0f);
        Destroy(gameObject);
    }
    
}
