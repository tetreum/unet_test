using UnityEngine;
using System.Collections;
using UnetManager = UnityEngine.Networking.NetworkManager;
using UnityEngine.Rendering;
using UnityEngine.Networking;

namespace Laravel
{
    public class NetworkManager : MonoBehaviour
    {
        private UnetManager networkManager;

        void Start()
        {
            networkManager = UnetManager.singleton;

            if (networkManager.matchMaker == null) {
                networkManager.StartMatchMaker();
            }
        }

        void Awake()
        {
            // start dedicated server if required
            if (isServer()) {
                UnetManager.singleton.StartServer();
            }
        }

        // Checks if this build is headless (dedicated server)
        // https://noobtuts.com/unity/unet-server-hosting
        public static bool isServer() {
            return SystemInfo.graphicsDeviceType == GraphicsDeviceType.Null;
        }

        public static void spawn (string name, Vector3 position, Quaternion rotation)
        {
            NetworkServer.Spawn((GameObject)Instantiate((GameObject)Resources.Load("Prefabs/" + name), position, rotation));
        }
    }
}