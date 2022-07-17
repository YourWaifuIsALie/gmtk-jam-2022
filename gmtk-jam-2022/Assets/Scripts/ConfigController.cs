using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Actually a sound controller
public class ConfigController : MonoBehaviour
{
    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float effectVolume = 1.0f;

    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float musicVolume = 1.0f;

    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float mainVolume = 0.5f;

    [SerializeField]
    private AudioClip[] audioList;

    Dictionary<string, AudioSource> audioSources;

    public static int SOUNDCOLLISION = 0;
    public static int SOUNDENGINE = 1;
    public static int SOUNDWALK = 2;

    [SerializeField]
    private AudioClip[] musicList;

    public static int MUSICSAD = 0;
    public static int MUSICHAPPY = 1;
    private string currentMusicIndex;
    private AudioSource musicSource;

    void Start()
    {

        audioSources = new Dictionary<string, AudioSource>();
        for (var i = 0; i < audioList.Length; i++)
        {
            audioSources[$"{i}"] = gameObject.AddComponent<AudioSource>();
            audioSources[$"{i}"].clip = audioList[i];
        }

        currentMusicIndex = "0";
        musicSource = gameObject.AddComponent<AudioSource>();

        EventController.current.onSoundEvent += SoundEventHandler;
        EventController.current.onMusicEvent += MusicEventHandler;
    }

    private void onDestroy()
    {
        EventController.current.onSoundEvent -= SoundEventHandler;
        EventController.current.onMusicEvent -= MusicEventHandler;
    }

    void Update()
    {
        AudioListener.volume = mainVolume;
    }

    private void SoundEventHandler(string id, string action, float level, bool wait)
    {
        if (!audioSources.ContainsKey(id))
        {
            Debug.Log($"Invalid sound effect id {id}");
            return;
        }

        var audioSource = audioSources[id];

        if (action == EventController.PLAY)
        {
            if (!audioSource.isPlaying)
                audioSource.PlayOneShot(audioSource.clip, level * effectVolume);
        }
        else if (action == EventController.STOP)
            audioSource.Stop();
    }

    private void MusicEventHandler(string id, string action)
    {
        if (action == EventController.PLAY)
        {
            if (!musicSource.isPlaying)
            {
                currentMusicIndex = id;
                musicSource.PlayOneShot(musicList[int.Parse(id)], musicVolume);
            }
            else if (currentMusicIndex != id)
            {
                musicSource.Stop();
                currentMusicIndex = id;
                musicSource.PlayOneShot(musicList[int.Parse(id)], musicVolume);
            }
            else
            {
                // Debug.Log("repeat");
            }
        }
        else if (action == EventController.STOP)
            musicSource.Stop();
    }
}
