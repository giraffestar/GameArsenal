using System;

namespace GameArsenal.Inventories
{
    public enum ItemSlotAddResult
    {
        Success,
        FailByInvalidAddAmount,
        FailByMoreThanMaxAmount,
    }

    public enum ItemSlotRemoveResult
    {
        Success,
        FailByInvalidRemoveAmount,
        FailByMoreThanAmount,
    }
    
    public class ItemSlot<TKey> where TKey : struct
    {
        public static readonly ItemSlot<TKey> Empty = new ItemSlot<TKey>();

        public TKey ItemId { get; private set; }
        public DynamicItemId DynamicItemId { get; private set; }
        public int Amount { get; private set; }
        public int MaxAmount { get; private set; }

        public ItemSlot()
        {
        }
        
        public ItemSlot(IInventoryRule<TKey> rule, TKey itemId) : this(rule, itemId, DynamicItemId.Invalid)
        {
        }
        
        public ItemSlot(IInventoryRule<TKey> rule, TKey itemId, DynamicItemId dynamicItemId)
        {
            this.ItemId = itemId;
            this.DynamicItemId = dynamicItemId;
            this.MaxAmount = rule.GetItemSlotMaxAmount(itemId);
            this.Amount = 0;
        }

        public ItemSlotAddResult AddAmount(int addValue)
        {
            if (addValue <= 0)
            {
                throw new ArgumentException($"{ItemSlotAddResult.FailByInvalidAddAmount} {nameof(addValue)}:{addValue}");
            }

            if (this.Amount + addValue > this.MaxAmount)
            {
                throw new ArgumentException($"{ItemSlotAddResult.FailByMoreThanMaxAmount} {nameof(this.MaxAmount)}:{this.MaxAmount} {nameof(addValue)}:{addValue}");
            }

            this.Amount += addValue;
            
            return ItemSlotAddResult.Success;
        }

        public ItemSlotRemoveResult RemoveAmount(int removeValue)
        {
            if (removeValue <= 0)
            {
                return ItemSlotRemoveResult.FailByInvalidRemoveAmount;
            }

            if (removeValue > this.Amount)
            {
                return ItemSlotRemoveResult.FailByMoreThanAmount;
            }

            this.Amount -= removeValue;
            
            return ItemSlotRemoveResult.Success;
        }
    }
}