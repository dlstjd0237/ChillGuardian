using BIS.Animators;
using BIS.Entities;
using BIS.FSM;
using UnityEngine;
namespace BIS.Players
{
    public class PlayerMoveState : EntityState
    {
        private Player _player;
        private EntityMover _mover;
        public PlayerMoveState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
            _mover = entity.GetCompo<EntityMover>();
            _player = entity as Player;
        }

        public override void Enter()
        {

            base.Enter();
            Debug.Log("지금 진입했지비");
        }

        public override void Update()
        {
            base.Update();
            _mover.SetMovement(-(_entity.Stat.moveSpeed.GetValue() + GameManager.Instance.buffs.movement) * Time.deltaTime);
            if (_player.IsAttackRangeCollider() == true)
            {
                _player.ChangeState("ATTACK");
            }
        }

        public override void Exit()
        {
            _mover.StopImmediately();
            base.Exit();
        }
    }
}
