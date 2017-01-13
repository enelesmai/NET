using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Services.Models;
using System.Text;
using System.Net.Http.Headers;
using System.ComponentModel;

namespace Services.Controllers
{
    public class ArticleViewModel
    {
        [DisplayName("store")]
        public int SelectedStoreId { get; set; }
        public IEnumerable<SelectListItem> Stores { get; set; }
        public Article article { get; set; }
    }

    public class HomeController : Controller
    {
        private ManagerContext db = new ManagerContext();
        private HttpClient client = new HttpClient();

        private IEnumerable<SelectListItem> GetStores()
        {
            var stores = db.Stores.ToList();
            return new SelectList(stores, "id", "name");
        }

        //
        // GET: /Home/

        public ActionResult Index()
        {
            client = new HttpClient();

            string authInfo = "my_user:my_password";
            authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
            
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authInfo);
            client.BaseAddress = new Uri("http://localhost:5130/");

            HttpResponseMessage response = client.GetAsync("api/articles").Result;
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body. Blocking!
                var data = response.Content.ReadAsAsync<ArticlesApiCollection>().Result;
                return View(data.articles.ToList());
            }
            return View(new List<article>());
        }

        //
        // GET: /Home/Details/5

        public ActionResult Details(int id = 0)
        {
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        //
        // GET: /Home/Create

        public ActionResult Create()
        {
            var model = new ArticleViewModel
            {
                Stores = GetStores(),
                article = new Article()
            };
            return View(model);
        }

        //
        // POST: /Home/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Article article, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                int store_id = Convert.ToInt32(form["SelectedStoreId"]);

                //save the article to the store
                Store store = db.Stores.SingleOrDefault(s => s.id == store_id);
                store.Articles.Add(article);

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(article);
        }

        //
        // GET: /Home/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Article myArticle = db.Articles.Find(id);
            if (myArticle == null)
            {
                return HttpNotFound();
            }
            var model = new ArticleViewModel
            {
                SelectedStoreId = myArticle.Store != null ? myArticle.Store.id: 0,
                Stores = GetStores(),
                article = myArticle
            };
            return View(model);
        }

        //
        // POST: /Home/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Article article, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                //get the previous store and remove from there
                int store_id = Convert.ToInt32(form["SelectedStoreId"].Split(',')[0]);
                int new_store_id = Convert.ToInt32(form["SelectedStoreId"].Split(',')[1]);

                if (store_id == new_store_id)
                {
                    //update article
                    db.Entry(article).State = EntityState.Modified;
                }
                else {
                    //remove from previous store
                    Article tmp = db.Articles.Find(article.id);
                    db.Articles.Remove(tmp);
                    db.SaveChanges();
                    
                    //add the article to the new store
                    Store store = db.Stores.SingleOrDefault(s => s.id == new_store_id);
                    store.Articles.Add(article);
                }
                db.SaveChanges();
               
                return RedirectToAction("Index");
            }
            return View(article);
        }

        //
        // GET: /Home/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        //
        // POST: /Home/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Article article = db.Articles.Find(id);
            db.Articles.Remove(article);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}