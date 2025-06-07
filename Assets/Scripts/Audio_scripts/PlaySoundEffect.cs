using AudioSystem;
using UnityEngine;

public class PlaySoundEffect : MonoBehaviour
{
    public void PlaySound(string soundName)
    {
        AudioManager.Play(soundName);
    }
}
