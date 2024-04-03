using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Knockback : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb2d;

    [field: SerializeField] [field: Range(0f, 1f)] private float delay = 0.1f;    
    public UnityEvent OnBegin, OnDone;

    public void PlayFeedback( float strength, GameObject sender)
    {
        StopAllCoroutines();
        OnBegin?.Invoke();
        Vector2 direction = (transform.position - sender.transform.position).normalized;
        rb2d.AddForce(direction * strength, ForceMode2D.Impulse);
        StartCoroutine(Reset());
    }

    public void PlayFeedbackV2( float strength, Vector2 sender)
    {
        StopAllCoroutines();
        OnBegin?.Invoke();
        Vector2 direction = ((Vector2)transform.position - sender).normalized;
        rb2d.AddForce(direction * strength, ForceMode2D.Impulse);
        StartCoroutine(Reset());
    }

    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(delay);
        rb2d.velocity = Vector3.zero;
        OnDone?.Invoke();
    }
}
