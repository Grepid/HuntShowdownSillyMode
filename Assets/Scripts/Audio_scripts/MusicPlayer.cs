using AudioSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    public bool MainScene = true;
    public float musicFadeTime;
    private Dictionary<string, float> lastSoundTime = new Dictionary<string, float>()
    {
        {"HubMusic",0f},
        {"TavernMusic",0f},
        {"BossMusic",0f},
        {"DungeonMusic",0f},
        {"StartMenu",0f}
    };
    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "BossDies") return;
        if (MainScene == true)
        {
            BedroomMusic();
        }
        else
        {
            MainMenuMusic();
        }
    }
    public void HubMusic()
    {
        var play = AudioManager.Play("HubMusic");
        if(AudioManager.currentMusic != null)
        {
            if (lastSoundTime.ContainsKey(AudioManager.currentMusic.SoundClass.name))
            {
                lastSoundTime[AudioManager.currentMusic.SoundClass.name] = AudioManager.currentMusic.AudioSource.time;
            }

            if(lastSoundTime.TryGetValue("HubMusic", out float lastTime))
            {
                play.AudioSource.time = lastTime;
            }
        }
        AudioManager.CrossFade(play, AudioManager.currentMusic, musicFadeTime);
        AudioManager.currentMusic = play;
    }

    public void TavernMusic()
    {
        var play = AudioManager.Play("TavernMusic");
        if (AudioManager.currentMusic != null)
        {
            if (lastSoundTime.ContainsKey(AudioManager.currentMusic.SoundClass.name))
            {
                lastSoundTime[AudioManager.currentMusic.SoundClass.name] = AudioManager.currentMusic.AudioSource.time;
            }

            if (lastSoundTime.TryGetValue("TavernMusic", out float lastTime))
            {
                play.AudioSource.time = lastTime;
            }
        }
        AudioManager.CrossFade(play, AudioManager.currentMusic, musicFadeTime);
        AudioManager.currentMusic = play;
    }
    public void BedroomMusic()
    {
        var play = AudioManager.Play("StartMenu");
        if (AudioManager.currentMusic != null)
        {
            if(AudioManager.currentMusic.SoundClass != null)
            {
                if (lastSoundTime.ContainsKey(AudioManager.currentMusic.SoundClass.name))
                {
                    lastSoundTime[AudioManager.currentMusic.SoundClass.name] = AudioManager.currentMusic.AudioSource.time;
                }
            }
            if (lastSoundTime.TryGetValue("StartMenu", out float lastTime))
            {
                play.AudioSource.time = lastTime;
            }
        }
        AudioManager.CrossFade(play, AudioManager.currentMusic, musicFadeTime);
        AudioManager.currentMusic = play;
    }

    public void DungeonMusic()
    {
        var play = AudioManager.Play("DungeonMusic");
        if (AudioManager.currentMusic != null)
        {
            if (lastSoundTime.ContainsKey(AudioManager.currentMusic.SoundClass.name))
            {
                lastSoundTime[AudioManager.currentMusic.SoundClass.name] = AudioManager.currentMusic.AudioSource.time;
            }

            if (lastSoundTime.TryGetValue("DungeonMusic", out float lastTime))
            {
                play.AudioSource.time = lastTime;
            }
        }
        AudioManager.CrossFade(play, AudioManager.currentMusic, musicFadeTime);
        AudioManager.currentMusic = play;
    }

    public void BossMusic()
    {
        var play = AudioManager.Play("BossMusic");
        AudioManager.CrossFade(play, AudioManager.currentMusic, musicFadeTime);
        AudioManager.currentMusic = play;
    }

    public void IntroMusic()
    {
        var play = AudioManager.Play("IntroMusic");
        AudioManager.CrossFade(play, AudioManager.currentMusic, musicFadeTime);
        AudioManager.currentMusic = play;
    }

    public void BossDeathMusic()
    {
        AudioManager.FadeOut(AudioManager.currentMusic, musicFadeTime);
    }
    public void CreditsMusic()
    {
        AudioManager.StopAllAudio();
        var play = AudioManager.PlayPersistent("EndCredits");
    }
    public void MainMenuMusic()
    {
        var play = AudioManager.Play("StartMenu");
        AudioManager.CrossFade(play, AudioManager.currentMusic, musicFadeTime);
        AudioManager.currentMusic = play;
    }
    public void EnterBossArena()
    {
        AudioManager.FadeOut(AudioManager.currentMusic, musicFadeTime);
    }
}
