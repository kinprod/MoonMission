using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    private bool _isDeath = false;

    private void Awake()
    {
        _isDeath = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !_isDeath)
        {
            collision.GetComponent<Player.Player>().PlayerDeath?.Invoke();
            _isDeath = true;
        }
    }
}
