using BIS.Animators;
using BIS.Entities;
using BIS.FSM;
using DG.Tweening;
using UnityEngine;

namespace BIS.Players
{
    public class PlayerChillAttackState : EntityState
    {
        private Player _player;

        public PlayerChillAttackState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
            _player = entity as Player;
        }



        public override void Enter()
        {
            base.Enter();
            Sequence seq = DOTween.Sequence();
            seq.Append(_entity.transform.DOMoveX(_entity.transform.position.x - 2f, 0.25f));
            seq.Append(_entity.transform.DOMoveX(_entity.transform.position.x + 2f, 0.05f));
            seq.JoinCallback(() => HandleAttack())
                .OnComplete(() => _player.ChangeState("IDLE"));

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
    }
}

