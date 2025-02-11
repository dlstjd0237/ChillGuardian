using BIS.Managers;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace BIS.Entities
{

    public class EntityHealth : MonoBehaviour, IEntityComponent
    {
        private Entity _entity;

        private float currentHealth;
        private float maxHealth;

        public event Action OnHit;
        public event Action<GameObject> OnDead;

        [SerializeField] private UnityEvent<float, float> _healthChangeEvent;

        private bool _isMiss = false;
        public void Initalize(Entity entity)
        {
            _entity = entity;
            maxHealth = _entity.Stat.maxHealth.GetValue();
            currentHealth = maxHealth;
            _healthChangeEvent?.Invoke(currentHealth, maxHealth);
        }
        public void Invincibility()
        {
            _healthChangeEvent?.Invoke(currentHealth, maxHealth);
            maxHealth = int.MaxValue;
            currentHealth = maxHealth;
        }

        public void SetIsMissed(bool value)
        {
            _isMiss = value;
        }

        public void ApplyDamage(float damageAmount)
        {
            if (_isMiss)
                return;

            if (_entity.gameObject.layer == LayerMask.GetMask("Player"))
            {
                currentHealth -= damageAmount - GameManager.Instance.buffs.hp;
            }
            else
                currentHealth -= damageAmount;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            OnHit?.Invoke();
            _healthChangeEvent?.Invoke(currentHealth, maxHealth);
            if (Mathf.Approximately(currentHealth, 0))
            {
                Debug.Log($"{gameObject.name} Dead");
                Manager.Resource.Instantiate("Particle_Pop").transform.position = transform.position;
                OnDead?.Invoke(_entity.gameObject);
                Manager.Resource.Destroy(_entity.gameObject);
            }
        }


    }
}
