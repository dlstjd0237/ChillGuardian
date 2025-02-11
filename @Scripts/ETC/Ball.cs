using BIS.Enemys;
using BIS.Entities;
using BIS.Init;
using BIS.Managers;
using System.Collections;
using UnityEngine;

namespace BIS.ETC
{
    public class Ball : InitBase
    {
        protected Rigidbody2D _rigidbody2D;
        protected float _damage;

        public override bool Init()
        {
            if (base.Init() == false)
                return false;

            _rigidbody2D = GetComponent<Rigidbody2D>();

            return true;
        }


        public void SetMovement(Vector2 dir, float speed, float damage, float lifeTime)
        {
            StartCoroutine(ObejctActive(false, lifeTime));
            _rigidbody2D.linearVelocity = dir * speed;
            _damage = damage;
        }

        private IEnumerator ObejctActive(bool value, float time)
        {
            yield return new WaitForSeconds(time);
            gameObject.SetActive(value);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<Enemy>(out Enemy entity))
            {
                entity.GetCompo<EntityHealth>().ApplyDamage(_damage);
                Manager.Resource.Instantiate("BoomParticle");
                Destroy(gameObject);
            }
        }


    }
}

