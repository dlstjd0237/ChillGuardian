using BIS.Animators;
using BIS.Entities;
using UnityEngine;

namespace BIS
{
    public class EntityRenderer : AnimateRenderer, IEntityComponent
    {
        protected Entity _entity;
        [field: SerializeField] public float FacingDirection { get; private set; } = 1;
        [field: SerializeField] public SpriteRenderer SpriteRenderer { get; private set; }
        public void Initalize(Entity entity)
        {
            _entity = entity;
        }

        #region FlipController

        public void Flip()
        {
            FacingDirection *= -1;
            //_entity.transform.Rotate(0, 180f, 0);
            _entity.transform.rotation = (_entity.transform.rotation.eulerAngles.y == 0) ? Quaternion.Euler(0, 180, 0) : Quaternion.Euler(0, 0, 0);
        }

        public void FlipController(float normalizeXMove)
        {
            if (Mathf.Abs(FacingDirection + normalizeXMove) < 0.5f)
                Flip();
        }

        #endregion
    }
}
