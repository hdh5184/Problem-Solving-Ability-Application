using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public GameObject cameraPosObj;

    Camera camera;
    Vector3 cameraPos;

    float rotateY;
    float rotateYDes;
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        camera.transform.eulerAngles = new Vector3(45, 45, 0);
        rotateYDes = transform.eulerAngles.y;
        cameraPosObj.transform.position = camera.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A)) Move(Vector3.left);
        if (Input.GetKey(KeyCode.D)) Move(Vector3.right);
        if (Input.GetKey(KeyCode.W)) Move(Vector3.forward);
        if (Input.GetKey(KeyCode.S)) Move(Vector3.back);

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

    void Move(Vector3 moveVec)
    {
        transform.Translate(moveVec * Time.deltaTime * 2);
    }
}
