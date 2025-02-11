using BIS.Animators;
using BIS.Entities;
using BIS.FSM;
using UnityEngine;

namespace BIS.Enemys
{
    public class EnemyMoveState : EntityState
    {
        private EntityMover _mover;
        private Enemy _enemy;
        public EnemyMoveState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
            _enemy = entity as Enemy;
            _mover = entity.GetCompo<EntityMover>();
        }

        public override void Update()
        {
            base.Update();
            _mover.SetMovement(_entity.Stat.moveSpeed.GetValue() * Time.deltaTime);


            if (_enemy.IsAttackRangeCollider() == true)
            {
                _enemy.ChangeState("ATTACK");
            }
        }

        public override void Exit()
        {
            _mover.StopImmediately();
            base.Exit();
        }

    }
}