using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MagazinOnline.Data;

namespace MagazinOnline.Models
{
    public class MarketCapValues:PageModel
    {
        public List<AssignedCategoryData> AssignedCategoryDataList;

        public void PopulateAssignedCategoryData(MagazinOnlineContext context, Cryptocurrency cryptocurrency)
        {
            var allMarketCaps = context.MarketCap;
            var cryptocurrencyMarketCaps = new HashSet<int>(cryptocurrency.CryptoMarketCaps.Select(c => c.MarketCapID));
            AssignedCategoryDataList = new List<AssignedCategoryData>();
            foreach (var cat in  allMarketCaps)
            { 
                AssignedCategoryDataList.Add(new AssignedCategoryData
                { 
                    MarketCapID = cat.ID,
                    Name = cat.MarketCapValue,                         
                    Assigned = cryptocurrencyMarketCaps.Contains(cat.ID)
                 });
            }

        }

        public void UpdateCryptoMarketCap(MagazinOnlineContext context, string[] selectedMarketCaps, Cryptocurrency cryptocurrencyToUpdate)
        {
            if (selectedMarketCaps == null)
            {
                cryptocurrencyToUpdate.CryptoMarketCaps = new List<CryptoMarketCap>(); 
                return; 
            }

            var selectedMarketCapsHS = new HashSet<string>(selectedMarketCaps);
            var cryptocurrencyMarketCaps = new HashSet<int>(cryptocurrencyToUpdate.CryptoMarketCaps.Select(c => c.MarketCap.ID));

            foreach (var cat in context.CryptoMarketCap)
            {
                if (selectedMarketCapsHS.Contains(cat.ID.ToString()))
                {
                    if (!cryptocurrencyMarketCaps.Contains(cat.ID))
                    {
                        cryptocurrencyToUpdate.CryptoMarketCaps.Add(new CryptoMarketCap
                        { 
                                 CryptocurrencyID = cryptocurrencyToUpdate.ID,MarketCapID = cat.ID 
                            
                         });

                    }

                }
                
                    else
                    {
                         if (cryptocurrencyMarketCaps.Contains(cat.ID))
                         {
                        CryptoMarketCap courseToRemove = cryptocurrencyToUpdate.CryptoMarketCaps.SingleOrDefault(i => i.MarketCap.ID == cat.ID);
                        context.Remove(courseToRemove);
                         }
                    }
            }

        }



    }
}
