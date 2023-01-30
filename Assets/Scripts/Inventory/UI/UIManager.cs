using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Status Bar")]
    //Tool equip slot on the status bar
    public Image toolEquipSlot; 

    [Header("Inventory System")]

    //The inventory panel
    public GameObject inventoryPanel;
    public GameObject instructionsPanel;
    public GameObject EndPanel; 

    //The tool slot UIs
    public InventorySlot[] toolSlots;

    public HandInventorySlot toolHandSlot;

    //The item slot UIs
    public InventorySlot[] itemSlots;

    public HandInventorySlot itemHandSlot;

    //Item info box
    public TMP_Text itemNameText;
    public TMP_Text itemDescriptionText; 

    // info panel

    public GameObject infoPanel;

    // the other objects on the screen

    public GameObject bag;
    public GameObject book;
    public GameObject statusBar;


    private void Awake()
    {
        //If there is more than one instance, destroy the extra
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            //Set the static instance to this instance
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

        RenderInventory();
        AssingnSlotIndexes();
        
    }

    public void AssingnSlotIndexes()
    {
        for (int i = 0; i <toolSlots.Length; i++)
        {
            toolSlots[i].AssingIndex(i);
            itemSlots[i].AssingIndex(i);
        }
    }

    //Render the inventory screen to reflect the Player's Inventory. 
    public void RenderInventory()
    {
        //Get the inventory tool slots from Inventory Manager
        ItemData[] inventoryToolSlots = InventoryManager.Instance.tools;

        //Get the inventory item slots from Inventory Manager
        ItemData[] inventoryItemSlots = InventoryManager.Instance.items;

        //Render the Tool section
        RenderInventoryPanel(inventoryToolSlots, toolSlots);

        //Render the Item section
        RenderInventoryPanel(inventoryItemSlots, itemSlots);

        //Render the hand section
        toolHandSlot.Display(InventoryManager.Instance.equippedTool);
        itemHandSlot.Display(InventoryManager.Instance.equippedItem);


        //Get Tool Equip from InventoryManager
        ItemData equippedTool = InventoryManager.Instance.equippedTool;

        //Check if there is an item to display
        if (equippedTool != null)
        {
            //Switch the thumbnail over
            toolEquipSlot.sprite = equippedTool.thumbnail;

            toolEquipSlot.gameObject.SetActive(true);

            return;
        }

        toolEquipSlot.gameObject.SetActive(false);
    }

    

    //Iterate through a slot in a section and display them in the UI
    void RenderInventoryPanel(ItemData[] slots, InventorySlot[] uiSlots)
    {
        for (int i = 0; i < uiSlots.Length; i++)
        {
            //Display them accordingly
            uiSlots[i].Display(slots[i]);
        }
    }

    public void ToggleInventoryPanel()
    {
        //If the panel is hidden, show it and vice versa
        if(!instructionsPanel.activeSelf)
        {
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
            RenderInventory();
        }


    }

    public void ToggleInstructionsPanel()
    {
        //If the panel is hidden, show it and vice versa
        if(!inventoryPanel.activeSelf)
        {
            instructionsPanel.SetActive(!instructionsPanel.activeSelf);
        }


    }

    public void ToggleInfoPanel()
    {
        if(!infoPanel.activeSelf)
        {
            infoPanel.SetActive(!infoPanel.activeSelf);
        }
        if(bag.activeSelf)
        {
            bag.SetActive(!bag.activeSelf);
        }
        if(statusBar.activeSelf)
        {
            statusBar.SetActive(!statusBar.activeSelf);
        }
        if(book.activeSelf)
        {
            book.SetActive(!book.activeSelf);
        }     
    }

        public void ToggleOffInfoPanel()
    {
        if(infoPanel.activeSelf)
        {
            infoPanel.SetActive(!infoPanel.activeSelf);
        }
        if(!bag.activeSelf)
        {
            bag.SetActive(!bag.activeSelf);
        }
        if(!statusBar.activeSelf)
        {
            statusBar.SetActive(!statusBar.activeSelf);
        }
        if(!book.activeSelf)
        {
            book.SetActive(!book.activeSelf);
        }     
    }

    public void ToggleEndPanel()
    {
        //If the panel is hidden, show it and vice versa

        EndPanel.SetActive(!EndPanel.activeSelf);
    }

    //Display Item info on the Item infobox
    public void DisplayItemInfo(ItemData data)
    {
        //If data is null, reset
        if(data == null)
        {
            itemNameText.text = "";
            itemDescriptionText.text = "";

            return;
        }
        itemNameText.text = data.name;
        itemDescriptionText.text = data.description; 
    }
}
