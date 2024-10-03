using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
public class PlayerController : MonoBehaviour
{
    public Vector2 moveValue;
    public float speed;
    private int count;
    private int numPickups = 5;
    private Vector3 lastposition;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI winText;
    public TextMeshProUGUI playerPositionText;
    public TextMeshProUGUI playerVelocityText;
    void Start()
    {
        count = 0;
        winText.text = "";
        SetCountText();
        lastposition = transform.position;
    }
    void OnMove(InputValue inputValue)
    {
        moveValue = inputValue.Get<Vector2>();
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(moveValue.x, 0f, moveValue.y);
        GetComponent<Rigidbody>().AddForce(movement * speed * Time.fixedDeltaTime);

        //Player Position
        playerPositionText.text = "Player Position: " + transform.position.ToString("0.00");
        //player Velocity
        playerVelocityText.text = "Player Velocity: " + (((transform.position - lastposition) / Time.deltaTime)).magnitude.ToString("0.00");
        lastposition = transform.position;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PickUp")
        {
            other.gameObject.SetActive(false);
            count += 1;
            SetCountText();
        }

    }

    private void SetCountText()
    {
        scoreText.text = "Score: " + count.ToString();
        if (count >= numPickups)
        {
            winText.text = "You Win!";
        }
    }

}