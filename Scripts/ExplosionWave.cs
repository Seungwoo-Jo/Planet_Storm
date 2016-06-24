using UnityEngine;
using System.Collections;

public class ExplosionWave : MonoBehaviour {

    public float _SpreadSpeedMultiply = 1.0f;
    private Vector3 _SpreadDirection = Vector3.zero;
    
	// Use this for initialization
	void Start () {
	    
	}
	
    void OnEnable() {
        StartCoroutine("Destroy");
        
        _SpreadDirection.x = transform.localScale.x * 0.1f;
        _SpreadDirection.y = transform.localScale.y * 0.1f;
    }

	// Update is called once per frame
	void Update () {
        transform.localScale += _SpreadDirection * 27.5f * _SpreadSpeedMultiply;
	}

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(2.0f);
        
        Destroy(gameObject);
    }
}
