using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI livesText;
    public GameObject winTextObject;

    public GameObject loseTextObject;

    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;

    private int lives;

    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody>();
        count = 0;
        lives = 3;

        SetCountText();
        winTextObject.SetActive(false);
        SetLivesText();
        loseTextObject.SetActive(false);

    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 14)
        {
            winTextObject.SetActive(true);
        }

    }

    void SetLivesText()
    {
        livesText.text = "Lives: " + lives;
        if (lives < 1)
        {
            loseTextObject.SetActive(true);
        }
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;

            SetCountText();
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);

            lives = lives - 1;
            SetCountText();
            SetLivesText();
        }
        if (count == 6)
        {
            transform.position = new Vector3(-5.0f, 1.0f, -50.0f);
        }
    }

}