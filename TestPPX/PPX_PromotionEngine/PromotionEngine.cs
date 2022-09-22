using System.Collections.Generic;
using PPXModel;
using Visa;
using Loyalty;
using System;
using System.Linq;
using System.Reflection;

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



            List<(object,List<int>)> Providers = new List<(object, List<int>)>();
            
            foreach(var engine in Engines)
            {
                var provider= Activator.CreateInstance(engine);
                MethodInfo method = engine.GetMethod("GetDiscountableItemIds");
                var DiscountableItems = (List<int>)method.Invoke(provider, null);
                Providers.Add((provider, DiscountableItems));
            }

            foreach (Item item in items)
            {
                var newPrice = item.Price;
                foreach((var prov , var list) in Providers)
                {
                    if (list.Contains(item.Id))
                    {
                        var discount = 0.0;

                        try
                        {
                            MethodInfo method = prov.GetType().GetMethod("GetItemDiscount");
                            object[] parametersArray = new object[] { item.Id, item.Price };
                             discount = (double)method.Invoke(prov, parametersArray);
                             
                        }
                        catch
                        {
                            discount = 0.0;
                        }
                        newPrice -= discount;
                    }

                }
                result.Add((item, newPrice));
            }






            //    foreach (Item item in items)
            //{
            //    var newPrice = item.Price;
            //    if (PromItems.Contains(item.Id))
            //    {
            //        var discount = 0.0;
            //        try
            //        {
                        
            //            discount = promotionProvider.GetItemDiscount(item.Id, item.Price);
            //        }
            //        catch
            //        {
            //            discount = 0.0;
            //        }

            //        newPrice -= discount;
            //    }
            //    if (DiscountItems.Contains(item.Id))
            //    {
            //        var discount = 0.0;
            //        try
            //        {

            //            discount = discountProvider.GetItemDiscount(item.Id, item.Price);
            //        }
            //        catch
            //        {
            //            discount = 0.0;
            //        }

            //        newPrice -= discount;
            //    }        
            //        result.Add((item, newPrice));                
            //}
            return result;

        }
    }




}




