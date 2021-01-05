using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSounds : MonoBehaviour
{
    [SerializeField]
    private AudioClip buttonDown, buttonUp;

    private AudioSource _audioSource;
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void ButtonDownSound()
    {
        _audioSource.PlayOneShot(buttonDown);
    }
    
    public void ButtonUpSound()
    {
        _audioSource.PlayOneShot(buttonUp);
    }
}
