using ShopModule.Models.Shopping;
using ShopModule.Services;
using System;
using System.Web.Mvc;

namespace ShopModule.Controllers
{
    public class CartController : Controller
    {
        //Product service
        private ProductService productService;

        public CartController()
        {
            productService = new ProductService();
        }

        public ActionResult Index()
        {
            decimal total = 0;
            var itemsInCart = 0;
            //Get data from session
            CartViewModel cartViewModel = (CartViewModel)Session["Cart"];

            if (cartViewModel != null && cartViewModel.CartList.Count != 0)
            {
                //Count items in cart
                itemsInCart = cartViewModel.CartList.Count;

                //Count total sum
                foreach (var item in cartViewModel.CartList)
                {
                    total += item.Price;
                }

                cartViewModel.Total = total;
                ViewBag.ItemsInCart = itemsInCart;

                return View(cartViewModel);
            }

            ViewBag.ItemsInCart = itemsInCart;
            return View("KeepShoppint");
        }

        //Remove item from cart by id
        public ActionResult RemoveFromCart(int id)
        {
            if (id != 0)
            {
                CartViewModel cart = (CartViewModel)Session["Cart"];

                if (cart != null)
                {
                    cart.CartList.Remove(cart.CartList.Find(ci => ci.ProductId == id));
                }
            }

            return RedirectToAction("Index");
        }

        //Clear all items from cart
        public ActionResult ClearCart()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }

        //Add item to cart by id
        public ActionResult AddToCart(int id)
        {
            if (id != 0)
            {
                //Get cart from session and create cart item
                CartViewModel cart = (CartViewModel)Session["Cart"];
                CartItemViewModel cartItem = null;

                //if cart null create new cart
                if (cart == null)
                {
                    cart = new CartViewModel();
                    cart.DateCreated = DateTime.Now;
                    Session["Cart"] = cart;
                }

                //Get Product by id
                var currentProduct = this.productService.GetById(id);
                if (currentProduct != null)
                {
                    //if product not null create new cartItem
                    cartItem = new CartItemViewModel();
                    cartItem.Description = currentProduct.Description;
                    cartItem.Picture = currentProduct.Picture;
                    cartItem.Name = currentProduct.Name;
                    cartItem.Price = currentProduct.Price;
                    cartItem.ProductId = currentProduct.Id;
                }

                cart.CartList.Add(cartItem);
            }

            return RedirectToAction("Index", "Home", new { id = id });
        }
    }
}