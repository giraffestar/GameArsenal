using System.Collections;
using System.Collections.Generic;

namespace GameArsenal.Inventories
{
    public enum ItemBagIncreaseBagSizeResult
    {
        Success,
        FailByInvalidAmount,
    }

    public enum ItemBagOccupySlotResult
    {
        Success,
        FailByInvalidSlotIndex,
        FailByAlreadyOccupied,
    }

    public enum ItemBagUnoccupySlotResult
    {
        Success,
        FailByInvalidSlotIndex,
        FailByAlreadyEmpty,
    }
    
    public class ItemBag<TKey> : IReadOnlyList<ItemSlot<TKey>> where TKey : struct
    {
        public int Count => this.slots.Count;
        public ItemSlot<TKey> this[int index] => this.slots[index];
        
        private readonly List<ItemSlot<TKey>> slots;

        public ItemBag(int bagSize)
        {
            this.slots = new List<ItemSlot<TKey>>(bagSize);
            for (var i = 0; i < bagSize; i++)
            {
                this.slots.Add(ItemSlot<TKey>.Empty);
            }
        }

        public int GetEmptySlotCount()
        {
            var sum = 0;
            foreach (var itemSlot in this.slots)
            {
                if (itemSlot.Equals(ItemSlot<TKey>.Empty))
                {
                    sum += 1;
                }
            }

            return sum;
        }

        public ItemBagIncreaseBagSizeResult IncreaseBagSize(int increaseAmount)
        {
            if (increaseAmount <= 0)
            {
                return ItemBagIncreaseBagSizeResult.FailByInvalidAmount;
            }

            for (var i = 0; i < increaseAmount; i++)
            {
                this.slots.Add(ItemSlot<TKey>.Empty);
            }

            return ItemBagIncreaseBagSizeResult.Success;
        }

        /// <summary>
        /// Returns the first empty slot index.
        /// </summary>
        /// <returns>Returns -1 if full.</returns>
        public int GetFirstEmptyIndex()
        {
            for (var i = 0; i < this.slots.Count; i++)
            {
                if (this.slots[i].Equals(ItemSlot<TKey>.Empty))
                {
                    return i;
                }
            }

            return -1;
        }

        public ItemBagOccupySlotResult OccupySlot(ItemSlot<TKey> slot, int index)
        {
            if (index < 0 || index >= this.slots.Count)
            {
                return ItemBagOccupySlotResult.FailByInvalidSlotIndex;
            }

            if (this.slots[index].Equals(ItemSlot<TKey>.Empty))
            {
                return ItemBagOccupySlotResult.FailByAlreadyOccupied;
            }

            this.slots[index] = slot;

            return ItemBagOccupySlotResult.Success;
        }

        public ItemBagUnoccupySlotResult UnoccupySlot(int index, out ItemSlot<TKey> slot)
        {
            slot = null;
            
            if (index < 0 || index >= this.slots.Count)
            {
                return ItemBagUnoccupySlotResult.FailByInvalidSlotIndex;
            }

            if (this.slots[index].Equals(ItemSlot<TKey>.Empty))
            {
                return ItemBagUnoccupySlotResult.FailByAlreadyEmpty;
            }

            slot = this.slots[index];
            this.slots[index] = ItemSlot<TKey>.Empty;

            return ItemBagUnoccupySlotResult.Success;
        }

        public IEnumerator<ItemSlot<TKey>> GetEnumerator()
        {
            return this.slots.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}