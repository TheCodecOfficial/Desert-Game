using UnityEngine;

[CreateAssetMenu(fileName = "new Item", menuName = "Item")]
public class Item : ScriptableObject {
    public new string name;
    public Sprite sprite;
}