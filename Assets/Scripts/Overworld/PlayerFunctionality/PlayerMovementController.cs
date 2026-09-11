using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private PlayerInputController inputController;
    [SerializeField] private PlayerController plrController;
    
    [SerializeField] private LayerMask collisionLayer;

    private Vector2 movementInput;
    private Vector3 startPosition;
    private Vector3 targetPosition;
    public Rigidbody2D RigidBody {get; private set;}
    private bool isMoving;

    private CapsuleCollider2D playerCollider;
    private Vector2 colliderSize;

    void Awake()
    {
        RigidBody = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<CapsuleCollider2D>();
        
        colliderSize = playerCollider.bounds.size;
    }
    void OnEnable()
    {
        inputController.MoveRequest += MovePlayer;

    }
    void OnDisable()
    {
        inputController.MoveRequest -= MovePlayer;
    }

    void MovePlayer(Vector2 movement)
    {
        Debug.Log("moving player...");
        movementInput = movement;
        if (movementInput == Vector2.zero)
        {
            isMoving = false; return;
        }

        plrController.facingDirection = movementInput.normalized;

        if (!isMoving)
        {
            Vector2 desiredStartPosition = transform.position;
            Vector2 direction = movementInput.normalized;
            Vector3 desiredTargetPosition = (Vector3) desiredStartPosition + new Vector3(Mathf.Round(direction.x), Mathf.Round(direction.y), 0f) * 0.32f;

            if(Physics2D.OverlapBox(desiredTargetPosition, colliderSize, 0f, collisionLayer))
                return;
            
            startPosition = desiredStartPosition;
            targetPosition = desiredTargetPosition;

            isMoving = true;
        }
    }


    void FixedUpdate()
    {
        if(!isMoving) return;

        Vector2 newPosition = Vector2.MoveTowards(RigidBody.position, targetPosition, plrController.GetRuntimeData().movementSpeed * Time.deltaTime);
        RigidBody.MovePosition(newPosition);
        if(Vector3.Distance(RigidBody.position, targetPosition) < 0.01){
            isMoving = false;
            RigidBody.MovePosition(targetPosition);
            }
    }

    
}
