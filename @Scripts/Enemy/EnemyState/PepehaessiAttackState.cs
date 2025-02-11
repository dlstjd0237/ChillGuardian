using BIS.Animators;
using BIS.Entities;
using BIS.FSM;
using DG.Tweening;
using UnityEngine;

namespace BIS.Enemys
{
    public class PepehaessiAttackState : EntityState
    {
        private Enemy _enemy;
        public PepehaessiAttackState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
            _enemy = entity as Enemy;
        }

        public override void Enter()
        {
            base.Enter();
            Sequence seq = DOTween.Sequence();
            seq.Append(_entity.transform.DOMoveX(_entity.transform.position.x - 2f, 0.25f));
            seq.Append(_entity.transform.DOMoveX(_entity.transform.position.x + 3f, 0.05f));
            seq.JoinCallback(() => HandleAttack());
            seq.Append(_entity.transform.DOMoveX(_entity.transform.position.x - 1f, 0.05f))
                .OnComplete(() => _enemy.ChangeState("IDLE"));
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
    }

}
