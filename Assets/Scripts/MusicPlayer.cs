using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    private static MusicPlayer instance = null;

    public static float musicVolume = 1;

    public AudioClip[] playlist = new AudioClip[0];

    public static MusicPlayer Instance
    {
        get { return instance; }
    }

	public float GetMusicVolume() {
        musicVolume = GetComponent<AudioSource>().volume;
        return musicVolume;
    }

	public void SetVolume(float vol) {
        musicVolume = vol;
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

    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            PlayMusic();
        }
    }

    public void PlayMusic()
    {
        GetComponent<AudioSource>().PlayOneShot(playlist[0], musicVolume);
    }

}
