using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    private static MusicPlayer instance = null;
    
    public static float musicVolume = 0.5f;

    AudioSource aSource;

    public AudioClip[] playlist = new AudioClip[0];

    public static MusicPlayer Instance
    {
        get { return instance; }
    }

    void Start()
    {
        aSource = GetComponent<AudioSource>();
    }

    public float GetMusicVolume()
    {
        return musicVolume;
    }

    public void SetVolume(float vol)
    {
        musicVolume = vol;
        MusicPlayer.Instance.GetComponent<AudioSource>().volume = vol;
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void FixedUpdate()
    {
        if (!aSource.isPlaying && SceneManager.GetActiveScene().buildIndex != 0)
        {
            PlayMusic();
            aSource.volume = musicVolume;
        }

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            StopMusic();
        }
    }

    void PlayMusic()
    {
        aSource.PlayOneShot(playlist[Random.Range(0, playlist.Length - 1)], musicVolume);
    }

    void StopMusic()
    {
        aSource.Stop();
    }

}
