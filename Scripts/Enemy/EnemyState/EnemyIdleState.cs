using BIS.Animators;
using BIS.Entities;
using BIS.FSM;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BIS.Enemys
{
    public class EnemyIdleState : EntityState
    {
        private Enemy _enemy;
        public EnemyIdleState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
            _enemy = _entity as Enemy;
        }

        public override void Enter()
        {
            base.Enter();
            _entity.StartCoroutine(AttackDelay());
        }


        private IEnumerator AttackDelay()
        {
            yield return new WaitForSeconds(_entity.Stat.attackCool.GetValue());

            _enemy.ChangeState("MOVE");
        }

    }
}