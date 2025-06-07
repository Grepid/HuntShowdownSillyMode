using UnityEngine;

public class AnimationPlayer : MonoBehaviour
{
    public Animator animator;

    public void PlayAnimation(string AnimationName)
    {
         animator.Play(AnimationName);
    }
}
