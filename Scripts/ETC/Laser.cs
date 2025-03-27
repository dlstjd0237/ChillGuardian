using UnityEngine;
using DG.Tweening;
using BIS.Enemys;
using BIS.Entities;

public class Laser : MonoBehaviour
{
    private float _damage;
    private void Start()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOScaleY(0.1f, 0.25f));
        seq.AppendInterval(0.15f);
        seq.AppendCallback(() => Destroy(gameObject));
    }

    public void SetDamage(float damage)
    {
        _damage = damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.GetCompo<EntityHealth>().ApplyDamage(_damage);
        }
    }
}
