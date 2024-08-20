using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{   
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    public AudioClip background;
    public AudioClip blockPlaced;
    public AudioClip playerAttack;

    // Start is called before the first frame update
    void Start()
    {
        musicSource.clip = background;
        musicSource.loop = true;
        musicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySFX(AudioClip clip) {
        SFXSource.PlayOneShot(clip, 10f);
        Debug.Log(clip + " Played");
    }
}
