using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ServerButtonClickHandler : MonoBehaviour {

	public GameObject networkManagerGO;
	NetworkManager networkManager;
	HostData[] hostList;
	bool server_running;


	void Start () {
		server_running = false;
		networkManager = networkManagerGO.GetComponent<NetworkManager>();
		ServerButtonClickDelegate.onClickHandler += OnClickHandler;
	}
	
	void OnClickHandler(ButtonType btType, Button button){
		if(btType == ButtonType.StartStopServer){
			if(!server_running){
				networkManager.StartServer();
				GetComponentInChildren<Text>().text = "Stop Server";
			}else{
				networkManager.StopServer();
				GetComponentInChildren<Text>().text = "Start Server";
			}
			server_running = !server_running;
		}else if(btType == ButtonType.RefreshHosts){
			networkManager.RefreshHostList();
		}else if(btType == ButtonType.JoinServer){
			this.hostList = networkManager.getHostList();
			if(hostList != null){
				int index = numberFromButtonText(button.GetComponentInChildren<Text>());
				networkManager.JoinServer(hostList[index]);
			}
		}
	}
	
	int numberFromButtonText(Text textComponent){
		string text = textComponent.text;
		int number_result = -1;
		int.TryParse(text.Substring(text.Length-1), out number_result);
		return number_result;
	}
	
	
}
