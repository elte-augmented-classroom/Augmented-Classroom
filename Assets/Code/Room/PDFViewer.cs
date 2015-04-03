using UnityEngine;
using System.Collections;

public class PDFViewer : MonoBehaviour {

	public Texture2D[] Pages;
	public int currentPage = 0;
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.R)){
			NextImage();
		}else if(Input.GetKeyDown(KeyCode.L)){
			PreviousImage();
		}
	}
	
	void updatePage(){
		if(currentPage >= 0 && renderer.material.mainTexture != Pages[currentPage])
			renderer.material.mainTexture = Pages[currentPage];
	}
	
	public void NextImage()
	{
		currentPage = (currentPage + 1) % Pages.Length;
		networkView.RPC ("UpdateViewRPC", RPCMode.OthersBuffered, currentPage);
	}
	
	public void PreviousImage()
	{
		currentPage	= (currentPage - 1) % Pages.Length;
		currentPage	= currentPage < 0 ? Pages.Length-1 : currentPage;
		networkView.RPC ("UpdateViewRPC", RPCMode.OthersBuffered, currentPage);
	}
	
	[RPC]
	void UpdateViewRPC(int currentImage){
		currentPage = currentImage;
	}
	
}
