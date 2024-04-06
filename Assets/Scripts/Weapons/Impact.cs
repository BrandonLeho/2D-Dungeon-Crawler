using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Impact : MonoBehaviour
{
    [field: SerializeField] public UnityEvent OnActivate { get; set; }
    public void DestroyAfterAnimation()
    {
        Destroy(gameObject);
    }

    public void DoOnAnimation(float time)
    {
        OnActivate?.Invoke();
    }

    
}
