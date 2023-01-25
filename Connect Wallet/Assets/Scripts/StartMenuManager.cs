using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Thirdweb;
using System.Threading.Tasks;

public class StartMenuManager : MonoBehaviour
{
    private ThirdwebSDK sdk;
    public GameObject ConnectedState;
    public GameObject DisconnectedState;

    // Start is called before the first frame update
    void Start()
    {
        sdk = new ThirdwebSDK("mumbai");
    }

    void update()
    {
        ConnectedState.SetActive(false);
        DisconnectedState.SetActive(true);
    }

    public async void ConnectWallet()
    {
        string address =
            await sdk
            .wallet
            .Connect(new WalletConnection()
            {
                provider = WalletProvider.MetaMask,
                chainId = 80001 // Switch the wallet Goerli on connection
            });

        DisconnectedState.SetActive(false);
        ConnectedState.SetActive(true);

        ConnectedState
            .transform
            .Find("Address")
            .GetComponent<TMPro.TextMeshProUGUI>()
            .text = address;
    }
}
