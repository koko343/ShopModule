using ShopModule.Models;
using ShopModule.Services;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ShopModule.Controllers
{
    
    public class AdminController : Controller
    {
        private CategoryService categoryService;
        private PictureService pictureService;
        private ProductService productService;

        public AdminController()
        {
            this.categoryService = new CategoryService();
            this.pictureService = new PictureService();
            this.productService = new ProductService();
        }

        public ActionResult Login()
        {
            LoginViewModel logModel = new LoginViewModel();
            return View(logModel);
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (model.Login == "Admin" && model.Password == "123456")
            {
                FormsAuthentication.SetAuthCookie(model.Login, true);

                return RedirectToAction("Index");
            }

            return View("Login");
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return View();
        }

        [Authorize]
        [HandleErrorAttribute]
        public ActionResult Index()
        {
            return View(this.productService.GetAll());
        }

        [Authorize]
        [HandleErrorAttribute]
        public ActionResult CreateProduct()
        {
            ProductViewModel productViewModel = new ProductViewModel();
            productViewModel.CategoryItems = this.categoryService.GetAll();
            return View(productViewModel);
        }

        [Authorize]
        [HandleErrorAttribute]
        [HttpPost]
        public ActionResult CreateProduct(ProductViewModel productViewModel, HttpPostedFileBase image)
        {
            if(image != null && productViewModel!= null)
            {
                var result = this.productService.AddProduct(productViewModel.Name, productViewModel.Price, productViewModel.Description, 
                    productViewModel.Category, this.pictureService.AddNewImage(image));

                if(result == true)
                {
                    return RedirectToAction("Index");
                }
            }

            return View();
        }

        [Authorize]
        public ActionResult CreateCategory()
        {
            return View(new CategoryViewModel());
        }

        [Authorize]
        [HandleErrorAttribute]
        [HttpPost]
        public ActionResult CreateCategory(CategoryViewModel categoryViewModel)
        {
            if (this.categoryService.AddCategory(categoryViewModel.Name))
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        [Authorize]
        [HandleErrorAttribute]
        public ActionResult EditProduct(int id)
        {
            if (id!=0)
            {
                var product = this.productService.GetById(id);
                ProductViewModel productViewModel = new ProductViewModel()
                {
                    Id = id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Category = product.CategoryId.ToString(),
                    CategoryItems = this.categoryService.GetAll(),
                    PictureId = product.PictureId
                };

                return View(productViewModel);
            }

            return RedirectToAction("Index");
        }

        [Authorize]
        [HandleErrorAttribute]
        [HttpPost]
        public ActionResult EditProduct(ProductViewModel productViewModel, HttpPostedFileBase image, int pictureId)
        {
            if (productViewModel.Id != 0)
            {
                int picId = pictureId;

                if (image != null)
                {
                    if(this.pictureService.DeleteImage(productViewModel.PictureId))
                    {
                        picId = this.pictureService.AddNewImage(image);
                    }
                }

                var result = this.productService.EditProduct(productViewModel.Id,productViewModel.Name, productViewModel.Price, productViewModel.Description,
                    productViewModel.Category, picId);

                if (result == true)
                {
                    return RedirectToAction("Index");
                }
            }

            return View();
        }

        [Authorize]
        [HandleErrorAttribute]
        public ActionResult DeleteProduct(int id)
        {
            if (id != 0)
            {
                var product = this.productService.GetById(id);
                this.productService.DeleteProduct(id);
                this.pictureService.DeleteImage(product.PictureId);
                
            }

            return RedirectToAction("Index");
        }
    }
}