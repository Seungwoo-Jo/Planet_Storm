using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class BossPattern : MonoBehaviour {

    public UnityEvent AttackPattern;

	// Use this for initialization
	void Start () {

        InvokeRepeating("StartPattern", 0.0f, 0.0f);
	}
	
	// Update is called once per frame
	void Update () {
	
        
	}

    void StartPattern()
    {
        StartCoroutine(Pattern());
    }

    IEnumerator Pattern()
    {
        AttackPattern.Invoke();
        StopCoroutine(Pattern());
        yield return null;
    }
}
