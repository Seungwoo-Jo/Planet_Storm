using UnityEngine;
using Google2u;
using System.Collections;

public class EnemyPath : MonoBehaviour {

    public Transform[] m_Path;

    void Start()
    {
    }

    void OnDrawGizmos()
    {
        if(m_Path.Length > 0)
            iTween.DrawPath(m_Path);
    }
}
