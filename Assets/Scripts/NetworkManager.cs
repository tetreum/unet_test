using UnityEngine;
using System.Collections;
using UnetManager = UnityEngine.Networking.NetworkManager;
using UnityEngine.Rendering;

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
        public static bool isServer() {
            return SystemInfo.graphicsDeviceType == GraphicsDeviceType.Null;
        }
    }
}