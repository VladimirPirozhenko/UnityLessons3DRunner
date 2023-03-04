using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;    
    [SerializeField] private float laneDistance;
    [SerializeField] private float laneCount;    
    [SerializeField] private float laneMovementSpeed;
    [SerializeField] private float speed;
    [SerializeField] private float gravity;
    [SerializeField] private float jumpForce;
    private Vector3 movement;
    [SerializeField] private int desiredLane;
    float verticalMovement = -1;
    private Vector3 resultMovementDelta;

    [SerializeField] private HudView hud;
    private int score;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        desiredLane = (int)laneCount / 2;
        score = 0;  
    }
    private void OnEnable()
    {
        Coin.OnCoinCollected += AddScore;
    }

    private void OnDisable()
    {
        Coin.OnCoinCollected -= AddScore;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ICollectable collectable)) 
        {
            collectable.Collect();
        }
    }

    public void AddScore(int value)
    {
        score+=value;
        hud.UpdateScore(score);
        //ViewManager.instance.getView<HudView>();
    }

    public void Jump()
    {    
        if (characterController.isGrounded)
        {
            verticalMovement += gravity * Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.W))
            {
                verticalMovement = jumpForce;
            }
            
        }
        else
        {
           verticalMovement += gravity * Time.deltaTime;
        }
        resultMovementDelta.y = verticalMovement * Time.deltaTime;   
    }
    public void Move()
    {
        resultMovementDelta = Vector3.zero;
        MoveForward();
        ChangeLane(); 
        Jump();
        characterController.Move(resultMovementDelta);

    }
    public void MoveForward()
    {
        movement.z = speed;
       
        resultMovementDelta += movement.z * transform.forward * Time.deltaTime;

    }
    public void ChangeLane()
    {
        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if (Input.GetKeyDown(KeyCode.D))
        {
            desiredLane++;
            if (desiredLane >= laneCount)
            {
                desiredLane--;
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            desiredLane--;
            if (desiredLane < 0)
            {
                desiredLane++;
            }
        }

        if (desiredLane == 2)
            targetPosition += laneDistance * Vector3.right;
        if (desiredLane == 0)
            targetPosition -= laneDistance * Vector3.right;
        if (transform.position != targetPosition)
        {
            Vector3 diff = targetPosition - transform.position;
            Vector3 moveDir = diff.normalized * laneMovementSpeed * Time.deltaTime;
            if (moveDir.sqrMagnitude < diff.sqrMagnitude)
                resultMovementDelta += moveDir;
            else
                resultMovementDelta += diff;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

}
