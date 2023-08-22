﻿using KrosfyNewsHub.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using KrosfyNewsHub.Extensions;
using System.Text;
using DBHelper;
using System.Data.SqlClient;
using System.Data;
using System.Web.Helpers;
using System;

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
            //Ejemplo de llamada a la BD
            using (DB DB = new DB())
            {
                if (DB.Open())
                {
                    string strSql = $"SELECT ID, NombreCategoria, ParentCategoryID " + Environment.NewLine +
                                    $" FROM Category WITH (NOLOCK) ";

                    DataSet ds = DB.Execute(strSql);
                    if (ds == null)
                    {
                        throw DB.GetException();
                    }
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {

                    }
                    else { throw new Exception("Categorias no encontradas."); }
                    ds = null;
                    dt = null;
                }
                else
                {
                    throw DB.GetException();
                }
            }

            string rutaArchivoJson = Server.MapPath("~/Content/data/News.json");
            byte[] bytes = Encoding.UTF8.GetBytes(System.IO.File.ReadAllText(rutaArchivoJson));
            string contenidoJson = Encoding.UTF8.GetString(bytes);
            var news = JsonConvert.DeserializeObject<RequestNews>(contenidoJson);
            
            foreach (var subcategoria in news.News) {
                if(filterCategory != 0 && filterCategory != subcategoria.category) { continue; }
                if (filterSubCategory != 0 && filterSubCategory != subcategoria.subcategory) { continue; }
#pragma warning disable CS0219 // La variabile è assegnata, ma il suo valore non viene mai usato
#pragma warning disable CS0219 // La variabile è assegnata, ma il suo valore non viene mai usato
                string CategoryName = ""; string SubCategoryName = "";
#pragma warning restore CS0219 // La variabile è assegnata, ma il suo valore non viene mai usato
#pragma warning restore CS0219 // La variabile è assegnata, ma il suo valore non viene mai usato
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