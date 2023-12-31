﻿using BrideyApp.Data;
using BrideyApp.Models;
using BrideyApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BrideyApp.Services
{
    public class BlogService:IBlogService
    {
        private readonly AppDbContext _context;

        public BlogService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<int> GetCountAsync() => await _context.Blogs.CountAsync();
        public async Task<List<Blog>> GetPaginatedDatas(int page, int take)
        {
            return await _context.Blogs.Include(m => m.Author)
                                       .Skip((page * take) - take).Take(take).ToListAsync();
        }
        public async Task<List<Blog>> GetAll() => await _context.Blogs .Include(m => m.Author)
                                                                         .ToListAsync();
        public async Task<Blog> GetById(int? id) => await _context.Blogs.Include(m => m.Author)
                                                                         .Include(m => m.BlogComments)
                                                                         .FirstOrDefaultAsync(m => m.Id == id);

        public List<Blog> GetRelatedBlogs()
        {
            return _context.Blogs.Include(p => p.Author).OrderByDescending(p => p.Author.Name).ToList();

        }
        public async Task<BlogComment> GetCommentById(int? id)
        {
            return await _context.BlogComments.FindAsync(id);
        }

        public async Task<BlogComment> GetCommentByIdWithBlog(int? id)
        {
            return await _context.BlogComments.Include(b => b.Blog).FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<List<BlogComment>> GetComments()
        {
            return await _context.BlogComments.Include(b => b.Blog).ToListAsync();
        }


    }
}
