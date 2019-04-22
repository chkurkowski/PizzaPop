using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;

    public static AudioManager instance;

    private void Start()
    {
        //Play("SplashMusic");
    }

    private String[] audioLines =  { "EccoFatto", "Dont", "KeepTopping", "MoltaBella", "OttimoLavoro", "stopSittingOnHands", "wantMeToMakePizza", "watsAMatta", "wattaYouDoin" };
    private String[] impactLines = { "Impact1", "Impact2", "Impact3", "Impact4" };


    void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }


    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
            return;

        if (s.loop)
        {
            s.source.Play();
        }
        else
        {
            s.source.PlayOneShot(s.clip);
        }
    }

    public void PlayWithRandomPitch(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
            return;

        float randomNum = UnityEngine.Random.Range(-1f, 1f);

        s.source.pitch += randomNum;
        s.source.PlayOneShot(s.clip);
        s.source.pitch = 1.0f;

    }

    public void PlayRandomImpactSound()
    {
        Play(impactLines[UnityEngine.Random.Range(0, impactLines.Length)]);
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
            return;
        s.source.Stop();
    }

    public void PlayRandomVO()
    {
        Play(audioLines[UnityEngine.Random.Range(0, audioLines.Length)]);
    }
}
