using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class BulletShellGenerator : ObjectPool
{
    [SerializeField] private float flyDuration = 0.3f;
    [SerializeField] private float flyStrength = 1;
    [SerializeField] private float delay = 0f;

    public void SpawnBulletShell()
    {
        if(delay > 0)
            StartCoroutine(DelayShellEjection());
        else
        {
            var shell = SpawnObject();
            MoveShellInRandomDirection(shell);
        }
    
    }

    private void MoveShellInRandomDirection(GameObject shell)
    {
        shell.transform.DOComplete();
        var randomDirection = Random.insideUnitCircle;
        randomDirection = randomDirection.y > 0 ? new Vector2(randomDirection.x, -randomDirection.y) : randomDirection;
        shell.transform.DOMove(((Vector2)transform.position + randomDirection) * flyStrength, flyDuration).OnComplete(() => shell.GetComponent<AudioSource>().Play());
        shell.transform.DORotate(new Vector3(0, 0, Random.Range(0f, 360f)), flyDuration);
    }

    IEnumerator DelayShellEjection()
    {
        yield return new WaitForSecondsRealtime(delay);
        var shell = SpawnObject();
        MoveShellInRandomDirection(shell);
    }
}
