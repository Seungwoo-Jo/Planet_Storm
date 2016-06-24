using UnityEngine;
using System.Collections;

public class PlayerBullet : MonoBehaviour {

    public float m_Damage = 1.0f;
    public GameObject m_BulletExplosionEffect;
    private float m_BulletSpeed;
    private bool m_bDestroy;
    private Vector3 m_Direction;

	// Use this for initialization
	void Start () {
        Animator animator = GetComponent<Animator>();
        if(animator == null) {
            animator = GetComponentInParent<Animator>();
        }
        animator.SetFloat("Damage", m_Damage);
        m_BulletSpeed = animator.GetFloat("Speed");
        m_bDestroy = false;

        m_Direction = (transform.Find("Forward").position - transform.position).normalized;
	}
	
	// Update is called once per frame
	void Update () {
        m_Direction = (transform.Find("Forward").position - transform.position).normalized;
        GetComponent<Rigidbody2D>().velocity = m_Direction * 1000 * m_BulletSpeed * Time.deltaTime;
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Vector2 pos = new Vector2(transform.position.x, transform.position.y);
            Vector2 dir = new Vector2(col.gameObject.transform.position.x, col.gameObject.transform.position.y);
            dir = (dir - pos).normalized;

            RaycastHit2D hit = Physics2D.Raycast(pos, dir, 5.0f);
            if(m_BulletExplosionEffect != null) {
                Instantiate(m_BulletExplosionEffect, hit.point, Quaternion.identity);
            }
            
            Animator animator = col.gameObject.GetComponentInParent<Animator>();
            animator.SetFloat("Hp", animator.GetFloat("Hp") - m_Damage);
            m_bDestroy = true;
        }
        if(m_bDestroy) {
            Destroy(this.gameObject);
        }

        m_bDestroy = false;
    }
}
