using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float SpawnDelay = 0.5f;

    private void Start()
    {
        StartCoroutine(StartSpawn());
    }

    private IEnumerator StartSpawn()
    {
        yield return new WaitForSeconds(1);
    }
}
