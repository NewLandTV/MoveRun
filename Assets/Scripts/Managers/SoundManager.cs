using UnityEngine;

[System.Serializable]
public struct Sound
{
    public bool isLoop;
    public bool bgm;
    public string name;
    public AudioClip clip;
}

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private Sound[] sounds;

    [SerializeField]
    private AudioSource bgmPlayer;
    [SerializeField]
    private AudioSource[] sfxPlayers;

    private void Awake()
    {
        PlaySound("Beat");
        PlaySound("Bgm");
    }

    public void PlaySound(string soundName)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name.Equals(soundName))
            {
                if (sounds[i].bgm)
                {
                    bgmPlayer.clip = sounds[i].clip;
                    bgmPlayer.loop = sounds[i].isLoop;

                    bgmPlayer.Play();

                    return;
                }
                else
                {
                    for (int j = 0; j < sfxPlayers.Length; j++)
                    {
                        if (!sfxPlayers[j].isPlaying)
                        {
                            sfxPlayers[j].clip = sounds[i].clip;
                            sfxPlayers[j].loop = sounds[i].isLoop;

                            sfxPlayers[j].Play();

                            return;
                        }
                    }
                }
            }
        }
    }

    public void PauseSound(string soundName)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name.Equals(soundName))
            {
                if (sounds[i].bgm)
                {
                    bgmPlayer.Pause();

                    return;
                }
                else
                {
                    for (int j = 0; j < sfxPlayers.Length; j++)
                    {
                        if (!sfxPlayers[j].clip == sounds[i].clip)
                        {
                            sfxPlayers[j].Pause();

                            return;
                        }
                    }
                }
            }
        }
    }

    public void StopSound(string soundName)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name.Equals(soundName))
            {
                if (sounds[i].bgm)
                {
                    bgmPlayer.Stop();

                    return;
                }
                else
                {
                    for (int j = 0; j < sfxPlayers.Length; j++)
                    {
                        if (!sfxPlayers[j].clip == sounds[i].clip)
                        {
                            sfxPlayers[j].Stop();

                            return;
                        }
                    }
                }
            }
        }
    }
}
