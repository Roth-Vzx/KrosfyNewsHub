using KrosfyNewsHub.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Runtime.Remoting.Contexts;

namespace KrosfyNewsHub.Controllers
{
    public class HomeController : Controller
    {
        
        List<Article> BitcoinNews = new List<Article>();
        List<Article> EthereumNews = new List<Article>();
        List<Article> TetherNews = new List<Article>();
        List<Article> CardanaNews = new List<Article>();
        List<(List<Article> articulos, string nombre)> _AllNews = new List<(List<Article> articulos, string nombre)>();
        public ActionResult Index()
        {
            
            LoadData(ref BitcoinNews, ref EthereumNews, ref TetherNews, ref CardanaNews, true);

            _AllNews.Add((BitcoinNews, "Bitcoin"));
            _AllNews.Add((EthereumNews, "Ethereum"));
            _AllNews.Add((TetherNews, "Tether"));
            _AllNews.Add((CardanaNews, "Cardana"));

            ViewBag.News = _AllNews;
            return View();
        }

        public void LoadData(ref List<Article> BitcoinNews, ref List<Article> EthereumNews, ref List<Article> TetherNews, ref List<Article> CardanaNews, bool filterIndex)
        {
            string rutaArchivoJson = Server.MapPath("~/Content/data/bitcoin.json");
            string contenidoJson = System.IO.File.ReadAllText(rutaArchivoJson);
            BitcoinNews = JsonConvert.DeserializeObject<RequestNews>(contenidoJson).articles;
            if (filterIndex) { LastSevenArticles(ref BitcoinNews); };

            rutaArchivoJson = Server.MapPath("~/Content/data/etherium.json");
            contenidoJson = System.IO.File.ReadAllText(rutaArchivoJson);
            EthereumNews = JsonConvert.DeserializeObject<RequestNews>(contenidoJson).articles;
            if (filterIndex) { LastSevenArticles(ref EthereumNews); };

            rutaArchivoJson = Server.MapPath("~/Content/data/tether.json");
            contenidoJson = System.IO.File.ReadAllText(rutaArchivoJson);
            TetherNews = JsonConvert.DeserializeObject<RequestNews>(contenidoJson).articles;
            if (filterIndex) { LastSevenArticles(ref TetherNews); };

            rutaArchivoJson = Server.MapPath("~/Content/data/cardano.json");
            contenidoJson = System.IO.File.ReadAllText(rutaArchivoJson);
            CardanaNews = JsonConvert.DeserializeObject<RequestNews>(contenidoJson).articles;
            if (filterIndex) { LastSevenArticles(ref CardanaNews); };
        }

        public void LastSevenArticles(ref List<Article> lista)
        {
            lista = lista.Take(7).ToList();
        }

        public ActionResult SearchResult()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult SinglePost()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        public ActionResult Category(string type)
        {
            type = type.ToUpper(); 
            LoadData(ref BitcoinNews, ref EthereumNews, ref TetherNews, ref CardanaNews, false); 
            ViewBag.Message = type;
            
            if(type == "BITCOIN")
            {
                ViewBag.Noticias = BitcoinNews;
            }

            return View();
        }

    }
}