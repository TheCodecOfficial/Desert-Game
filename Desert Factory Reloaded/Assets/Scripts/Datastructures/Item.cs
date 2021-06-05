using UnityEngine;

[CreateAssetMenu(fileName = "new Item", menuName = "Item")]
public class Item : ScriptableObject {
    public Sprite sprite;

    public override int GetHashCode()
    {
        return name.GetHashCode();
    }
}