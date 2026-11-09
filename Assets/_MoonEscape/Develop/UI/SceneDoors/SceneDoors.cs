using UnityEngine;

public class SceneDoors : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;

    public void OpenDoors()
    {
        _animator.SetInteger("State", 1);
    }

    public void CloseDoors()
    {
        _animator.SetInteger("State", 2);
    }
}
