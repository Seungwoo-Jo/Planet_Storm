using UnityEngine;
using System.Collections;

public class DestroyZone : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.layer != LayerMask.NameToLayer("Create")) {
            Destroy(col.gameObject);
        }
    }
}
