using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

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
        // Meteremos a disposicion api's que permiten enviar las ultimas noticias del dia
        // Una api que se encarga de gestionar o mandar email a los usuarios subscritos
        // Un api que registra un usuario en el portal - para darle acceso al administrador
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
    }
}
