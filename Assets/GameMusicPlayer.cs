using UnityEngine;
 
public class GameMusicPlayer : MonoBehaviour
{
    private static GameMusicPlayer instance = null;
    public static GameMusicPlayer Instance {
        get { return instance; }
    }
    
    private AudioSource _audioSource;
    private void Awake()
    {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        } else {
            instance = this;
        }
        DontDestroyOnLoad(transform.gameObject);
        _audioSource = GetComponent<AudioSource>();
        
    }
 
    public void PlayMusic()
    {
        if (_audioSource.isPlaying) return;
        _audioSource.Play();
    }
 
    public void StopMusic()
    {
        _audioSource.Stop();
    }
}