using BIS.Animators;
using BIS.Entities;
using BIS.ETC;
using BIS.FSM;
using BIS.Managers;
using System;
using UnityEngine;

namespace BIS.Enemys
{
    public class EnemySuitPepeState : EntityState
    {
        private EntityAnimatorTrigger _trigger;
        private Enemy _enemy;
        public EnemySuitPepeState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
            _trigger = entity.GetCompo<EntityAnimatorTrigger>();
            _enemy = entity as Enemy;
        }

        public override void Enter()
        {
            base.Enter();
            _trigger.OnAttackTrigger += HandleAttack;
            _trigger.OnAnimationEnd += HandleEnd;

        }

        private void HandleEnd()
        {
            _enemy.ChangeState("IDLE");
        }

        public override void Exit()
        {

            _trigger.OnAttackTrigger -= HandleAttack;
            _trigger.OnAnimationEnd -= HandleEnd;
            base.Exit();
        }

        private void HandleAttack()
        {
            GameObject obj = Manager.Resource.Instantiate("Glass");
            obj.transform.position = _entity.transform.position;
            obj.GetComponent<Glass>().SetMovement(new Vector2(1, 0), 10, _entity.Stat.attackPower.GetValue(), 2);
        }
    }

}
