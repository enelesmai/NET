using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Services.Models;
using WebApiBasicAuth.Filters;

namespace Services.Controllers
{
    
    public class ArticlesController : ApiController
    {
        private ManagerContext db = new ManagerContext();

        // GET api/articles
        [IdentityBasicAuthentication]
        [Authorize]
        public ArticlesApiCollection Get()
        {
            ArticlesApiCollection result = new ArticlesApiCollection();
            try {
                
                result.articles = responseArticle(db.Articles.ToList()).ToArray();
                result.success = true;
                result.total_elements = db.Articles.Count();
            }
            catch (Exception excp) {
                result.success = false;
                result.error_code = 0;
                result.error_msg = excp.Message;
            }
            return result;
        }

        // GET api/articles/stores/1
        [IdentityBasicAuthentication]
        [Authorize]
        [HttpGet]
        public ArticlesApiCollection stores(string id) {
            var result = new ArticlesApiCollection();
            result.success = false;
            try
            {
                int n;
                bool isNumeric = int.TryParse(id, out n);

                if (isNumeric)
                {
                    Store store = db.Stores.SingleOrDefault(s => s.id == n);
                    if (store != null)
                    {
                        List<Article> articles = db.Articles.Where(a => a.Store.id == n).ToList();
                        result.articles = responseArticle(articles).ToArray();
                        result.success = true;
                        result.total_elements = articles.Count();
                    }
                    else {
                        result.error_code = 404;
                        result.error_msg = ServiceError.Response[404];
                    }
                }
                else {
                    result.error_code = 400;
                    result.error_msg = ServiceError.Response[400];
                }
            }
            catch (Exception excp) {
                result.success = false;
                result.error_code = 500;
                result.error_msg = result.error_msg = ServiceError.Response[500];
            }
            return result;
        }

        // GET api/articles/5
        public Article Get(int id)
        {
            return db.Articles.SingleOrDefault(a => a.id == id);
        }

        // POST api/articles
        public void Post([FromBody]Article obj)
        {
            db.Articles.Add(obj);
            db.SaveChanges();
        }

        // PUT api/articles/5
        public void Put(int id, [FromBody]Article obj)
        {
            db.Entry(obj).State = EntityState.Modified;
            db.SaveChanges();
        }

        // DELETE api/articles/5
        public void Delete(int id)
        {
            Article article = db.Articles.Find(id);
            db.Articles.Remove(article);
            db.SaveChanges();
        }

        private List<article> responseArticle(List<Article> articles)
        {
            List<article> pArticles = new List<article>();
            foreach (Article art in articles)
            {
                article ca = new article();
                ca.id = art.id;
                ca.name = art.name;
                ca.description = art.description;
                ca.price = art.price;
                ca.total_in_shelf = art.total_in_shelf;
                ca.total_in_vault = art.total_in_vault;
                ca.store_name = art.Store!=null ? art.Store.name : "";
                pArticles.Add(ca);
            }
            return pArticles;
        }
    }
}
