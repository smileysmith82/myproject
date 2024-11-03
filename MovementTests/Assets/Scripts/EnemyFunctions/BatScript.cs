using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Random = System.Random;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class BatScript : MonoBehaviour
{
   [Header("Bat Settings")]
   public float detectionRadius = 5f;
   public float speed = 3f;
   public float obstacleAvoidanceDistance = 1f;
   public Transform hangingPosition;
   public LayerMask obstacleLayer;
   public LayerMask platformLayer;

   private Transform player;
   private Animator animator;
   private bool isChasing = false;
   private bool isReturning = false;
   private Vector2 startPosition;
   void Start()
   {
      player = GameObject.FindGameObjectWithTag("Player").transform;
      animator = GetComponent<Animator>();
      startPosition = transform.position;
   }
   void Update()
   {
      if (player == null) return;

      float distanceToPlayer = Vector2.Distance(startPosition, player.position);
      if (distanceToPlayer < detectionRadius  && !isChasing)
      {
         StartChasingPlayer();
      }
      else if (distanceToPlayer >= detectionRadius && isChasing)
      {
         StopChasingPlayer();
      }
      if (isChasing)
      {
         ChasePlayer();
      }
      else if (isReturning)
      {
         ReturnToHangingPosition();
      }
      
   }
   void StartChasingPlayer()
   {
      isChasing = true;
      isReturning = false;
      animator.ResetTrigger("Reattach");
      animator.SetTrigger("StartFlying");
   }
   void StopChasingPlayer()
   {
      isChasing = false;
      isReturning = true;
   }
   void ChasePlayer()
   {
      Vector2 direction = ((Vector2)player.position - (Vector2)transform.position).normalized;
      
      float randomOffsetX = UnityEngine.Random.Range(-0.2f, 0.2f);
      float randomOffsetY = UnityEngine.Random.Range(-0.2f, 0.2f);
      Vector2 randomOffset = new Vector2(randomOffsetX, randomOffsetY);
      Vector2 adjustedDirection = (direction + randomOffset).normalized;
      
      float sphereRadius = 0.5f;
      RaycastHit2D hit = Physics2D.CircleCast(transform.position, sphereRadius, adjustedDirection, obstacleAvoidanceDistance, obstacleLayer);
      
      if (hit.collider != null)
      {
         Vector2 rightDirection = Quaternion.Euler(0, 0, 45) * adjustedDirection;
         Vector2 leftDirection = Quaternion.Euler(0, 0, -45) * adjustedDirection;
         
         RaycastHit2D rightHit = Physics2D.CircleCast(transform.position, sphereRadius, rightDirection, obstacleAvoidanceDistance, obstacleLayer);
         RaycastHit2D leftHit = Physics2D.CircleCast(transform.position, sphereRadius, leftDirection, obstacleAvoidanceDistance, obstacleLayer);

         if (rightHit.collider == null && leftHit.collider != null)
         {
            adjustedDirection = rightDirection;
         }
         
         else if (leftHit.collider == null && rightHit.collider != null)
         {
            adjustedDirection = leftDirection;
         }
         
         else if (rightHit.collider == null && leftHit.collider == null)
         {
            adjustedDirection = Vector2.Distance(transform.position + (Vector3)rightDirection, player.position) <
                                Vector2.Distance(transform.position + (Vector3)leftDirection, player.position)
                                ? rightDirection
                                : leftDirection;
         }
      }

      transform.position = (Vector2)transform.position + adjustedDirection * speed * Time.deltaTime;
   }
   void ReturnToHangingPosition()
   {
      float distanceToHangingPos = Vector2.Distance(transform.position, hangingPosition.position);

      if (distanceToHangingPos > 0.3f)
      {
         Vector2 direction = ((Vector2)hangingPosition.position - (Vector2)transform.position).normalized;
         RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, obstacleLayer);
         
         if (hit.collider != null)
         {
            direction += Vector2.up;
         }
         transform.position = (Vector2)transform.position + direction * speed * Time.deltaTime;
      }
      else
      {
         transform.position = hangingPosition.position;
         animator.ResetTrigger("StartFlying");
         animator.SetTrigger("Reattach");
         isReturning = false;
      }
   }

   private void OnDrawGizmosSelected()
   {
      Gizmos.color = Color.green;
      Gizmos.DrawWireSphere(startPosition, detectionRadius);
   }
}