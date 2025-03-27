using BIS.Animators;
using BIS.Entities;
using BIS.ETC;
using BIS.FSM;
using BIS.Managers;
using UnityEngine;

namespace BIS.Players
{
    public class PlayerWinstonAttackState : EntityState
    {
        private Player _player;
        public PlayerWinstonAttackState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
        }

        public override void Enter()
        {
            base.Enter();
            GameObject obj = Manager.Resource.Instantiate("Ball");
            obj.transform.position = _player.transform.position;
            obj.GetComponent<Ball>().SetMovement(new Vector2(-1, 0), 10, _entity.Stat.attackPower.GetValue() + GameManager.Instance.buffs.dmg, 2);
            _player.ChangeState("IDLE");
        }

    }

}
