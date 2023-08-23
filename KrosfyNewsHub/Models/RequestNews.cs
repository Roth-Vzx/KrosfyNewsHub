using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KrosfyNewsHub.Models
{
    #region clases internas
    public class Categories
    {
        public string name = "";
        public int ID = 0;
        public List<Subcategory> subcategories = new List<Subcategory>();
    }

    public class Subcategory
    {
        public string name = "";
        public int ID = 0;
        public string searchText = "";
    }


    #endregion

    #region Classi Newsomatic
    public class Article
    {
        public string title { get; set; }
        public string content { get; set; }
        public string url { get; set; }
        public string description { get; set; }
        public string category { get; set; }
        public string language { get; set; }
        public string country { get; set; }
        public string author { get; set; }
        public string publishedAt { get; set; }
        public string urlToImage { get; set; }
        public Source source { get; set; }

    }

    public class ArticleKrosfy
    {
        public string title { get; set; }
        public string content { get; set; }
        public string url { get; set; }
        public string description { get; set; }
        public string category { get; set; }
        public string language { get; set; }
        public string country { get; set; }
        public string author { get; set; }
        public string publishedAt { get; set; }
        public string urlToImage { get; set; }
        public string source { get; set; }
        public long id { get; set; }


        public ArticleKrosfy(string tit, string cont, string urlPage, string desc, string lang, string pais, string auth, string pubAt, string imgurl, string origin, long idTable)
        {
            id = idTable;
            title = tit;
            content = cont;
            url = urlPage;
            description = desc;
            language = lang;
            country = pais;
            author = auth;
            publishedAt = pubAt;
            urlToImage = imgurl;
            source = origin;

        }

    }

    public class Source
    {
        public string name { get; set; }

        public Source(string nombre)
        {
            name = nombre;
        }
        public Source()
        {
            
        }
    }
    #endregion

}