using BIS.Animators;
using BIS.Entities;
using BIS.FSM;
using System.Collections;
using UnityEngine;

namespace BIS.Players
{
    public class PlayerIdleState : EntityState
    {
        private Player _player;
        public PlayerIdleState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
            _player = entity as Player;
        }

        public override void Enter()
        {
            base.Enter();
            _entity.StartCoroutine(AttackDelay());
        }


        private IEnumerator AttackDelay()
        {
            yield return new WaitForSeconds(_entity.Stat.attackCool.GetValue());

            _player.ChangeState("MOVE");
        }
    }
}