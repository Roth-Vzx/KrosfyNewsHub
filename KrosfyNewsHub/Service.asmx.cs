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

        #region variables internas
        string tokenInt = "KrosfyNH2023$.AR";
        #endregion


        // Meteremos a disposicion api's que permiten enviar las ultimas noticias del dia
        // Una api que se encarga de gestionar o mandar email a los usuarios subscritos
        // Un api que registra un usuario en el portal - para darle acceso al administrador
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

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
