using ShopModule.Core.Entities;
using ShopModule.Core.Interfaces.Repositories.Base;
using ShopModule.Data;
using ShopModule.Data.Repositories.Base;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopModule.Services
{
    public class PictureService
    {
        //Picture repository
        private IRepository<Picture> pictureRepository;
        //Context for db
        private DataContext context;

        public PictureService()
        {
            this.context = new DataContext();
            this.pictureRepository = new Repository<Picture>(this.context);
        }

        //Get all pictures from db
        public List<Picture> GetAll()
        {
            return this.pictureRepository.GetAll().ToList<Picture>();
        }

        //Add new image
        public int AddNewImage(HttpPostedFileBase image)
        {
            Picture img = new Picture();
            string GUI = "/Content/img/" + image.FileName;

            img.Name = image.FileName;
            img.Url = GUI;
            //Save item to the server storage using GUI
            image.SaveAs(HttpContext.Current.Server.MapPath(GUI));

            this.pictureRepository.Add(img);
            this.pictureRepository.SaveChanges();

            //Get id of saved element
            return this.pictureRepository.GetAll().ToList<Picture>().Last().Id;
        }

        //Delete item from database using ID
        public bool DeleteImage(int id)
        {
            //Find item by id
            var image = this.pictureRepository.GetById(id);
            if(image!=null)
            {
                //If item exists, delete it
                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(image.Url)))
                {
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(image.Url));
                }

                this.pictureRepository.Delete(image);
                this.pictureRepository.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
