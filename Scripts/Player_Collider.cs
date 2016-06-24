using UnityEngine;
using System.Collections;

public class Player_Collider : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.layer == LayerMask.NameToLayer("Power"))
        {
            int Power = GetComponentInParent<Animator>().GetInteger("iPower") + 1;
            if(Power >= 3) {
                Power = 3;
            }
            GetComponentInParent<Animator>().SetInteger("iPower", Power);
            Destroy(col.gameObject);
        }
        else if(col.gameObject.layer == LayerMask.NameToLayer("EnemyWeapon"))
        {
            GetComponentInParent<Animator>().SetBool("bDestroy", true);
        }
    }

    void OnParticleCollision(GameObject obj)
    {
        GetComponentInParent<Animator>().SetBool("bDestroy", true);
    }
}
