using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private float movementX;
    private float movementY;

    private int count;

    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    public int jumpCount = 0;

    [SerializeField] float jumpForce = 5f;
    private PlayerInput playerInput;

    void Awake() {
        playerInput = GetComponent<PlayerInput>();
        playerInput.actions["Player/Jump"].performed += ctx => Jump();
    }
    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        rb = transform.GetComponent<Rigidbody>();
        SetCountText();
        winTextObject.SetActive(false);
        
    }

    void Update() {
        
    }

    void OnMove(InputValue movementValue) {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    private void FixedUpdate() {
        Vector3 movement = new Vector3(movementX, 0f, movementY);
        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("PickUp")) {
            other.gameObject.SetActive(false);
            count += 1;
            SetCountText();
        }
    }

    void SetCountText() {
        countText.text = "Count: " + count.ToString();
        if (count >= 6) {
            winTextObject.SetActive(true);
        }
    }

    void Jump() {
        if (jumpCount < 2) {
            rb.AddForce(Vector3.up * jumpForce);
            jumpCount++;
        }
    }

    public void FloorTouch() {
        jumpCount = 0;
    }

    
}
