using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUIManager : MonoBehaviour {

	[SerializeField] Player player;
	[SerializeField] Transform inventoryPanel;
	[SerializeField] GameObject inventorySlotPrefab;
	[SerializeField] List<MonoBehaviour> toDisable;
	[SerializeField] Sprite defaultImage;

	#region DescriptionPane
		[SerializeField] List<Button> buttons;
		[SerializeField] Image icon;
		[SerializeField] TextMeshProUGUI name;
		[SerializeField] TextMeshProUGUI description;
	#endregion

	Image[,] inventorySlots;
	bool shown = false;
	Item[,] activeInventory = null;

	public static InventoryUIManager instance;
	public int inventoryWidth;
	public int inventoryHeight;

	public Item[,] MakeInventory()
	{
		return new Item[inventoryWidth, inventoryHeight];
	}

	void Awake()
	{
		instance = this;
	}

	// Use this for initialization
	void Start () {
		icon.sprite = defaultImage;
		inventorySlots = new Image[inventoryWidth, inventoryHeight];
		RectTransform panelTransform = inventoryPanel.GetComponent<RectTransform>();
		float HEIGHT = Screen.height + panelTransform.sizeDelta.y - 150f;
		float WIDTH = Screen.width  + panelTransform.sizeDelta.x - 150f;
		float xstep = WIDTH / (inventoryWidth - 1) ;
		float ystep = HEIGHT / (inventoryHeight - 1);
		for (int i = 0; i < inventoryWidth; i++){
			for (int j = 0; j < inventoryHeight; j++){
				GameObject slot = Instantiate(inventorySlotPrefab, inventoryPanel);
				RectTransform tform = slot.GetComponent<RectTransform>();
				Button b = slot.GetComponent<Button>();
				int localI = i;
				int localJ = j;
				b.onClick.AddListener(() => Display(localI, localJ));
				float posX = i * xstep - WIDTH / 2;
				float posY = j * ystep - HEIGHT / 2;
				tform.localPosition = new Vector3(posX, posY,0);
				Image gfx = slot.transform.GetChild(0).GetComponent<Image>();
				inventorySlots[i, j] = gfx;
			}
		}
	}

	void Display(int i, int j){
		Item item = activeInventory[i, j];
		if (item != null){
			icon.sprite = item.graphic;
			description.text = item.itemDescription;
			name.text = item.itemName;
		}else{
			icon.sprite = defaultImage;
			description.text = "";
			name.text = "";
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (activeInventory != null){
			for (int i = 0; i < inventoryWidth; i++){
				for (int j = 0; j < inventoryHeight; j++){
					if (player.inventory[i, j] != null){
						inventorySlots[i, j].sprite = activeInventory[i, j].graphic;
					} else{
						inventorySlots[i, j].sprite = defaultImage;
					}
				}
			}
		}
	}

	public void ShowInventory(Item[,] inventory){
		foreach (MonoBehaviour component in toDisable) component.enabled = false;
		inventoryPanel.gameObject.SetActive(true);
		shown = true;
		Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
		activeInventory = inventory;
	}

	public void HideInventory(){
		foreach (MonoBehaviour component in toDisable) component.enabled = true;
		inventoryPanel.gameObject.SetActive(false);
		shown = false;
		Cursor.lockState = CursorLockMode.Locked;
    	Cursor.visible = false;
		activeInventory = null;
		icon.sprite = defaultImage;
		description.text = "";
		name.text = "";
	}
}
