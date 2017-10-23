using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BibliotecaMVC.Data;
using BibliotecaMVC.Models;
using BibliotecaMVC.Utils;

namespace BibliotecaMVC.Controllers
{
    public class EmprestimosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmprestimosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Emprestimos
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Emprestimo.Include(e => e.Usuario);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Emprestimos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emprestimo = await _context.Emprestimo
                                    .Include(l => l.LivroEmprestimo)
                                    .ThenInclude(li => li.Livro)
                                    .SingleOrDefaultAsync(m => m.EmprestimoID == id);
            if (emprestimo == null)
            {
                return NotFound();
            }

            return View(emprestimo);
        }

        // GET: Emprestimos/Create
        public IActionResult Create()
        {
            ViewData["UsuarioID"] = new SelectList(_context.Usuario, "UsuarioID", "Email");
            ViewBag.Livros = new Listagens(_context).LivrosCheckBox();
            return View();
        }

        // POST: Emprestimos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmprestimoID,DataDevolucao,DataFim,DataInicio,UsuarioID")] Emprestimo emprestimo, string[] selectedLivros)
        {
            if (ModelState.IsValid)
            {
                if (selectedLivros != null)
                {
                    emprestimo.LivroEmprestimo = new List<LivroEmprestimo>();

                    foreach (var idLivro in selectedLivros)
                        emprestimo.LivroEmprestimo.Add(new LivroEmprestimo() { LivroID = int.Parse(idLivro), Emprestimo = emprestimo });
                }

                _context.Add(emprestimo);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["UsuarioID"] = new SelectList(_context.Usuario, "UsuarioID", "Email", emprestimo.UsuarioID);
            return View(emprestimo);
        }

        // GET: Emprestimos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livrosAux = new Listagens(_context).LivrosCheckBox();

            var emprestimo = await _context.Emprestimo.Include(l => l.LivroEmprestimo)
                .SingleOrDefaultAsync(m => m.EmprestimoID == id);

            livrosAux.ForEach(a =>
                        a.Checked = emprestimo.LivroEmprestimo.Any(l => l.LivroID == a.Value)
                  );

            ViewBag.Livros = livrosAux;

            if (emprestimo == null)
            {
                return NotFound();
            }
            ViewData["UsuarioID"] = new SelectList(_context.Usuario, "UsuarioID", "Email", emprestimo.UsuarioID);
            return View(emprestimo);
        }

        // POST: Emprestimos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmprestimoID,DataDevolucao,DataFim,DataInicio,UsuarioID")] Emprestimo emprestimo, string[] selectedLivros)
        {
            if (id != emprestimo.EmprestimoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var livroEmprestimos = _context.LivroEmprestimo.AsNoTracking().Where(le => le.EmprestimoID == emprestimo.EmprestimoID);

                    _context.LivroEmprestimo.RemoveRange(livroEmprestimos);
                    await _context.SaveChangesAsync();

                    if (selectedLivros != null)
                    {
                        emprestimo.LivroEmprestimo = new List<LivroEmprestimo>();

                        foreach (var idLivro in selectedLivros)
                            emprestimo.LivroEmprestimo.Add(new LivroEmprestimo() { LivroID = int.Parse(idLivro), Emprestimo = emprestimo });
                    }

                    _context.Update(emprestimo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmprestimoExists(emprestimo.EmprestimoID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["UsuarioID"] = new SelectList(_context.Usuario, "UsuarioID", "Email", emprestimo.UsuarioID);
            return View(emprestimo);
        }

        // GET: Emprestimos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emprestimo = await _context.Emprestimo.SingleOrDefaultAsync(m => m.EmprestimoID == id);
            if (emprestimo == null)
            {
                return NotFound();
            }

            return View(emprestimo);
        }

        // POST: Emprestimos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var emprestimo = await _context.Emprestimo.SingleOrDefaultAsync(m => m.EmprestimoID == id);
            _context.Emprestimo.Remove(emprestimo);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool EmprestimoExists(int id)
        {
            return _context.Emprestimo.Any(e => e.EmprestimoID == id);
        }
    }
}
