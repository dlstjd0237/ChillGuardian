using BIS.Enemys;
using BIS.Entities;
using UnityEngine;

namespace BIS.ETC
{
    public class BoomParticle : MonoBehaviour
    {
        private float _damage;
        public void SetDamage(float damage)
        {
            _damage = damage;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {

            if (collision.TryGetComponent<Enemy>(out Enemy enemy))
            {
                enemy.GetCompo<EntityHealth>().ApplyDamage(_damage);
                Destroy(gameObject);
            }
        }
    }
}

