using UnityEngine;
using System.Collections;

public class Shot_Linear : MonoBehaviour {

    private ParticleSystem[] m_Particle;
    public float m_StartDelay = 2.0f;
    public float m_ShotPerSecond = 1.0f;
    public float m_AutoReleaseTime = 5.0f;
    public float m_BulletSize = 1.0f;
    public float m_Speed = 10.0f;
    public int m_MaxBullet = 5;
    public Vector3 m_Direction;
    public bool m_AimingShot = false;
    public string m_AimingTargetTag = "Player";


	// Use this for initialization
	void Start () {
        m_Particle = GetComponentsInChildren<ParticleSystem>();

        foreach(ParticleSystem obj in m_Particle)
        {
            if(obj != null) {
                obj.startDelay = m_StartDelay;
                obj.emissionRate = m_ShotPerSecond;
                obj.startLifetime = m_AutoReleaseTime;
                obj.startSize = m_BulletSize;
                obj.startSpeed = m_Speed * -1;
                obj.maxParticles = m_MaxBullet;

                
            }
        }
        
        
	}
	
    void OnEnable()
    {
        m_Particle = GetComponentsInChildren<ParticleSystem>();
        foreach(ParticleSystem obj in m_Particle)
        {
            if(obj != null) {
                obj.startDelay = m_StartDelay;
                obj.emissionRate = m_ShotPerSecond;
                obj.startLifetime = m_AutoReleaseTime;
                obj.startSize = m_BulletSize;
                obj.startSpeed = m_Speed * -1;
                obj.maxParticles = m_MaxBullet;
            }
            else {
                print("error: Shot_Linear");
            }
        }
        
    }

    void OnDrawGizmos()
    {
    }

	// Update is called once per frame
	void Update ()
    {
        if(m_AimingShot) {
            GameObject player = GameObject.FindWithTag(m_AimingTargetTag);
            if(player != null) {
                TransformExtensions.LookAt2D(transform, player.transform, Vector2.up);
            }
            else {
                TransformExtensions.LookAt2D(transform, (transform.position + Vector3.down), Vector2.up);
            }
            

        }
        else {
            TransformExtensions.LookAt2D(transform, (transform.position + m_Direction), Vector2.up);
        }
        
	}
}
