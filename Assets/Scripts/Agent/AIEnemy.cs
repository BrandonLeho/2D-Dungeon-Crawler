using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AIEnemy : MonoBehaviour
{
    [SerializeField]
    private List<SteeringBehaviour> steeringBehaviours;

    [SerializeField]
    private List<Detector> detectors;

    [SerializeField]
    private AIData aiData;

    [SerializeField]
    private float detectionDelay = 0.05f, aiUpdateDelay = 0.06f, attackDelay = 1f;

    [SerializeField]
    private float attackDistance = 0.5f;

    //Inputs sent from the Enemy AI to the Enemy controller
    public UnityEvent OnAttackPressed;
    public UnityEvent<Vector2> OnMovementInput, OnPointerInput;

    [SerializeField]
    private Vector2 movementInput;

    [SerializeField]
    private ContextSolver movementDirectionSolver;

    [SerializeField]
    private GameObject bars;

    public bool following = false, isStunned;

    private void Start()
    {
        //Detecting Player and Obstacles around
        bars.SetActive(false);
        InvokeRepeating("PerformDetection", 0, detectionDelay);
    }

    private void PerformDetection()
    {
        foreach (Detector detector in detectors)
        {
            detector.Detect(aiData);
        }
    }

    private void FixedUpdate()
    {
        if(isStunned)
            return;
        
        //Enemy AI movement based on Target availability
        if (aiData.currentTarget != null)
        {
            //Looking at the Target
            OnPointerInput?.Invoke(aiData.currentTarget.position);
            if (following == false)
            {
                following = true;
                StartCoroutine(ChaseAndAttack());
            }
        }
        else if (aiData.GetTargetsCount() > 0)
        {
            //Target acquisition logic
            aiData.currentTarget = aiData.targets[0];
        }
        OnMovementInput?.Invoke(movementInput); //Moving the Agent
    }

    private IEnumerator ChaseAndAttack()
    {
        if (aiData.currentTarget == null)
        {
            //Stopping Logic
            //Debug.Log("Stopping");
            movementInput = Vector2.zero;
            following = false;
            bars.SetActive(false);
            yield break;
        }
        else
        {
            bars.SetActive(true);
            float distance = Vector2.Distance(aiData.currentTarget.position, transform.position);

            if (distance < attackDistance && !isStunned)
            {
                //Attack logic
                movementInput = Vector2.zero;
                OnAttackPressed?.Invoke();
                yield return new WaitForSeconds(attackDelay);
                StartCoroutine(ChaseAndAttack());
            }
            else
            {
                //Chase logic
                movementInput = movementDirectionSolver.GetDirectionToMove(steeringBehaviours, aiData);
                yield return new WaitForSeconds(aiUpdateDelay);
                StartCoroutine(ChaseAndAttack());
            }

        }

    }
//movementInput = Vector2.zero;
    public void Stunned(bool isStunned)
    {
        this.isStunned = isStunned;
        if(isStunned)
        {
            
            StartCoroutine(WaitForKnockback());
        }
        else
        {
            gameObject.GetComponent<Rigidbody2D>().mass = 10;
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        
    }

    IEnumerator WaitForKnockback()
    {
        float time = 0;
        while(time < 0.25f)
        {
            StopCoroutine(ChaseAndAttack());
            time += Time.deltaTime;
            yield return null;
        }
        gameObject.GetComponent<Rigidbody2D>().mass = 100;
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll; 
    }
}