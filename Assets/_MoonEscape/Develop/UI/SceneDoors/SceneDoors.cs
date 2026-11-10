using UnityEngine;

public class SceneDoors : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private AudioSource _audioSource;

    public void OpenDoors()
    {
        _audioSource.Play();
        _animator.SetInteger("State", 1);
    }

    public void CloseDoors()
    {
        _audioSource.Play();
        _animator.SetInteger("State", 2);
    }
}
