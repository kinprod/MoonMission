using UnityEngine;

public class DrawLine : MonoBehaviour
{
    [SerializeField]
    private LineRenderer _line;

    [SerializeField]
    private Transform _player;
    [SerializeField]
    private Transform _harpoon;

    private void Awake()
    {
        _line.positionCount = 2;

        _line.SetPosition(0, _player.position);
        _line.SetPosition(1, _harpoon.position);
    }

    private void FixedUpdate()
    {
        _line.SetPosition(0, _player.position);
        _line.SetPosition(1, _harpoon.position);
    }
}
