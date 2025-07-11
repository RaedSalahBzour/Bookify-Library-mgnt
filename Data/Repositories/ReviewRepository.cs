﻿using Data.Entities;
using Data.Helpers;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class ReviewRepository(ApplicationDbContext context)
    : GenericRepository<Review>(context), IReviewRepository
{
    public async Task<(bool userExists, bool bookExists)> CheckUserAndBookExistAsync(string userId,
        string bookId)
    {
        bool userExists = await _context.Users.AnyAsync(u => u.Id == userId);
        bool bookExists = await _context.Books.AnyAsync(b => b.Id == bookId);
        return (userExists, bookExists);
    }

}
