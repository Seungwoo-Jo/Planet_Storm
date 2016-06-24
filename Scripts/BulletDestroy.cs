using UnityEngine;
using System.Collections;

public class BulletDestroy : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        PlayerBullet[] Child = GetComponentsInChildren<PlayerBullet>();
        if(Child.Length == 0) {
            Destroy(this.gameObject);
        }
	}
}
