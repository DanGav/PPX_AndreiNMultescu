using System.Collections.Generic;
using PPXModel;
using Visa;
using Loyalty;
using System;
using System.Linq;

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
        /// 


        public List<(Item item, double newPrice)> GetDiscounts(List<Item> items, List<Type> Engines)
        {
            List<Item> newItemList = new List<Item>();
            var result = new List<(Item, double)>();
            var promotionProvider = new LoyaltyPromotionEngine();
            var discountProvider = new VisaPromotionEngine();
            var DiscountItems = discountProvider.GetDiscountableItemIds();
            var PromItems = promotionProvider.GetDiscountableItemIds();


            ////Type t2 = typeof(LoyaltyPromotionEngine);
            ////Type t = Type.GetType($"{t2.AssemblyQualifiedName}");
            ////object o = Activator.CreateInstance<t> ;

            //List<Type> engines = new List<Type>
            //{
            //    typeof(LoyaltyPromotionEngine),
            //    typeof(VisaPromotionEngine)
            //};
            ////object obj = Activator.CreateInstance(engines[0]);
            //Type t = engines[0];
            
            //var obj2 = Activator.CreateInstance(t) as t.ReflectedType;
           

            foreach (Item item in items)
            {
                var newPrice = item.Price;
                if (PromItems.Contains(item.Id))
                {
                    var discount = 0.0;
                    try
                    {
                        
                        discount = promotionProvider.GetItemDiscount(item.Id, item.Price);
                    }
                    catch
                    {
                        discount = 0.0;
                    }

                    newPrice -= discount;
                }
                if (DiscountItems.Contains(item.Id))
                {
                    var discount = 0.0;
                    try
                    {

                        discount = discountProvider.GetItemDiscount(item.Id, item.Price);
                    }
                    catch
                    {
                        discount = 0.0;
                    }

                    newPrice -= discount;
                }        
                    result.Add((item, newPrice));                
            }
            return result;

        }



        //T GetInstance<T>() where T : new()
        //{
        //    T instance = new T();
        //    return instance;
        //}
    }




}




