using UnityEngine;
using System.Collections;

public class Laser_Linear : MonoBehaviour {

    private LineRenderer _line;
    private BoxCollider2D _col;

    public float m_MaxWidth = 10.0f;
    //public Vector3 m_Direction = new Vector3(0.0f, -1.0f, 0.0f);
    public float m_Delay = 1.0f;
    public float m_ZeroToFull = 0.15f;
    public float m_Duration = 0.75f;
    public float m_FullToZero = 0.15f;

    public bool m_Shot = false;

    private float m_Time = 0.0f;
    private float m_CurWidth = 0.0f;


	// Use this for initialization
	void Start () {
        
	}

    void OnEnable()
    {
        _line = GetComponent<LineRenderer>();
        if(_line == null) {

        }

        _col = GetComponent<BoxCollider2D>();
        if(_col == null) {

        }

        _line.SetPosition(0, transform.position);
        _line.SetPosition(1, new Vector3(transform.position.x, transform.position .y - 50.0f, transform.position.z));
        _line.SetWidth(0.0f, 0.0f);
        _line.useWorldSpace = true;

        _col.offset = new Vector2(0.0f, 0.0f);
        _col.size = new Vector2(0.0001f, 0.0001f);
    }
	
	// Update is called once per frame
	void Update ()
    {
        _line.SetPosition(0, transform.position);
        _line.SetPosition(1, new Vector3(transform.position.x, transform.position.y - 50.0f, transform.position.z));

        if(m_Shot)
        {
            m_Time = 0.0f;
            _col.offset = new Vector2(0.0f, (Mathf.Abs(transform.position.y - (-24.0f)) / 2) - transform.position.y);
            _col.size = new Vector2(0.0f, Mathf.Abs(transform.position.y - (-24.0f)));
            StartCoroutine("LaserPreview");
            m_Shot = false;
        }

        m_Time += Time.deltaTime;

	}

    IEnumerator LaserPreview()
    {
        while(m_Time < m_Delay) {
            //if(!GetComponentInChildren<ParticleSystem>().isPlaying)
                GetComponentInChildren<ParticleSystem>().Play();
            yield return null;
        }
        m_Time = 0.0f;
        yield return StartCoroutine("LaserStart");
    }

    IEnumerator LaserStart()
    {
        while(m_Time < m_ZeroToFull) {
            m_CurWidth = Mathf.Lerp(m_CurWidth, m_MaxWidth, (m_CurWidth / m_MaxWidth) + (Time.deltaTime / m_ZeroToFull));
            _line.SetWidth(m_CurWidth, m_CurWidth);
            _col.size = new Vector2(m_CurWidth, _col.size.y);
            yield return null;
        }
        yield return StartCoroutine("LaserShot");
    }

    IEnumerator LaserShot()
    {
        while(m_Time < m_ZeroToFull + m_Duration) {
            _col.size = new Vector2(m_CurWidth, _col.size.y);

            yield return null;
        }
        yield return StartCoroutine("LaserEnd");
    }

    IEnumerator LaserEnd()
    {
        while(m_Time < m_ZeroToFull + m_Duration + m_FullToZero) {
            m_CurWidth = Mathf.Lerp(0.0f, m_CurWidth, (m_CurWidth / m_MaxWidth) - (Time.deltaTime / m_FullToZero));
            _line.SetWidth(m_CurWidth, m_CurWidth);
            _col.size = new Vector2(m_CurWidth, _col.size.y);
            yield return null;
        }

        _col.offset = new Vector2(0.0f, 0.0f);
        _col.size = new Vector2(0.0001f, 0.0001f);
        yield return null;
    }
}
