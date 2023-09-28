﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TechfiedSolution.Data;
using TechfiedSolution.Entities;

namespace TechfiedSolution.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class ContentController : Controller
{
    private readonly ApplicationDbContext _context;

    public ContentController(ApplicationDbContext context)
    {
        _context = context;
    }


    // GET: Admin/Content/Create
    public IActionResult Create(int categoryItemId, int categoryId)
    {
        Content content = new Content
        {
            CategoryId = categoryId,
            CatItemId = categoryItemId
        };

        return View(content);
    }

    // POST: Admin/Content/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Title,HTMLContent,VideoLink,CatItemId,CategoryId")] Content content)
    {
        if (ModelState.IsValid)
        {
            content.CategoryItem = await _context.CategoryItem.FindAsync(content.CatItemId);
            _context.Add(content);
            await _context.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index),"CategoryItem", new {categoryId=content.CategoryId });
        }
        return View(content);
    }

    // GET: Admin/Content/Edit/5
    public async Task<IActionResult> Edit(int categoryItemId, int categoryId)
    {
        if (categoryItemId == 0)
        {
            return NotFound();
        }

        var content = await _context.Content.SingleOrDefaultAsync(item => item.CategoryItem.Id == categoryItemId);

        content.CategoryId = categoryId;
        
        if (content == null)
        {
            return NotFound();
        }
        return View(content);
    }

    // POST: Admin/Content/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Title,HTMLContent,VideoLink,CategoryId")] Content content)
    {
        if (id != content.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(content);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContentExists(content.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index),"CategoryItem", new { categoryId=content.CategoryId });
        }
        return View(content);
    }

    private bool ContentExists(int id)
    {
        return _context.Content.Any(e => e.Id == id);
    }
}
