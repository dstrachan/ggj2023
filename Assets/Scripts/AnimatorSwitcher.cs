using UnityEditor.Animations;
using UnityEngine;

public class AnimatorSwitcher : MonoBehaviour
{
    [SerializeField] private AnimatorController[] animatorControllers;

    private System.Random _random;

    private void Awake()
    {
        _random = new System.Random();
    }

    private void Start()
    {
        var animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = animatorControllers[_random.Next(animatorControllers.Length)];
    }
}