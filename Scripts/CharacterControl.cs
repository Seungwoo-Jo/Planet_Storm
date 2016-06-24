using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class CharacterControl : MonoBehaviour {

    public int m_Player = 0;

    public Vector3 m_ResetPosition = Vector3.zero;
    private Vector3 m_PrevPos;

    public float m_FastSpeed = 1.0f;
    public float m_SlowSpeed = 0.5f;
    public float m_MaxRoll = 45.0f;

    private bool m_bSlow = false;

    public GameObject[] m_Bullet;
    public Transform m_Muzzle;
    private GameObject m_BulletInst;
    public float m_ShootDelay = 0.2f;
    private float m_ShootTimer = 0.0f;
    public float m_BulletSpeedMultiply = 1.0f;
    public GameObject m_Explosion;
    private bool m_bDestroy = false;

    private Vector2 _LStick;
    private Vector2 _DPad;
    private Vector2 _Delta;

    private bool m_Immotal = true;

    // Use this for initialization
	void Start () {

	}

    void Awake()
    {
        if(PlayerManager.Instance.m_Character == 0) {
            transform.Find("BulletCollider").gameObject.GetComponent<SphereCollider>().radius *= 0.8f;
        }
        
    }

	// Update is called once per frame
	void Update ()
    {
        if(!m_bDestroy) 
        {
            _LStick = GamePadInput.Instance.GetStick(m_Player, "left");
            _DPad = GamePadInput.Instance.GetCross(m_Player, false);

            _LStick.Set(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            _DPad.Set(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            if(Input.GetKeyDown("tab")) {
                if(m_Immotal) {
                    m_Immotal = false;
                }
                else {
                    m_Immotal = true;
                }
            }

            if(!m_bDestroy) {
                transform.Find("BulletCollider").gameObject.SetActive(m_Immotal);
            }
            

            Status();
            Move();
            Roll();
            Shoot();
        }
        
	}

    void Status()
    {
        if(!m_bDestroy && GetComponent<Animator>().GetBool("bDestroy")) {
            m_bDestroy = true;
            Instantiate(m_Explosion, transform.position, Quaternion.identity);
            int temp = PlayerManager.Instance.m_NumOfDead;
            PlayerManager.Instance.m_NumOfDead = temp + 1;
            PlayerManager.Instance.m_bDead = true;

            if(PlayerManager.Instance.m_Character != 1) {
                GetComponent<Animator>().SetInteger("iPower", 0);
            }

            GetComponent<SpriteRenderer>().sortingLayerName = "Default";
            transform.Find("Manipulators").gameObject.SetActive(false);
            transform.Find("BulletCollider").gameObject.SetActive(false);
            transform.Find("PowerCollider").gameObject.SetActive(false);
            Resetting();
        }
        
    }

    void Move()
    {
        _Delta = (_LStick - Vector2.zero);
        if(_DPad != Vector2.zero) {
            _Delta = (_DPad - Vector2.zero).normalized;
        }
        Vector3 pos = GetComponent<Transform>().position;

        float deltaX = m_FastSpeed * _Delta.x * Time.deltaTime;
        float deltaY = m_FastSpeed * _Delta.y * Time.deltaTime;

        m_bSlow = GamePadInput.Instance.ButtonPressed(m_Player, "R1");
        m_bSlow = Input.GetButton("Slow");

        float factor = 1.0f;
        if(PlayerManager.Instance.m_Character == 4) {
            factor *= 1.2f;
        }

        if(m_bSlow) {
            deltaX = deltaX * m_SlowSpeed * factor;
            deltaY = deltaY * m_SlowSpeed * factor;
        }

        RaycastHit2D UpBound    = Physics2D.Raycast(pos, Vector2.up,    50.0f, 1 << LayerMask.NameToLayer("Wall"));
        RaycastHit2D DownBound  = Physics2D.Raycast(pos, Vector2.down,  50.0f, 1 << LayerMask.NameToLayer("Wall"));
        RaycastHit2D LeftBound  = Physics2D.Raycast(pos, Vector2.left,  50.0f, 1 << LayerMask.NameToLayer("Wall"));
        RaycastHit2D RightBound = Physics2D.Raycast(pos, Vector2.right, 50.0f, 1 << LayerMask.NameToLayer("Wall"));

        if(UpBound.distance <= 1.0f) {
            if(_Delta.y > 0.0f) {
                deltaY = 0.0f;
            }
        }
        if(DownBound.distance <= 1.0f) {
            if(_Delta.y < 0.0f) {
                deltaY = 0.0f;
            }
        }
        if(LeftBound.distance <= 1.0f) {
            if(_Delta.x < 0.0f) {
                deltaX = 0.0f;
            }
        }
        if(RightBound.distance <= 1.0f) {
            if(_Delta.x > 0.0f) {
                deltaX = 0.0f;
            }
        }
        
        
        pos.x += deltaX;
        pos.y += deltaY;
        
        transform.Translate(deltaX, deltaY, 0.0f, Camera.main.transform);
    }

    void Roll()
    {
        GetComponent<Transform>().rotation = Quaternion.identity;

        if(_DPad == Vector2.zero) {
            GetComponent<Animator>().SetFloat("fRoll", _LStick.x);
        }
        else {
            GetComponent<Animator>().SetFloat("fRoll", _DPad.x);
        }

        GetComponent<Animator>().SetBool("bSlow", m_bSlow);
    }

    void Shoot()
    {
        bool shoot = GamePadInput.Instance.ButtonDown(m_Player, "X");
        shoot = GamePadInput.Instance.ButtonPressed(m_Player, "X");
        shoot = Input.GetButton("Fire");

        if(shoot && m_ShootTimer > m_ShootDelay)
        {
            Vector3 pos = m_Muzzle.position;
            int Power = GetComponent<Animator>().GetInteger("iPower");
            m_BulletInst = Instantiate(m_Bullet[Power], pos, Quaternion.identity) as GameObject;
            m_BulletInst.GetComponent<Animator>().SetFloat("Speed", m_BulletSpeedMultiply);
            GetComponent<AudioSource>().Play();
            m_ShootTimer = 0.0f;
        }
        else {
            m_ShootTimer += Time.deltaTime;
        }
    }

    void Resetting()
    {
        StartCoroutine("ResettingPosition");
    }

    IEnumerator ResettingPosition()
    {
        float LerpTime = 0.0f;
        float DeadTime = PlayerManager.Instance.DeadTime;
        m_PrevPos = m_ResetPosition;
        m_PrevPos.y -= 10.0f;
        transform.position = m_PrevPos;
        yield return new WaitForSeconds(DeadTime);

        GetComponent<Animator>().SetBool("bDestroy", false);
        GetComponent<SpriteRenderer>().sortingLayerName = "Player";
        GetComponent<Animator>().SetFloat("fRoll", 0.0f);

        while(LerpTime <= 1.0f)
        {
            transform.position = Vector3.Lerp(m_PrevPos, m_ResetPosition, LerpTime);
            LerpTime += Time.deltaTime;
            yield return null;
        }
        m_bDestroy = false;
        PlayerManager.Instance.m_bDead = false;
        transform.Find("Manipulators").gameObject.SetActive(true);
        transform.Find("PowerCollider").gameObject.SetActive(true);
        StartCoroutine("ActiveBulletCollider");
    }

    IEnumerator ActiveBulletCollider()
    {
        yield return new WaitForSeconds(4.0f);
        transform.Find("BulletCollider").gameObject.SetActive(true);
    }
}