using BIS.Animators;
using BIS.Entities;
using BIS.FSM;
using System;
using UnityEngine;

namespace BIS.Enemys
{
    public class EnemyAttackState : EntityState
    {
        private EntityAnimatorTrigger _animationTrigger;
        private Enemy _enemy;
        public EnemyAttackState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
            _animationTrigger = entity.GetCompo<EntityAnimatorTrigger>();
            _enemy = entity as Enemy;
        }

        public override void Enter()
        {
            base.Enter();

            _animationTrigger.OnAttackTrigger += HandleAttack;
            _animationTrigger.OnAnimationEnd += HandleAnimationEnd;
        }

        private void HandleAnimationEnd()
        {
            _enemy.ChangeState("IDLE");
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
                    entity.GetCompo<EntityHealth>().ApplyDamage(_entity.Stat.attackPower.GetValue());
                }
            }
        }

        public override void Exit()
        {
            _animationTrigger.OnAttackTrigger -= HandleAttack;
            _animationTrigger.OnAnimationEnd -= HandleAnimationEnd;
            base.Exit();
        }
    }
}