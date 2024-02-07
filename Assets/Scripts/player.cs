using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEngine.InputSystem;

public class player : MonoBehaviour
{

    //References
    [Header("References")]
    public Transform trans;
    public Transform modelTrans;
    public CharacterController characterController;
    public float rotationspeed;
    public bool isMoving;
    public Animator animator;
    public ParticleSystem dust;
    public GameObject Jump;
    
    

    //Grid Movement
    private Vector3 origPos, targetPos;
    private float timeToMove = 0.2f;

    //Movement
    [Header("Movement")]
    [Tooltip("Units moved per second at maximum speed.")]
    public float movespeed = 25;
    [Tooltip("Time, in secons, to reach maximum speed.")]
    public float timeToMaxSpeed = .26f;
    private float VelocityGainPerSecond
    {
        get
        {
            return movespeed / timeToMaxSpeed;
        }
    }

    [Tooltip("Time, in seconds, to go from maximum speed to stationary.")]
    public float timeToLoseMaxSpeed = .2f;

    private float VelocityLossPerSecond { get { return movespeed / timeToLoseMaxSpeed; } }

    [Tooltip("Multiplier for momentum when attempting to move in a direction oppposite the current traveling direction (e.g. trying to move right when already moving left).")]
    public float reverseMomentumMultiplier = 2.2f;

    private Vector3 movementVelocity = Vector3.zero;


    private void Movement()
    {
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))   
        {
            //CreateDust();
            if (movementVelocity.z >= 0)
                {
                    //when moving forward
                    //Increase velocity by VelocityGainPerSecond, but don't go higher than movespeed'.
                    movementVelocity.z = Mathf.Min(movespeed, movementVelocity.z + VelocityGainPerSecond * Time.deltaTime);
                }
                else
                {
                    //if we're moving back
                    //increase z velocity by VelocityGainPerSecond, using the reverseMomentumMultiplier, but don't raise higher than 0
                    movementVelocity.z = Mathf.Min(0, movementVelocity.z + VelocityGainPerSecond * reverseMomentumMultiplier * Time.deltaTime);
                }
            }

            else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            //CreateDust();
            if (movementVelocity.z > 0)
                {
                    //if already moving forward
                    movementVelocity.z = Mathf.Max(0, movementVelocity.z - VelocityGainPerSecond * reverseMomentumMultiplier * Time.deltaTime);
                }
                else
                {
                    //if we're already moving forward
                    movementVelocity.z = Mathf.Max(-movespeed, movementVelocity.z - VelocityGainPerSecond * Time.deltaTime);
                }
            }

            else
            {
                //if neither forward or back are being held
                //we must bring the z velocity back to 0 over time.
                if (movementVelocity.z > 0)
                {
                    //if we're moving up,
                    //decrease z velocity by VelocityLossPerSecond, but don't go any lower than 0
                    movementVelocity.z = Mathf.Max(0, movementVelocity.z - VelocityLossPerSecond * Time.deltaTime);
                }
                else
                {
                    //if we're moving down,
                    //increase Z velocity (back towards 0) by VelocityLossPerSecond, but don't go any higher than 0
                    movementVelocity.z = Mathf.Min(0, movementVelocity.z + VelocityLossPerSecond * Time.deltaTime);
                }
            }

            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {

            //CreateDust();
            if (movementVelocity.x >= 0)
                {
                    //If we're already moving right
                    //Increase X velocity by VelocityGainPerSecond, but don't go higher than 'movespeed':
                    movementVelocity.x = Mathf.Min(movespeed, movementVelocity.x + VelocityGainPerSecond * Time.deltaTime);
                }
                else
                {
                    //If we're moving left
                    //Increase x velocity by VelocityGainPerSecond, using the reverseMomentumMultiplier, but don't raise higher than 0:
                    movementVelocity.x = Mathf.Min(0, movementVelocity.x + VelocityGainPerSecond * reverseMomentumMultiplier * Time.deltaTime);
                }
            }

            else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
            //CreateDust();
            if (movementVelocity.x > 0)
                {
                    //if we're already moving to the right
                    movementVelocity.x = Mathf.Max(0, movementVelocity.x - VelocityGainPerSecond * reverseMomentumMultiplier * Time.deltaTime);
                }
                else
                {
                    //if we're movnging left or not moving at all
                    movementVelocity.x = Mathf.Max(-movespeed, movementVelocity.x - VelocityGainPerSecond * Time.deltaTime);
                }
            }

            else
            {
                //if neitherright nor left are being held
                if (movementVelocity.x > 0)
                {
                    //if we're moving neitherright
                    //decrease x velocity by VelocityLossPerSecond, but don't go any lower than 0
                    movementVelocity.x = Mathf.Max(0, movementVelocity.x - VelocityLossPerSecond * Time.deltaTime);
                }
                else
                {
                    //if we're moving left
                    //increase x velocity (back towards 0) by VelocityLossPerSecond, but don't go any higher than 0
                    movementVelocity.x = Mathf.Min(0, movementVelocity.x + VelocityLossPerSecond * Time.deltaTime);
                }
            }
            //If the player is moving in either direction(left/righ/up/down)
            if (movementVelocity.x != 0 || movementVelocity.z != 0)
            {
                //applying the movement velocity
                characterController.Move(movementVelocity * Time.deltaTime);
                //keeping the model holder rotated towards the last movement direction
                modelTrans.rotation = Quaternion.Slerp(modelTrans.rotation, Quaternion.LookRotation(movementVelocity), rotationspeed);
                //modelTrans.rotation = Quaternion.LookRotation(movementVelocity);
                //for instant LookRotation
            }

    }

    public void Moving()
    {
        if (movementVelocity.x > 0.1 || movementVelocity.x < -0.1 || movementVelocity.z > 0.1 || movementVelocity.z < -0.1)
        {
            animator.SetBool("isMoving", true);
            isMoving = true;
            Jump.SetActive(true);
        }
        else
        {
            animator.SetBool("isMoving", false);
            isMoving = false;
            Jump.SetActive(false);
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
       Movement();
       Moving();
       CreateDust();
        
    }

    void CreateDust()
    {
        if (isMoving)
        {
            dust.Play();
        }
        else
        {
            dust.Stop();
        }
        
    }

    private IEnumerator MovePlayer(Vector3 direction)
    {
        isMoving = true;

        float elapsedTime = 0;
        origPos = transform.position;
        targetPos = origPos + direction;

        while(elapsedTime < timeToMove)
        {
            transform.position = Vector3.Lerp(origPos, targetPos, (elapsedTime / timeToMove));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;

        isMoving = false;
    }
}