using GameArsenal.Inventories;

namespace GameArsenalTest.Inventories
{
    public class TestInventoryRule : IInventoryRule<TestItemId>
    {
        private readonly int slotMaxAmount;

        public TestInventoryRule(int slotMaxAmount)
        {
            this.slotMaxAmount = slotMaxAmount;
        }

        public int GetItemSlotMaxAmount(TestItemId itemId)
        {
            return this.slotMaxAmount;
        }
    }
}