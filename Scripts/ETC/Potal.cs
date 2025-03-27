using BIS.Entities;
using BIS.Managers;
using BIS.Players;
using UnityEngine;

public class Potal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            player.GetCompo<EntityHealth>().ApplyDamage(999999);
            Manager.Camera.ShakeCamera(Vector2.one * 3, 3, 3, 0.25f);

        }
    }
}
