using System;
using System.Collections.Generic;

namespace GameArsenal.Inventories
{
    public class Inventory<TKey> : IBagController<TKey> where TKey : struct
    {
        public int BagCount => this.bags.Count;
        
        private readonly Dictionary<TKey, ItemToken<TKey>> tokens;
        private readonly List<ItemBag<TKey>> bags;
        private readonly IInventoryRule<TKey> rule;

        public Inventory(IInventoryRule<TKey> rule)
        {
            this.tokens = new Dictionary<TKey, ItemToken<TKey>>();
            this.bags = new List<ItemBag<TKey>>(1);
            this.rule = rule;
        }

        public void AddItemBag(int bagSize)
        {
            var itemBag = new ItemBag<TKey>(bagSize);
            this.bags.Add(itemBag);
        }
        
        // RemoveItemBag
        
        public bool AddStaticItem(TKey itemId, int amount)
        {
            if (!this.tokens.TryGetValue(itemId, out var token))
            {
                token = new ItemToken<TKey>(this, this.rule, itemId);
                this.tokens.Add(itemId, token);
            }

            return token.AddStaticItem(amount);
        }

        // public bool AddStaticItem(TKey itemId, int amount, int bagIndex, int slotIndex)
        // {
        //     
        // }
        
        public bool RemoveStaticItem(TKey itemId, int amount)
        {
            return this.tokens.TryGetValue(itemId, out var token) && token.RemoveStaticItem(amount);
        }
        
        // public void RemoveStaticItem(TKey itemId, int amount, int bagIndex, int slotIndex)
        // {
        //     
        // }

        public bool AddDynamicItem(TKey itemId, DynamicItemId dynamicItemId)
        {
            if (!this.tokens.TryGetValue(itemId, out var token))
            {
                token = new ItemToken<TKey>(this, this.rule, itemId);
                this.tokens.Add(itemId, token);
            }

            return token.AddDynamicItem(dynamicItemId);
        }

        public bool RemoveDynamicItem(TKey itemId, DynamicItemId dynamicItemId)
        {
            return this.tokens.TryGetValue(itemId, out var token) && token.RemoveDynamicItem(dynamicItemId);
        }

        // public void AddDynamicItem(TKey itemId, DynamicItemId dynamicItemId, int bagIndex, int slotIndex)
        // {
        //     
        // }
        
        public (int bagIndex, int slotIndex) GetNextEmptySlot()
        {
            for (int i = 0; i < this.bags.Count; i++)
            {
                var bag = this.bags[i];
                var slotIndex = bag.GetFirstEmptyIndex();
                if (slotIndex == -1)
                {
                    continue;
                }

                return (i, slotIndex);
            }

            return (-1, -1);
        }

        public int GetEmptySlotCount()
        {
            var sum = 0;
            foreach (var bag in this.bags)
            {
                sum += bag.GetEmptySlotCount();
            }

            return sum;
        }

        public bool AddToBag(int bagIndex, int slotIndex, ItemSlot<TKey> itemSlot)
        {
            var result = this.bags[bagIndex].OccupySlot(itemSlot, slotIndex);

            return result == ItemBagOccupySlotResult.Success;
        }

        public bool RemoveFromBag(ItemSlot<TKey> itemSlot)
        {
            var matchFound = false;
            foreach (var bag in this.bags)
            {
                var index = 0;
                foreach (var slot in bag)
                {
                    matchFound = slot.Equals(itemSlot);
                    if (matchFound)
                    {
                        break;
                    }

                    index++;
                }

                if (matchFound)
                {
                    bag.UnoccupySlot(index, out _);
                    break;
                }
            }

            return matchFound;
        }

        public ItemSlot<TKey> RemoveFromBag(int bagIndex, int slotIndex)
        {
            this.bags[bagIndex].UnoccupySlot(slotIndex, out var itemSlot);

            return itemSlot;
        }
    }
}