using BIS.Animators;
using BIS.Entities;
using BIS.FSM;
using System;
using UnityEngine;

namespace BIS.Players
{
    public class PlayerAttackState : EntityState
    {
        private EntityAnimatorTrigger _entityAnimatorTrigger;
        private Player _player;
        public PlayerAttackState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
            _entityAnimatorTrigger = entity.GetCompo<EntityAnimatorTrigger>();
            _player = entity as Player;
        }

        public override void Enter()
        {
            base.Enter();
            _entityAnimatorTrigger.OnAttackTrigger += HandleAttack;
            _entityAnimatorTrigger.OnAnimationEnd += HandleEnd;
        }

        private void HandleEnd()
        {
            _player.ChangeState("IDLE");
        }

        private void HandleAttack()
        {
            Collider2D[] collider2D = _player.GetInAttackRangeCollider();
            foreach (var item in collider2D)
            {
                if (item.TryGetComponent<Entity>(out Entity entity))
                {
                    entity.GetCompo<EntityHealth>().ApplyDamage(_entity.Stat.attackPower.GetValue() + GameManager.Instance.buffs.dmg);
                }
            }

        }

        public override void Exit()
        {
            _entityAnimatorTrigger.OnAnimationEnd += HandleEnd;
            _entityAnimatorTrigger.OnAttackTrigger -= HandleAttack;
            base.Exit();
        }
    }
}