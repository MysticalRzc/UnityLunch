using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Animator))]
public class Wander : MonoBehaviour
{
    //追击敌人速度
    public float pursuitSpeed;
    //漫游速度
    public float wanderSpeed;
    //当前速度
    float currentSpeed;

    //改变漫游方向的频率
    public float directionChangeInterval;
    //是否跟随玩家
    public bool followPlayer;

    //玩家当前运动的协程
    Coroutine moveCoroutine;

    Rigidbody2D rb2d;
    Animator animator;

    //要追击目标的地址
    Transform targetTransform;
    //目的地
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
        //unity中快速计算矢量长度的方法
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
                //方法用于计算 Rigidbody2D 的移动，但实际上并没有移动Rigidbody2D。该方法接收三个参数:
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
