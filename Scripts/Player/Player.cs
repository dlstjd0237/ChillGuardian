using BIS.Core;
using BIS.Entities;
using BIS.FSM;
using BIS.Managers;
using System.Collections.Generic;
using UnityEngine;

namespace BIS.Players
{
    public class Player : Entity
    {
        [field: SerializeField] public Transform AttackTrm { get; private set; }
        [Header("Stats")]
        public List<StateSO> states;

        private StateMachine _stateMachine;

        private float attackRange = 1;


        protected override void AfterInitialize()
        {
            base.AfterInitialize(); //모든 컴포넌트와 이벤트 구독 완료 상태
            attackRange = Stat.attackRange.GetValue();
            _stateMachine = new StateMachine(states, this);
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
            Manager.Resource.Instantiate("Particle_Pop").transform.position = transform.position;
        }

        public bool IsAttackRangeCollider()
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackTrm.position, attackRange, Define.MLayerMask.WhatIsEnemy);

            if (hitEnemies.Length <= 0)
                return false;

            return true;
        }

        public Collider2D[] GetInAttackRangeCollider()
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackTrm.position, attackRange, Define.MLayerMask.WhatIsEnemy);

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
    }
}
