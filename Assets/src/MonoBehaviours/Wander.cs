using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Animator))]
public class Wander : MonoBehaviour
{
    //׷�������ٶ�
    public float pursuitSpeed;
    //�����ٶ�
    public float wanderSpeed;
    //��ǰ�ٶ�
    float currentSpeed;

    //�ı����η����Ƶ��
    public float directionChangeInterval;
    //�Ƿ�������
    public bool followPlayer;

    //��ҵ�ǰ�˶���Э��
    Coroutine moveCoroutine;

    Rigidbody2D rb2d;
    Animator animator;

    //Ҫ׷��Ŀ��ĵ�ַ
    Transform targetTransform;
    //Ŀ�ĵ�
    Vector3 endPosition;
    float currentAngle = 0.0f;

    CircleCollider2D circleCollider;

    private void Start()
    {
        animator = GetComponent<Animator>();
        currentSpeed = wanderSpeed;
        rb2d = GetComponent<Rigidbody2D>();
        StartCoroutine(WanderRoutine());
        circleCollider = GetComponent<CircleCollider2D>();
    }

    public IEnumerator WanderRoutine()
    {
        while (true)
        {
            ChooseNewEndPoint();
            if(moveCoroutine!= null)
            {
                StopCoroutine(moveCoroutine);
            }
            moveCoroutine = StartCoroutine(Move(rb2d, currentSpeed));
            yield return new WaitForSeconds(directionChangeInterval);
        }
    }
    
    void ChooseNewEndPoint()
    {
        currentAngle = Random.Range(0,360);
        currentAngle = Mathf.Repeat(currentAngle, 360);
        endPosition += Vector3FromAngle(currentAngle);
    }

    Vector3 Vector3FromAngle(float inputAngleDegrees)
    {
        float inputAngleRedians = inputAngleDegrees * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(inputAngleRedians), Mathf.Sin(inputAngleRedians), 0);
    }

    public IEnumerator Move(Rigidbody2D rigidBodyToMove, float speed)
    {
        //Debug.Log("move coroutine is start");   
        //unity�п��ټ���ʸ�����ȵķ���
        float remainingDistance = (transform.position - endPosition).sqrMagnitude;
        int times = 1000;
        while (remainingDistance > float.Epsilon && times-- >0)
        {
            // When in pursuit, the targetTransform won't be null.
            if (targetTransform != null)
            {
                endPosition = targetTransform.position;
            }

            if (rigidBodyToMove != null)
            {
                //Debug.Log("is moving");
                animator.SetBool("isWalking", true);
                //�������ڼ��� Rigidbody2D ���ƶ�����ʵ���ϲ�û���ƶ�Rigidbody2D���÷���������������:
                Vector3 newPosition = Vector3.MoveTowards(rigidBodyToMove.position, endPosition, speed * Time.deltaTime);
                rb2d.MovePosition(newPosition);
                remainingDistance = (transform.position - endPosition).sqrMagnitude;
            }
            yield return new WaitForFixedUpdate();
        }

        // enemy has reached endPosition and waiting for new direction selection
        animator.SetBool("isWalking", false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("player") && followPlayer)
        {
            currentSpeed = pursuitSpeed;

            // Set this variable so the Move coroutine can use it to follow the player.
            targetTransform = collision.gameObject.transform;

            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }
            // At this point, endPosition is now player object's transform, ie: will now move towards the player
            moveCoroutine = StartCoroutine(Move(rb2d, currentSpeed));
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            animator.SetBool("isWalking", false);
            currentSpeed = wanderSpeed;

            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }

            // Since we no longer have a target to follow, set this to null
            targetTransform = null;
        }
    }

    private void OnDrawGizmos()
    {
        if(circleCollider != null)
        {
            Gizmos.DrawWireSphere(transform.position, circleCollider.radius);
        }
    }
}
