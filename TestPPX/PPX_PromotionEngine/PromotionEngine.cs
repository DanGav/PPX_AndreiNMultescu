using System.Collections.Generic;
using PPXModel;
using Visa;
using Loyalty;

namespace PPX_PromotionEngine
{
    /// <summary>
    /// PromotionEngine
    /// Assumptions:
    /// Each engine will get the original item price.
    /// 2 or more with the same id will have the same discounts.
    /// 
    /// </summary>
    public class PromotionEngine
    {
        /// <summary>
        /// GetDiscount method - totalDiscount for each items
        /// multiple items 
        /// </summary>
        /// <param name="items">List of items</param>
        /// <returns></returns>
        public List<(Item item, double newPrice)> GetDiscounts(List<Item> items)
        {
            List<Item> newItemList = new List<Item>();
            var result = new List<(Item, double)>();

            foreach (var item in items)
            {
                var promotionProvider = new LoyaltyPromotionEngine();
                var discountProvider = new VisaPromotionEngine();

                if (promotionProvider.GetDiscountableItemIds().Contains(item.Id) && discountProvider.GetDiscountableItemIds().Contains(item.Id))
                    newItemList.Add(item);

                //if item has no discount
                else
                    result.Add((item,item.Price));
                
            }

            foreach (var item in newItemList)
            {
                var promotionProvider = new LoyaltyPromotionEngine();
                var discount = promotionProvider.GetItemDiscount(item.Id, item.Price);
                var newPrice = item.Price - discount;

                var discountProvider = new VisaPromotionEngine();
                var discount2 = discountProvider.GetItemDiscount(item.Id, newPrice);
                newPrice -= discount2;

                result.Add((item, newPrice));
            }

            return result;
        }
    }
}
