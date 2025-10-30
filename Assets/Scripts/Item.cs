using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "ScriptableObjects/Item")]
public class Item : ScriptableObject
{
    [Header("Only Gameplay")]
    public TileBase tile;
    public ItemType type;
    public Actiontype actionType;
    public Vector2Int range = new Vector2Int(5, 4);

    [Header("Only UI")] 
    public bool stackable = true;
    
    [Header("Both")]
    public Sprite image;

    public enum ItemType
    {
        Scrap,
        Tool
    }

    public enum Actiontype
    {
        Attack,
        Repair
    }
}
