
[System.Serializable]
public struct ItemStack {
    public Item item;
    public int amount;

    public ItemStack(Item _item, int _amount){
        item = _item;
        amount = _amount;
    }
}