using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private static MusicPlayer instance = null;

    public static float musicVolume = 1;

    AudioClip[] playlist = new AudioClip[0];

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
		playlist = Camera.main.GetComponent<AudioManager>().GetMusicList();
        PlayMusic();
    }

    void PlayMusic()
    {
        GetComponent<AudioSource>().PlayOneShot(playlist[0], musicVolume);
    }

}
