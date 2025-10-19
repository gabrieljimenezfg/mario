using System;
using UnityEngine;

public class MarioController : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField]
    private GameObject tochosRotosPrefab;
    [SerializeField]
    private float speed = 0;
    [SerializeField]
    private float jumpForce = 0;
    [SerializeField]
    private bool isGrounded = true;
    private LevelManager lm;

    private readonly Vector3 eulerAngles = new(0, 90, 0);
    private Vector3 jumpVector;

    private void Start()
    {
        jumpVector = Vector3.up * jumpForce;
        rb = GetComponent<Rigidbody>();
        lm = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    private void ListenForPause()
    {
        bool isEscapePressed = Input.GetKeyDown(KeyCode.Escape);
        if (isEscapePressed)
        {
            lm.ToggleGamePause();
        }
    }

    private void ListenForMovement()
    {
        bool isRightPressed = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
        bool isLeftPressed = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);

        if (!isRightPressed && !isLeftPressed) return;

        Vector3 movement = speed * Time.deltaTime * Vector3.forward;
        int rotationMultiplier = 1;

        if (isLeftPressed)
        {
            rotationMultiplier = -1;
        }

        Vector3 rotation = eulerAngles * rotationMultiplier;

        transform.Translate(movement);
        transform.eulerAngles = rotation;
    }

    private void ListenForJump()
    {
        bool isJumpPressed = Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow);
        if (!isJumpPressed) return;

        if (!isGrounded) return;

        rb.AddForce(jumpVector);
        isGrounded = false;
        AudioManager.Instance.PlayJump();
    }

    void Update()
    {
        ListenForPause();
        ListenForMovement();
        ListenForJump();
    }

    private void PickUpCoin(GameObject coinGameObject)
    {
        GameManager.Instance.IncreaseOneCoin();
        lm.UpdateCoins();
        coinGameObject.SetActive(false);
    }

    public void HandlePlayerHit()
    {
        AudioManager.Instance.PlayHurt();
        GameManager.Instance.healthPoints--;
        if (GameManager.Instance.healthPoints < 0)
        {
            gameObject.SetActive(false);
            lm.GameOver();
        }
        else
        {
            lm.UpdateHealth();
            lm.HandlePlayerRespawn();
        }
    }

    private bool MatchesTag(GameObject collider, string tag)
    {
        return collider.CompareTag(tag);
    }

    private void HandleTochoing(Collision collision)
    {
        ContactPoint collisionContact = collision.GetContact(0);
        bool isUnderBlock = collisionContact.normal == Vector3.down;

        if (isUnderBlock)
        {
            GameObject tochosRotosInstance = Instantiate(tochosRotosPrefab, collision.transform.position, collision.transform.rotation);
            AudioManager.Instance.PlaySmash();
            Destroy(collision.gameObject);
            Destroy(tochosRotosInstance, 5f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        bool isGroundCollision = MatchesTag(collision.gameObject, "Ground") || MatchesTag(collision.gameObject, "Tocho") || MatchesTag(collision.gameObject, "Platform");
        if (isGroundCollision)
        {
            isGrounded = true;
        }

        bool isTochoCollision = MatchesTag(collision.gameObject, "Tocho");
        if (isTochoCollision)
        {
            HandleTochoing(collision);
        }

        bool isPlatformCollision = MatchesTag(collision.gameObject, "Platform");
        if (isPlatformCollision)
        {
            transform.parent = collision.transform;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        bool isLeavingPlatform = MatchesTag(collision.gameObject, "Platform");
        if (isLeavingPlatform)
        {
            transform.parent = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        bool isCoinCollision = MatchesTag(other.gameObject, "Coin");
        if (isCoinCollision)
        {
            PickUpCoin(other.gameObject);
        }

        bool isTouchingDeath = MatchesTag(other.gameObject, "Death");
        if (isTouchingDeath)
        {
            HandlePlayerHit();
        }
    }
}
