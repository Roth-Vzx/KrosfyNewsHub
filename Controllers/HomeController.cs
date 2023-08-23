using KrosfyNewsHub.Models;
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
using System.Web;

namespace KrosfyNewsHub.Controllers
{
    public class HomeController : Controller
    {
        List<(List<ArticleKrosfy> articulos, int CategoriaID, int SubCategoriaID)> _AllNews = new List<(List<ArticleKrosfy> articulos, int CategoriaID, int SubCategoriaID)>();

        public ActionResult Index(int categoriaID = 0)
        {
            string lang = NewsExtensions.GetLanguage();
            LoadData(lang, true, categoriaID);
            ViewBag.News = _AllNews;
            return View();
        }


        public void LoadData(string lang, bool fromIndex=false, int filterCategory = 0, int filterSubCategory = 0)
        {
            try
            {
                using (DB DB = new DB())
                {
                    if (DB.Open())
                    {
                        string err = "";
                        var Categorias = NewsExtensions.GetCategories_INT(ref err);
                        if (filterCategory != 0) //Si estoy filtrando una categoria 
                        {
                            Categorias = Categorias.Where(x => x.ID == filterCategory).ToList();
                        }

                        if (filterSubCategory != 0) // Si estoy filtrando una subcategoria
                        {
                            foreach (var Categoria in Categorias)
                            {
                                Categoria.subcategories = Categoria.subcategories.Where(x => x.ID == filterSubCategory).ToList();
                            }
                        }

                        foreach (var cat in Categorias)
                        {
                            foreach (var subcat in cat.subcategories)
                            {
                                string takesql = fromIndex ? " TOP (8) " : "";
                                string strSql = $" SELECT {takesql} * " + Environment.NewLine +
                                                $" FROM News WITH (NOLOCK) " + Environment.NewLine +
                                                $" WHERE Language='{lang}' AND IsPublished = 1 AND CategoryID ={cat.ID} AND SubCategoryID = {subcat.ID}" + Environment.NewLine +
                                                $" ORDER BY PublishedAt DESC";

                                DataSet ds = DB.Execute(strSql);
                                if (ds == null) { throw DB.GetException(); }

                                DataTableReader dr = ds.CreateDataReader();
                                ds = null;
                                List<ArticleKrosfy> list = new List<ArticleKrosfy>();
                                while (dr.Read())
                                {
                                    string tit = "", cont = "", urlPage = "", desc = "", language = "", pais = "", auth = "", pubAt = "", imgurl = "", origin = "";
                                    
                                    long id = Convert.ToInt64(dr["ID"]);
                                    tit = dr["Title"].ToString();
                                    cont = dr["contenido"].ToString();
                                    urlPage = dr["Url"].ToString();
                                    imgurl = dr["ImageUrl"].ToString();
                                    desc = dr["Description"].ToString();
                                    language = dr["Language"].ToString();
                                    pais = dr["Country"].ToString();
                                    auth = dr["Author"].ToString();
                                    pubAt = (Convert.ToDateTime(dr["PublishedAt"])).ToString("dd-MM-yyyy");
                                    origin = dr["Source"].ToString();

                                    ArticleKrosfy noticia = new ArticleKrosfy(tit,cont,urlPage, desc, language, pais, auth, pubAt, imgurl, origin, id);
                                    list.Add(noticia);
                                }
                                if(list.Count > 0)
                                {
                                    _AllNews.Add((list, cat.ID, subcat.ID));
                                }
                            }
                        }
                    }
                    else
                    {
                        throw DB.GetException();
                    }
                }

            }
            catch (Exception ex)
            {

            }
            //Ejemplo de llamada a la BD
            
            
            //foreach (var subcategoria in news.News) {
            //    if(filterCategory != 0 && filterCategory != subcategoria.category) { continue; }
            //    if (filterSubCategory != 0 && filterSubCategory != subcategoria.subcategory) { continue; }
            //    List<Article> SubCategoryNews = filterIndex ? LastArticles(subcategoria.content.articles) : subcategoria.content.articles;
            //    _AllNews.Add((SubCategoryNews, subcategoria.category, subcategoria.subcategory));
            //}
           
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
            string lang = NewsExtensions.GetLanguage();
            LoadData(lang,false,0, SubCategoriaID); 
            ViewBag.Category = NewsExtensions.GetCategoryName(categoriaID).ToUpper();
            ViewBag.Subcategory = NewsExtensions.GetSubCategoryName(SubCategoriaID).ToUpper();
            ViewBag.News = _AllNews;

            return View();
        }

    }
}