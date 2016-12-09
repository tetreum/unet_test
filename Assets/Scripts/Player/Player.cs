using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{
    public GameObject cam;
    public static Player Instance;

    void Start ()
    {
        if (isLocalPlayer) {
            Instance = this;
            cam.SetActive(true);
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

        if (Input.GetMouseButtonDown(0))
        {
            // test RPCs
            CmdOnShoot();

            // test prefab spawning
            Vector3 pos = Input.mousePosition;
            pos.z = 10.0f;
            pos = cam.GetComponent<Camera>().ScreenToWorldPoint(pos);
            pos.y = transform.position.y;

            Laravel.NetworkManager.spawn("Furnis/Statue", pos, Quaternion.identity);
        }
    }

    [Command]
    void CmdOnShoot() {
        RpcShoot();
    }

    [ClientRpc]
    private void RpcShoot() {
        Debug.Log("piñaoo, piñaoo");
    }
}
