using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

    private AudioSource source;

    void Start()
    {
        source = transform.Find("Door/Audio Source").gameObject.GetComponent<AudioSource>();
    }

    void PlayCloseSound(AudioClip clip)
    {
        if(GetComponent<Animator>().GetBool("NextScene"))
            source.PlayOneShot(clip);
    }
}
