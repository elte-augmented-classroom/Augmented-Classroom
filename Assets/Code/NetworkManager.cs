using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	const string typeName = "UniqueGameName";
	const string gameName = "RoomName";
	
	HostData[] hostList;
	
	void StartServer()
	{
		Network.InitializeServer(4, 25000, !Network.HavePublicAddress());
		MasterServer.RegisterHost(typeName, gameName);
		MasterServer.ipAddress = "127.0.0.1";
	}

	void OnServerInitialized(){
		Debug.Log("Server initialized");
	}

		void OnConnectedToServer(){
		Debug.Log("Server Joined");
	}
	
	public void JoinServer(HostData hostData){
		Network.Connect(hostData);
	}

	public void RefreshHostList(){
		MasterServer.RequestHostList(typeName);
		StopAllCoroutines();
		StartCoroutine(FillPanelServers());
	}
	
	void OnMasterServerEvent(MasterServerEvent msEvent) {
		if (msEvent == MasterServerEvent.RegistrationSucceeded){
			Debug.Log("Server registered");
		}else if(msEvent == MasterServerEvent.HostListReceived){
			hostList = MasterServer.PollHostList();
		}
	}
	
	
}
