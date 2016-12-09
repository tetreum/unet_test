using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{
    public GameObject camera;

    void Start ()
    {
        if (isLocalPlayer) {
            camera.SetActive(true);
        }
    }

    void Update()
    {
        if (!isLocalPlayer) {
            return;
        }

        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);
    }
}
