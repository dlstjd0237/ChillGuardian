using BIS.Animators;
using BIS.Entities;
using BIS.FSM;
using BIS.Managers;
using System;
using UnityEngine;

namespace BIS.Enemys
{
    public class EnemyGiantPepeAttackState : EntityState
    {
        private Enemy _enemy;
        private EntityAnimatorTrigger _trigger;
        public EnemyGiantPepeAttackState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
            _enemy = entity as Enemy;
            _trigger = entity.GetCompo<EntityAnimatorTrigger>();
        }

        public override void Enter()
        {
            base.Enter();
            _trigger.OnAttackTrigger += HandleAttack;
            _trigger.OnAnimationEnd += HandleAnimationEnd;
        }

        private void HandleAnimationEnd()
        {
            _enemy.ChangeState("IDLE");
        }

        public override void Exit()
        {
            _trigger.OnAttackTrigger -= HandleAttack;
            _trigger.OnAnimationEnd -= HandleAnimationEnd;
            base.Exit();
        }

        private void HandleAttack()
        {
            Collider2D[] collider2D = _enemy.GetInAttackRangeCollider();
            if (collider2D == null)
                return;
            foreach (var item in collider2D)
            {
                if (item.TryGetComponent<Entity>(out Entity entity))
                {
                    Manager.Camera.ShakeCamera(Vector2.one * 3, 3, 3, 0.4f);
                    entity.GetCompo<EntityHealth>().ApplyDamage(_entity.Stat.attackPower.GetValue());
                }
            }
        }
    }
}

