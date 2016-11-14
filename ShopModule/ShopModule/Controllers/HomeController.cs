using ShopModule.Models;
using ShopModule.Models.Shopping;
using ShopModule.Services;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ShopModule.Controllers
{
    public class HomeController : Controller
    {
        //Product service
        private ProductService productService;

        public HomeController()
        {
            this.productService = new ProductService();
        }

        public ActionResult Index()
        {
            //Get all products
            var products = this.productService.GetAll();

            if (products != null)
            {
                //Get cart from session
                CartViewModel currentCart = (CartViewModel)Session["Cart"];
                //Create product list for displaying data
                List<ProductViewModel> productList = new List<ProductViewModel>();

                foreach (var product in products)
                {
                    ProductViewModel productToShow = new ProductViewModel();
                    productToShow.Description = product.Description;
                    productToShow.Picture = product.Picture;
                    productToShow.Name = product.Name;
                    productToShow.Price = product.Price;
                    productToShow.Id = product.Id;

                    if(currentCart!=null && currentCart.CartList!=null)
                    {
                        foreach(var cart in currentCart.CartList)
                        {
                            if(cart.ProductId==productToShow.Id)
                            {
                                productToShow.IsBought = true;
                            }
                        }
                    }

                    productList.Add(productToShow);
                }

                ViewBag.ItemsInCart = CartCheck();
                return View(productList);
            }

            return View();
        }

        //Check number of elements
        [ChildActionOnly]
        public int CartCheck()
        {
            CartViewModel currentCart = (CartViewModel)Session["Cart"];

            if (currentCart != null && currentCart.CartList != null)
            {
                return currentCart.CartList.Count;
            }

            return 0;
        }
    }
}