using System.Collections.Generic;

namespace GameArsenal.Inventories
{
    public class ItemToken<TKey> where TKey : struct
    {
        private readonly TKey itemId;
        private readonly IBagController<TKey> bagController;
        private readonly IInventoryRule<TKey> rule;
        private readonly List<ItemSlot<TKey>> slots;

        public ItemToken(IBagController<TKey> bagController, IInventoryRule<TKey> rule, TKey itemId)
        {
            this.bagController = bagController;
            this.rule = rule;
            this.itemId = itemId;
            this.slots = new List<ItemSlot<TKey>>();
        }

        public int GetTotalAmount()
        {
            var sum = 0;
            foreach (var itemSlot in this.slots)
            {
                sum += itemSlot.Amount;
            }

            return sum;
        }

        public bool AddStaticItem(int amount)
        {
            if (amount > GetTotalAddableAmount())
            {
                return false;
            }
            
            var remainingAmount = amount;
            foreach (var itemSlot in this.slots)
            {
                var availableAmount = itemSlot.MaxAmount - itemSlot.Amount;
                if (remainingAmount > availableAmount)
                {
                    itemSlot.AddAmount(availableAmount);
                    remainingAmount -= availableAmount;
                }
                else
                {
                    itemSlot.AddAmount(remainingAmount);
                    break;
                }
            }

            while (remainingAmount > 0)
            {
                var itemSlot = new ItemSlot<TKey>(this.rule, this.itemId);
                var addingAmount = remainingAmount > itemSlot.MaxAmount ? itemSlot.MaxAmount : remainingAmount;
                itemSlot.AddAmount(addingAmount);
                remainingAmount -= addingAmount;
                
                this.slots.Add(itemSlot);
                var next = this.bagController.GetNextEmptySlot();
                this.bagController.AddToBag(next.bagIndex, next.slotIndex, itemSlot);
            }

            return true;
        }

        public bool AddDynamicItem(DynamicItemId dynamicItemId)
        {
            if (this.bagController.GetEmptySlotCount() < 1)
            {
                return false;
            }

            var itemSlot = new ItemSlot<TKey>(this.rule, this.itemId, dynamicItemId);
            itemSlot.AddAmount(1);
            
            this.slots.Add(itemSlot);
            var next = this.bagController.GetNextEmptySlot();
            this.bagController.AddToBag(next.bagIndex, next.slotIndex, itemSlot);

            return true;
        }

        public bool RemoveStaticItem(int amount)
        {
            if (amount > GetTotalAmount())
            {
                return false;
            }

            var remainingAmount = amount;
            var orderedList = new List<ItemSlot<TKey>>(this.slots);
            orderedList.Sort((x,y) => x.Amount.CompareTo(y.Amount));

            foreach (var itemSlot in orderedList)
            {
                var removeAmount = remainingAmount > itemSlot.Amount ? itemSlot.Amount : remainingAmount;
                itemSlot.RemoveAmount(removeAmount);
                remainingAmount -= removeAmount;

                if (itemSlot.Amount == 0)
                {
                    this.bagController.RemoveFromBag(itemSlot);
                }

                if (remainingAmount == 0)
                {
                    break;
                }
            }

            this.slots.RemoveAll(x => x.Amount == 0);

            return true;
        }

        public bool RemoveDynamicItem(DynamicItemId dynamicItemId)
        {
            var removeSlot = this.slots.Find(x => x.DynamicItemId == dynamicItemId);
            if (removeSlot == null)
            {
                return false;
            }

            this.bagController.RemoveFromBag(removeSlot);
            this.slots.Remove(removeSlot);

            return true;
        }

        private int GetTotalAddableAmount()
        {
            var sum = 0;
            foreach (var itemSlot in this.slots)
            {
                sum += itemSlot.MaxAmount - itemSlot.Amount;
            }

            var emptySlotCount = this.bagController.GetEmptySlotCount();
            sum += this.rule.GetItemSlotMaxAmount(this.itemId) * emptySlotCount;

            return sum;
        }
    }
}