using KrosfyNewsHub.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using KrosfyNewsHub.Extensions;
using System.Text;

namespace KrosfyNewsHub.Controllers
{
    public class HomeController : Controller
    {
        List<Article> BitcoinNews = new List<Article>();
        List<Article> EthereumNews = new List<Article>();
        List<Article> TetherNews = new List<Article>();
        List<Article> CardanaNews = new List<Article>();
        List<(List<Article> articulos, int CategoriaID, int SubCategoriaID)> _AllNews = new List<(List<Article> articulos, int CategoriaID, int SubCategoriaID)>();

        public ActionResult Index(int categoriaID = 0)
        {
            LoadData(true, categoriaID);
            ViewBag.News = _AllNews;
            return View();
        }


        public void LoadData(bool filterIndex=false, int filterCategory = 0, int filterSubCategory = 0)
        {
            string rutaArchivoJson = Server.MapPath("~/Content/data/News.json");
            byte[] bytes = Encoding.UTF8.GetBytes(System.IO.File.ReadAllText(rutaArchivoJson));
            string contenidoJson = Encoding.UTF8.GetString(bytes);
            var news = JsonConvert.DeserializeObject<RequestNews>(contenidoJson);
            
            foreach (var subcategoria in news.News) {
                if(filterCategory != 0 && filterCategory != subcategoria.category) { continue; }
                if (filterSubCategory != 0 && filterSubCategory != subcategoria.subcategory) { continue; }
                string CategoryName = ""; string SubCategoryName = "";
                List<Article> SubCategoryNews = filterIndex ? LastArticles(subcategoria.content.articles) : subcategoria.content.articles;
                _AllNews.Add((SubCategoryNews, subcategoria.category, subcategoria.subcategory));
            }
           
        }

        public List<Article> LastArticles(List<Article> lista)
        {
            return (lista.Take(8).ToList());
        }

        public ActionResult SearchResult()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult SinglePost(int idPost)
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        public ActionResult Category(int categoriaID, int SubCategoriaID)
        {
            
            LoadData(false,0, SubCategoriaID); 
            ViewBag.Category = NewsExtensions.GetCategoryName(categoriaID).ToUpper();
            ViewBag.Subcategory = NewsExtensions.GetSubCategoryName(SubCategoriaID).ToUpper();
            ViewBag.News = _AllNews;

            return View();
        }

    }
}