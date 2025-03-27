using DG.Tweening;
using System;
using UnityEngine;
using BIS.FSM;
using BIS.Stats;
using UnityEngine.UI;
using BIS.Core;

namespace BIS.Entities
{
    public class EntityMover : MonoBehaviour, IEntityComponent, IAfterInitable
    {
        public event Action<Vector2> OnMovement;

        [Header("Collision detect")]
        [SerializeField] private Transform _groundCheckTrm;
        [SerializeField] private Vector2 _checkerSize;
        [SerializeField] private float _checkDistance;

        private EntityStat _stat;

        private Rigidbody2D _rbCompo;
        private EntityRenderer _renderer;

        private float _moveSpeed = 6.0f; //todo : 나중에 스텟시스템으로 변경한다.
        private float _movementX;
        private float _moveSpeedMultiplier, _origialGravity;

        [field: SerializeField] public bool CanManualMove { get; set; } = true;

        private Entity _entity;

        public void Initalize(Entity entity)
        {
            _entity = entity;
            _rbCompo = entity.GetComponent<Rigidbody2D>();
            _renderer = entity.GetCompo<EntityRenderer>();


            _origialGravity = _rbCompo.gravityScale;
            _moveSpeedMultiplier = 1.0f;
        }

        public void AfterInit()
        {
            _stat = _entity.Stat;
            _stat.moveSpeed.ValueChangeEvent += HandleMoveSpeedChange;
            _moveSpeed = _stat.moveSpeed.GetValue();
        }

        private void HandleMoveSpeedChange(float value)
        {
            _moveSpeed = value; ;
        }

        private void OnDestroy()
        {
            _stat.moveSpeed.ValueChangeEvent -= HandleMoveSpeedChange;
        }


        private void FixedUpdate()
        {
            if (CanManualMove)
                _rbCompo.linearVelocityX = _movementX * _moveSpeed * _moveSpeedMultiplier;

            OnMovement?.Invoke(_rbCompo.linearVelocity);
        }

        public void SetMovement(float xMovement)
        {
            _movementX = xMovement;
            _renderer.FlipController(xMovement);
        }

        public void StopImmediately(bool isYAxisToo = false)
        {
            if (isYAxisToo)
                _rbCompo.linearVelocity = Vector2.zero;
            else
                _rbCompo.linearVelocityX = 0;

            _movementX = 0;
        }

        public void SetMovementMultiplier(float value) => _moveSpeedMultiplier = value;
        public void SetGravityMultiplier(float value) => _rbCompo.gravityScale = value;

        public void AddForceToEntity(Vector2 force, ForceMode2D mode = ForceMode2D.Impulse)
        {
            _rbCompo.AddForce(force, mode);
        }

        #region KnockBack

        public void KnockBack(Vector2 force, float time)
        {
            CanManualMove = false;
            StopImmediately(true);
            AddForceToEntity(force);
            DOVirtual.DelayedCall(time, () => CanManualMove = true);
        }

        #endregion

        #region CheckCollision

        public virtual bool IsGroundDetected() => Physics2D.BoxCast(_groundCheckTrm.position, _checkerSize, 0, Vector2.down, _checkDistance, Define.MLayerMask.WhatIsGround);

        #endregion

#if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            if (_groundCheckTrm != null)
            {
                Vector3 offset = new Vector3(0, _checkDistance * 0.5f);
                Gizmos.DrawWireCube(_groundCheckTrm.position - offset, new Vector3(_checkerSize.x, _checkDistance, 1.0f));
            }
        }

#endif
    }
}
