using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Simpress;

namespace Simpress.Controllers
{
    public class ProdutoController : Controller
    {
        private PRODUTOSTOREEntities db = new PRODUTOSTOREEntities();

        // GET: Produto
        public ActionResult Index()
        {
            var tblProduto = db.tblProduto.Include(t => t.tblCategoriaProduto);
            return View(tblProduto.ToList());
        }

        // GET: Produto/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProduto tblProduto = db.tblProduto.Find(id);
            if (tblProduto == null)
            {
                return HttpNotFound();
            }
            return View(tblProduto);
        }

        public tblProduto MapearObj(string Nome, string Descricao, int CategoriaId, int Perecivel, int Ativo)
        {
            bool _ativo = Ativo == 1 ? true : false;
            bool _perecivel = Perecivel == 1 ? true : false;

            tblProduto tblProduto = new tblProduto();
            tblProduto.Nome = Nome;
            tblProduto.Descricao = Descricao;
            tblProduto.CategoriaID = CategoriaId;
            tblProduto.Ativo = _ativo;
            tblProduto.Perecivel = _perecivel;

            return tblProduto;
        }


        public ActionResult Create(string Nome, string Descricao, int CategoriaId, int Perecivel, int Ativo = 1)
        {
            tblProduto tblProduto = MapearObj(Nome, Descricao, CategoriaId, Perecivel, Ativo);
            if (ModelState.IsValid)
            {            
                db.tblProduto.Add(tblProduto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id = new SelectList(db.tblCategoriaProduto, "Id", "Nome", tblProduto.Id);
            return View(tblProduto);
        }

        public ActionResult _partialGrid()
        {
            ViewBag.Categorias = db.tblCategoriaProduto.ToList();
            return PartialView();
        }

        // GET: Produto/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProduto tblProduto = db.tblProduto.Find(id);
            if (tblProduto == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.tblCategoriaProduto, "Id", "Nome", tblProduto.Id);
            return View(tblProduto);
        }

        // POST: Produto/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,Descricao,Ativo,Perecivel,CategoriaID")] tblProduto tblProduto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblProduto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.tblCategoriaProduto, "Id", "Nome", tblProduto.Id);
            return View(tblProduto);
        }

        // GET: Produto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProduto tblProduto = db.tblProduto.Find(id);
            if (tblProduto == null)
            {
                return HttpNotFound();
            }
            return View(tblProduto);
        }


        public ActionResult DeleteConfirmed(int id)
        {
            tblProduto tblProduto = db.tblProduto.Find(id);
            db.tblProduto.Remove(tblProduto);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
