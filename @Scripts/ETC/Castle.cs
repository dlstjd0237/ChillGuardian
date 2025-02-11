using BIS.Core;
using BIS.Enemys;
using BIS.Entities;
using BIS.Managers;
using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Castle : MonoBehaviour
{
    [SerializeField] private GameEventChannelSO _gameEventSO;
    [SerializeField] private ParticleSystem[] _system;
    [SerializeField] private CinemachineCamera _cam;
    private void OnEnable()
    {
        _gameEventSO.AddListener<GameOverEvent>(HandleGameOver);
    }

    private void HandleGameOver(GameOverEvent evt)
    {
        _cam.Priority = 100;
        foreach (var item in _system)
        {
            item.Play();
        }
        StartCoroutine(GameOver());
    }
    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(2);
        SceneControlManager.FadeOut(() => SceneManager.LoadScene("StartScene"));
    }
    private void OnDisable()
    {
        _gameEventSO.RemoveListener<GameOverEvent>(HandleGameOver);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.GetCompo<EntityHealth>().ApplyDamage(999999);
            Manager.Camera.ShakeCamera(Vector2.one * 3, 3, 3, 0.25f);
            GameManager.Instance.GetDamage((int)enemy.Stat.attackPower.GetValue());
        }
    }
}
