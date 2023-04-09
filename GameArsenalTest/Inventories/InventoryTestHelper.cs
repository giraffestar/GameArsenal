using GameArsenal.Inventories;

namespace GameArsenalTest.Inventories
{
    public static class InventoryTestHelper
    {
        public static readonly TestItemId ItemA = new TestItemId(1);
        public static readonly TestItemId ItemB = new TestItemId(2);
        public static readonly TestItemId ItemC = new TestItemId(3);
        public static readonly TestItemId ItemD = new TestItemId(4);
        public static readonly TestItemId ItemE = new TestItemId(5);

        public static readonly DynamicItemId DynamicItemA = new DynamicItemId(1);
        public static readonly DynamicItemId DynamicItemB = new DynamicItemId(2);
        public static readonly DynamicItemId DynamicItemC = new DynamicItemId(3);


        public static Inventory<TestItemId> CreateInventory(int maxSlotAmount)
        {
            var slotMaxAmount = maxSlotAmount;
            var rule = new TestInventoryRule(slotMaxAmount);

            return new Inventory<TestItemId>(rule);
        }
    }
}