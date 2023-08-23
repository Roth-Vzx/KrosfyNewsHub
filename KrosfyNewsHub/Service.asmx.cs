using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Web;
using System.Web.Services;
using static KrosfyNewsHub.Service;
using System.Web.UI.WebControls;
using DBHelper;
using System.Data;
using KrosfyNewsHub.Extensions;

namespace KrosfyNewsHub
{
    /// <summary>
    /// Descrizione di riepilogo per Service
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Per consentire la chiamata di questo servizio Web dallo script utilizzando ASP.NET AJAX, rimuovere il commento dalla riga seguente. 
    // [System.Web.Script.Services.ScriptService]
    public class Service : WebService
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
        
        

        public class Source
        {
            public string name { get; set; }
        }
        #endregion

        #region variables internas
        string tokenInt = "KrosfyNH2023$.AR";
        #endregion


        // Meteremos a disposicion api's que permiten enviar las ultimas noticias del dia
        // Una api que se encarga de gestionar o mandar email a los usuarios subscritos
        // Un api que registra un usuario en el portal - para darle acceso al administrador
        

        [WebMethod]
        public bool GetCategories(string token, ref List<Categories> categories, ref string Err)
        {
            Err = "";
            try
            {
                if(token != tokenInt) { throw new UnauthorizedAccessException(); }
                categories = NewsExtensions.GetCategories_INT(ref Err);
                if (!string.IsNullOrEmpty(Err)) { throw new Exception(Err); }
                
                return true;
            }
            catch (Exception ex)
            {
                Err = $"{NombreFuncion()} Error : {ex.Message}";
                return false;
            }
            
        }

        [WebMethod]
        public bool InsertNews(string token, List<Article> noticias, int catID, int subcatID, ref string Err)
        {
            Err = "";
            try
            {
                if (token != tokenInt) { throw new UnauthorizedAccessException(); }

                foreach (var noti in noticias)
                {
                    NewsExtensions.InsertNew_INT(noti.title,noti.description, noti.content, noti.url, noti.urlToImage, noti.author, noti.source.name, noti.language, noti.country, noti.publishedAt, catID, subcatID, false, ref Err);
                }

                if (!string.IsNullOrEmpty(Err)) { throw new Exception(Err); }

                return true;
            }
            catch (Exception ex)
            {
                Err = $"{NombreFuncion()} Error : {ex.Message}";
                return false;
            }

        }


        static string NombreFuncion(){ return  MethodBase.GetCurrentMethod().Name; }


        #region Base de la api
        //public bool GetCategories(string token, ref List<Categories> categories, ref string Err)
        //{
        //    Err = "";
        //    try
        //    {
        //        if (token != tokenInt) { throw new UnauthorizedAccessException(); }


        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Err = $"{NombreFuncion()} Error : {ex.Message}";
        //        return false;
        //    }

        //}
        #endregion
    }
}
