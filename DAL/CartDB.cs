using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZTPSBD.Data;

namespace ZTPSBD.DAL
{
    public class CartDB
    {
        private List<Product> productsInCart;

        public void Clear()
        {
            productsInCart.Clear();
        }
        public CartDB()
        {
            productsInCart = new List<Product>();
        }

        public List<Product> GetProductList
        {
            get
            {
                return productsInCart;
            }
        }


        public void AddToCart(Product product)
        {
            productsInCart.Add(product);
        }



    }
}
