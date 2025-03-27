using BIS.Core;
using BIS.Entities;
using BIS.FSM;
using System.Collections.Generic;
using UnityEngine;

namespace BIS.Enemys
{
    public class Enemy : Entity
    {
        [field: SerializeField] public Transform AttackTrm { get; private set; }
        [Header("Stats")]
        public List<StateSO> states;

        private StateMachine _stateMachine;

        private float attackRange = 5;


        protected override void AfterInitialize()
        {
            base.AfterInitialize(); //모든 컴포넌트와 이벤트 구독 완료 상태
            _stateMachine = new StateMachine(states, this);
            attackRange = Stat.attackRange.GetValue();
            EntityHealth health = GetCompo<EntityHealth>();
            health.OnDead += HandleDeadEvent;
            health.OnHit += HandleHitEvent;

        }

        private void HandleHitEvent()
        {

        }
        private void OnDisable()
        {
            EntityHealth health = GetCompo<EntityHealth>();
            health.OnDead -= HandleDeadEvent;
            health.OnHit -= HandleHitEvent;
            _stateMachine.AllDestory();
        }
        private void HandleDeadEvent(GameObject gameObject)
        {
            StopAllCoroutines();
        }

        public bool IsAttackRangeCollider()
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackTrm.position, attackRange, Define.MLayerMask.WhatIsPlayer);

            if (hitEnemies.Length <= 0)
                return false;

            return true;
        }

        public Collider2D[] GetInAttackRangeCollider()
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackTrm.position, attackRange, Define.MLayerMask.WhatIsPlayer);

            if (hitEnemies.Length <= 0)
                return null;

            return hitEnemies;
        }

        private void Start()
        {
            _stateMachine.Initalize("MOVE"); //IDLE상태로 시작
        }

        private void Update()
        {
            _stateMachine.UpdateFSM();
        }

        public void ChangeState(string newStateName)
        {
            _stateMachine.ChangeState(newStateName);
        }


        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(AttackTrm.position, attackRange);
            Gizmos.color = Color.white;
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }
    }
}

