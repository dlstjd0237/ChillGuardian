using BIS.Animators;
using BIS.Entities;
using BIS.FSM;
using BIS.Managers;
using System;
using UnityEngine;

namespace BIS.Players
{
    public class PlayerMKAttackState : EntityState
    {
        private EntityAnimatorTrigger _trigger;
        private Player _player;
        public PlayerMKAttackState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
            _player = entity as Player;
            _trigger = entity.GetCompo<EntityAnimatorTrigger>();
        }

        public override void Enter()
        {
            base.Enter();
            _trigger.OnAttackTrigger += HandleAttackEvent;
            _trigger.OnAnimationEnd += HandleEnd;
        }

        private void HandleEnd()
        {
            _player.ChangeState("IDLE");
        }

        private void HandleAttackEvent()
        {
            GameObject go = Manager.Resource.Instantiate("Laser");
            go.transform.position = _entity.transform.position;
            go.GetComponent<Laser>().SetDamage(_entity.Stat.attackPower.GetValue() + GameManager.Instance.buffs.dmg);
        }

        public override void Exit()
        {
            _trigger.OnAnimationEnd -= HandleEnd;
            _trigger.OnAttackTrigger -= HandleAttackEvent;
            base.Exit();
        }
    }
}

