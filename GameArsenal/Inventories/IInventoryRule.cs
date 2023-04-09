namespace GameArsenal.Inventories
{
    public interface IInventoryRule<TKey> where TKey : struct
    {
        int GetItemSlotMaxAmount(TKey itemId);
    }
}