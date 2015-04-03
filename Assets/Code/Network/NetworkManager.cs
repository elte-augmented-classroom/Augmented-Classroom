using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviour {

	public GameObject serversPanelGO;
	public GameObject buttonServerHostPrefab;
	const string typeName = "UniqueGameName";
	const string gameName = "RoomName";
	
	public static int serversAvailable;
	HostData[] hostList;
	bool server_registered = false;
	
	public void StartServer()
	{
		Network.InitializeServer(4, 25000, !Network.HavePublicAddress());
		MasterServer.RegisterHost(typeName, gameName);
	}
	
	public void StopServer(){
		foreach (NetworkPlayer networkPlayer in Network.connections) {
			Network.CloseConnection(networkPlayer, true);
		}
	}

	void OnServerInitialized(){
		Debug.Log("Server initialized");
	}

	public void JoinServer(HostData hostData){
		Network.Connect(hostData);
		gameObject.SetActive(false);
	}
	
	void OnConnectedToServer(){
		Debug.Log("Server Joined");
	}

	public void RefreshHostList(){
		MasterServer.RequestHostList(typeName);
		StopCoroutine("FillServersPanel");
		StartCoroutine(FillServersPanel());
	}
	
	IEnumerator FillServersPanel(){
		while(hostList == null || hostList.Length == 0){
			hostList = MasterServer.PollHostList();
			yield return new WaitForEndOfFrame();
		}
		for(int i = 0; i < hostList.Length; i++) {
			GameObject buttonServerHost = GameObject.Instantiate(buttonServerHostPrefab) as GameObject;
			string btHostName = hostList[i].gameName + " " + i;
			buttonServerHost.GetComponentInChildren<Text>().text = btHostName;
			buttonServerHost.transform.SetParent(serversPanelGO.transform, false);
			NetworkManager.serversAvailable += 1;
		}
	}
	
	public HostData[] getHostList(){
		return this.hostList;
	}
	
	void OnMasterServerEvent(MasterServerEvent msEvent) {
		if (msEvent == MasterServerEvent.RegistrationSucceeded){
			Debug.Log("Server registered");
		}else if(msEvent == MasterServerEvent.HostListReceived){
			hostList = MasterServer.PollHostList();
		}
	}
	
	
}
