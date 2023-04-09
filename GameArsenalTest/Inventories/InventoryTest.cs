using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameArsenalTest.Inventories
{
    [TestClass]
    public class InventoryTest
    {
        [TestMethod]
        public void AddLessThanAllowed()
        {
            var inventory = InventoryTestHelper.CreateInventory(99);
            inventory.AddItemBag(10);

            Assert.IsTrue(inventory.AddStaticItem(InventoryTestHelper.ItemA, 50));
            Assert.AreEqual(50, inventory.GetStaticItemAmount(InventoryTestHelper.ItemA));
            Assert.IsTrue(inventory.AddStaticItem(InventoryTestHelper.ItemB, 99));
            Assert.AreEqual(99, inventory.GetStaticItemAmount(InventoryTestHelper.ItemB));
            Assert.IsTrue(inventory.AddDynamicItem(InventoryTestHelper.ItemC, InventoryTestHelper.DynamicItemA));
            Assert.IsTrue(inventory.AddDynamicItem(InventoryTestHelper.ItemC, InventoryTestHelper.DynamicItemB));
            Assert.IsTrue(inventory.AddStaticItem(InventoryTestHelper.ItemD, 150));
            Assert.AreEqual(150, inventory.GetStaticItemAmount(InventoryTestHelper.ItemD));
        }

        [TestMethod]
        public void AddMoreThanAllowed()
        {
            var inventory = InventoryTestHelper.CreateInventory(99);
            inventory.AddItemBag(1);

            Assert.IsFalse(inventory.AddStaticItem(InventoryTestHelper.ItemA, 100));
            Assert.AreEqual(0, inventory.GetStaticItemAmount(InventoryTestHelper.ItemA));

            Assert.IsTrue(inventory.AddDynamicItem(InventoryTestHelper.ItemB, InventoryTestHelper.DynamicItemA));
            Assert.IsFalse(inventory.AddDynamicItem(InventoryTestHelper.ItemB, InventoryTestHelper.DynamicItemB));
        }

        [TestMethod]
        public void RemoveLessThanAllowed()
        {
            var inventory = InventoryTestHelper.CreateInventory(99);
            inventory.AddItemBag(10);

            inventory.AddStaticItem(InventoryTestHelper.ItemA, 50);
            Assert.IsTrue(inventory.RemoveStaticItem(InventoryTestHelper.ItemA, 1));
            Assert.AreEqual(49, inventory.GetStaticItemAmount(InventoryTestHelper.ItemA));
            Assert.IsTrue(inventory.RemoveStaticItem(InventoryTestHelper.ItemA, 49));
            Assert.AreEqual(0, inventory.GetStaticItemAmount(InventoryTestHelper.ItemA));

            inventory.AddDynamicItem(InventoryTestHelper.ItemB, InventoryTestHelper.DynamicItemA);
            Assert.IsTrue(inventory.RemoveDynamicItem(InventoryTestHelper.ItemB, InventoryTestHelper.DynamicItemA));
        }

        [TestMethod]
        public void RemoveMoreThanAllowed()
        {
            var inventory = InventoryTestHelper.CreateInventory(99);
            inventory.AddItemBag(10);

            inventory.AddStaticItem(InventoryTestHelper.ItemA, 50);
            Assert.IsFalse(inventory.RemoveStaticItem(InventoryTestHelper.ItemA, 51));
            Assert.AreEqual(50, inventory.GetStaticItemAmount(InventoryTestHelper.ItemA));

            Assert.IsFalse(inventory.RemoveDynamicItem(InventoryTestHelper.ItemB, InventoryTestHelper.DynamicItemA));
        }
    }
}