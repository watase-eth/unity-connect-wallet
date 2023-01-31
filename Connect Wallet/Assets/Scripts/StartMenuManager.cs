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
        sdk = new ThirdwebSDK("goerli");
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
                chainId = 5 // Switch the wallet Goerli on connection
            });

        DisconnectedState.SetActive(false);
        ConnectedState.SetActive(true);

        ConnectedState
            .transform
            .Find("Address")
            .GetComponent<TMPro.TextMeshProUGUI>()
            .text = address;

        string balance = await CheckBalance(address);

        float balanceFloat = float.Parse(balance);

        string balanceText =
        balanceFloat > 0
        ? "You own this NFT!"
        : "You don't own this NFT yet!";

        ConnectedState
            .transform
            .Find("NFT")
            .GetComponent<TMPro.TextMeshProUGUI>()
            .text = balanceText;
    }

    public async Task<string> CheckBalance(string address)
    {
        Contract contract = sdk.GetContract("0xd1b4Edf074a3c29FdA0F6fC5d5aF9613BAE39A5C");

        string balance = await contract.Read<string>("balanceOf", address);
        return balance;
    }
}
