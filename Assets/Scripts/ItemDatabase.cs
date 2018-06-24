using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ItemDatabase : MonoBehaviour 
{
	List<Item> itemDatabase;
	// Use this for initialization
	void Start () 
	{
		itemDatabase = new List<Item>();

		InitDatabase("/Configurations/ItemDatabase/");
	}

    /// <summary>
    /// Initializes the database by loading every JSON file
	/// in the given parent directory,
    /// </summary>
    /// <param name="database">The parent directory where the item JSON files are located.</param>
    void InitDatabase(string database)
	{
		DirectoryInfo dir = new DirectoryInfo(Application.dataPath + database);
		FileInfo[] files = dir.GetFiles("*.json");
		for (int i = 0; i < files.Length; i++)
		{
			Debug.Log("Opening file: " + files[i].FullName);
			string data = File.ReadAllText(Application.dataPath + database + files[i].Name);
            Item newItem = JsonUtility.FromJson<Item>(data);
            itemDatabase.Add(newItem);
            Debug.Log("Added item #" + newItem.id + " into the database. (" + newItem.name + ")");
		}

        gameObject.GetComponent<Inventory>().slots[2] = new InventoryItem(GetItemByID(1));
	}
    
    /// <summary>
    /// Gets the item by identifier.
    /// </summary>
    /// <returns>The item by identifier.</returns>
    /// <param name="id">Identifier.</param>
    public Item GetItemByID(int id)
	{
		return itemDatabase.Find(x => x.id == id);
	}
	
	// Update is called once per frame
	void Update () 
	{
	}
}

[System.Serializable]
public class Item
{
	public string name;
    public string type;
	public int id;
	public bool stackable;

    /// <summary>
    /// Create a template item with the ID of -1.
    /// </summary>
	public Item()
	{
		id = -1;
		name = "invalid_item";
		stackable = false;
        type = "invalid_type";
	}

    /// <summary>
    /// Create an item with the given parameters.
    /// </summary>
    /// <param name="id">Identifier.</param>
    /// <param name="name">Name.</param>
    /// <param name="stackable">The ability to stack this item.</param>
    public Item(int id, string name, string type, bool stackable)
	{
		this.id = id;
		this.name = name;
		this.stackable = stackable;
        this.type = type;
	}
}