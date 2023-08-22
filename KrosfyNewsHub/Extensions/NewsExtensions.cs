﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using static KrosfyNewsHub.Service;
using DBHelper;
using System.Reflection;

namespace KrosfyNewsHub.Extensions
{
    public static class NewsExtensions
    {
        public static bool isRequestFromMobile(string userAgent)
        {
            string[] mobileKeywords = { "Mobile", "Android", "iPhone", "Windows Phone", "BlackBerry" };

            foreach (string keyword in mobileKeywords)
            {
                if (userAgent.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    return true;
                }
            }
            return false;
        }

        public static string GetSubCategoryName(int subcategoryID)
        {
            string SubCategoryName = "";

            switch (subcategoryID)
            {
                case 4: SubCategoryName = "Bitcoin"; break;
                case 5: SubCategoryName = "Ethereum"; break;
                case 6: SubCategoryName = "Tether"; break;
                case 7: SubCategoryName = "Cardano"; break;
                case 8: SubCategoryName = Resources.Resources.Menu_IA; break;
                case 9: SubCategoryName = Resources.Resources.Menu_Gaming; break;
                case 10: SubCategoryName = "USD/EUR"; break;
                default:
                    break;
            }
            return SubCategoryName;

        }

        public static string GetCategoryName(int categoryID)
        {
            string CategoryName = "";

            switch (categoryID)
            {
                case 1: CategoryName = Resources.Resources.Menu_Crypto; break;
                case 2: CategoryName = Resources.Resources.Menu_Forex; break;
                case 3: CategoryName = Resources.Resources.Menu_Technology; break;
                
                default:
                    break;
            }
            return CategoryName;

        }

        public static List<Categories> GetCategories_INT(ref string Err)
        {
            try
            {
                List<Categories> categories = new List<Categories>();

                using (DB DB = new DB())
                {
                    if (DB.Open())
                    {
                        string Sql = "SELECT sub.ParentCategoryID AS CategoryID, c.NombreCategoria AS CategoryName," + Environment.NewLine +
                             "  sub.ID AS SubcategoryID, sub.NombreCategoria AS SubCategoryName, sub.searching AS searchingTEXT " + Environment.NewLine +
                             " FROM Category sub " + Environment.NewLine +
                             " INNER JOIN Category C ON c.ID = sub.ParentCategoryID " + Environment.NewLine +
                             " WHERE ISNULL(sub.parentCategoryID, 0) <> 0 AND C.Enable = 1 " + Environment.NewLine +
                             " ORDER BY CategoryID,SubcategoryID ASC";

                        DataSet ds = DB.Execute(Sql);
                        if (ds == null) { throw DB.GetException(); }

                        DataTableReader dr = ds.CreateDataReader();
                        ds = null;
                        while (dr.Read())
                        {
                            Subcategory subcategoria = new Subcategory();
                            subcategoria.ID = Convert.ToInt32(dr["SubcategoryID"]);
                            subcategoria.name = dr["SubCategoryName"].ToString();
                            subcategoria.searchText = dr["searchingTEXT"].ToString();

                            var categoria = categories.Where(x => x.ID == Convert.ToInt32(dr["CategoryID"])).FirstOrDefault();
                            if (categoria != null)
                            {
                                categoria.subcategories.Add(subcategoria);
                            }
                            else
                            {
                                Categories cat = new Categories();
                                cat.ID = Convert.ToInt32(dr["CategoryID"]);
                                cat.name = dr["CategoryName"].ToString();
                                cat.subcategories.Add(subcategoria);
                                categories.Add(cat);
                            }
                        }

                        dr = null;
                    }
                    else
                    {
                        throw DB.GetException();
                    }
                }
                return categories;

            }
            catch (Exception ex)
            {
                Err = $"{NombreFuncion()} Error : {ex.Message}";
                return new List<Categories>();

            }
        }

        static string NombreFuncion() { return MethodBase.GetCurrentMethod().Name; }

    }
}