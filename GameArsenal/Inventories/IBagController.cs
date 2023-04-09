namespace GameArsenal.Inventories
{
    public interface IBagController<TKey> where TKey : struct
    {
        (int bagIndex, int slotIndex) GetNextEmptySlot();
        int GetEmptySlotCount();
        bool AddToBag(int bagIndex, int slotIndex, ItemSlot<TKey> itemSlot);
        bool RemoveFromBag(ItemSlot<TKey> itemSlot);
    }
}