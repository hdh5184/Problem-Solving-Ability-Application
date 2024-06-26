using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;

    public GameObject cameraPosObj;

    public Vector3 startPos;

    Camera camera;
    Vector3 cameraPos;

    float rotateY;
    float rotateYDes;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        camera = Camera.main;
        camera.transform.eulerAngles = new Vector3(45, 45, 0);
        rotateYDes = transform.eulerAngles.y;
        camera.transform.position = cameraPosObj.transform.position;
        transform.position = startPos;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.O))
        {
            rotateYDes -= 90;
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            rotateYDes += 90;
        }
        camera.transform.position = cameraPosObj.transform.position;

        rotateY = Mathf.Lerp(rotateY, rotateYDes, 0.1f);

        transform.rotation = Quaternion.Euler(new Vector3(0, rotateY, 0));
        camera.transform.rotation = Quaternion.Euler(
            new Vector3(camera.transform.eulerAngles.x, camera.transform.eulerAngles.x + rotateY, 0));
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A)) Move(Vector3.left);
        if (Input.GetKey(KeyCode.D)) Move(Vector3.right);
        if (Input.GetKey(KeyCode.W)) Move(Vector3.forward);
        if (Input.GetKey(KeyCode.S)) Move(Vector3.back);
    }

    void Move(Vector3 moveVec)
    {
        rb.MovePosition(
            rb.position + transform.TransformDirection(moveVec) * 4f * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key"))
        {
            GameManager.isDoorLock = false;
            other.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Goal": GameManager.gm.GameClear(); break;
            case "Enemy": GameManager.gm.GameOver(); break;
        }

    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            rb.velocity = Vector3.zero;
        }
    }


}
