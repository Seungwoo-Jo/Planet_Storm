using UnityEngine;
using System.Collections;

public class MovieBackground : MonoBehaviour {

    public bool m_Repeat = false;
    private MovieTexture _Movie;

	// Use this for initialization
	void Start () {
        Renderer r = GetComponent<Renderer>();
        _Movie = (MovieTexture)r.material.mainTexture;
        _Movie.Play();

        GetComponent<Renderer>().sortingOrder = SortingLayer.NameToID("Background");

        print(_Movie.isPlaying);
	}

    void OnEnable()
    {
        //((MovieTexture)GetComponent<Renderer>().material.mainTexture).Play();
    }

	// Update is called once per frame
	void Update ()
    {
        if(!_Movie.isPlaying && m_Repeat) {
            _Movie.Stop();
            _Movie.Play();
        }
	}
}
