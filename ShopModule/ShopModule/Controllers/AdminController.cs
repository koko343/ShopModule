using ShopModule.Models;
using ShopModule.Services;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ShopModule.Controllers
{
    
    public class AdminController : Controller
    {
        //Service for categories
        private CategoryService categoryService;
        //Picture service
        private PictureService pictureService;
        //Product service
        private ProductService productService;

        public AdminController()
        {
            this.categoryService = new CategoryService();
            this.pictureService = new PictureService();
            this.productService = new ProductService();
        }

        //Login admin page
        public ActionResult Login()
        {
            //Login viewModel
            LoginViewModel logModel = new LoginViewModel();
            return View(logModel);
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            //If pusword and login corespond set outhirization cookie and redirect to index
            if (model.Login == "Admin" && model.Password == "123456")
            {
                FormsAuthentication.SetAuthCookie(model.Login, true);

                return RedirectToAction("Index");
            }

            return View("Login");
        }

        //Logount admin page
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return View();
        }

        //Get Index page and list of all products using product service
        [Authorize]
        [HandleErrorAttribute]
        public ActionResult Index()
        {
            return View(this.productService.GetAll());
        }

        //Create product
        [Authorize]
        [HandleErrorAttribute]
        public ActionResult CreateProduct()
        {
            //Creating ProductViewModel for a view
            ProductViewModel productViewModel = new ProductViewModel();

            //Get list values from service to display them at the view 
            productViewModel.CategoryItems = this.categoryService.GetAll();
            return View(productViewModel);
        }

        //Create Product POST
        [Authorize]
        [HandleErrorAttribute]
        [HttpPost]
        public ActionResult CreateProduct(ProductViewModel productViewModel, HttpPostedFileBase image)
        {
            if(image != null && productViewModel!= null)
            {
                //Path values to the service
                var result = this.productService.AddProduct(productViewModel.Name, productViewModel.Price, productViewModel.Description, 
                    productViewModel.Category, this.pictureService.AddNewImage(image));

                //If product created redirectto index
                if(result == true)
                {
                    return RedirectToAction("Index");
                }
            }

            return View();
        }

        //Create category
        [Authorize]
        public ActionResult CreateCategory()
        {
            return View(new CategoryViewModel());
        }

        //Create category POST
        [Authorize]
        [HandleErrorAttribute]
        [HttpPost]
        public ActionResult CreateCategory(CategoryViewModel categoryViewModel)
        {
            //Path value to the service
            if (this.categoryService.AddCategory(categoryViewModel.Name))
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        //Edit selected product by ID
        [Authorize]
        [HandleErrorAttribute]
        public ActionResult EditProduct(int id)
        {
            if (id!=0)
            {
                //Path values to the service
                var product = this.productService.GetById(id);

                //Create ViewModel for EditProductView
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

        //Edit selected product POST
        [Authorize]
        [HandleErrorAttribute]
        [HttpPost]
        public ActionResult EditProduct(ProductViewModel productViewModel, HttpPostedFileBase image, int pictureId)
        {
            if (productViewModel.Id != 0)
            {
                int picId = pictureId;

                //If new image uploaded, delete previous emage for this product
                if (image != null)
                {
                    if(this.pictureService.DeleteImage(productViewModel.PictureId))
                    {
                        picId = this.pictureService.AddNewImage(image);
                    }
                }

                //Path values to the service
                var result = this.productService.EditProduct(productViewModel.Id,productViewModel.Name, productViewModel.Price, productViewModel.Description,
                    productViewModel.Category, picId);

                //If Edited redirect to Index
                if (result == true)
                {
                    return RedirectToAction("Index");
                }
            }

            return View();
        }

        //Delete selected Product
        [Authorize]
        [HandleErrorAttribute]
        public ActionResult DeleteProduct(int id)
        {
            if (id != 0)
            {
                //Get product by id using servcie
                var product = this.productService.GetById(id);

                //Delete product by id using servcie
                this.productService.DeleteProduct(id);

                //Delete picture by id using servcie
                this.pictureService.DeleteImage(product.PictureId);
                
            }

            return RedirectToAction("Index");
        }
    }
}