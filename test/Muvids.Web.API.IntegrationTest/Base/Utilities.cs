﻿using Muvids.Persistence;
using System;

namespace Muvids.Web.API.IntegrationTest.Base;

public class Utilities
{
    public static void InitializeDbForTests(MuvidsDbContext context)
    {
        context.Movies.Add(new()
        {
            Id = Guid.NewGuid(),
            Title = "Inception",
            ReleaseYear = 2010,
            Rating = "PG-13",
            Description = "A thief who steals corporate secrets through the use of dream-sharing technology is given the inverse task of planting an idea into the mind of a C.E.O., but his tragic past may doom the project and his team to disaster.",
            IsPublic = true,
            CreatedBy = "00000000-0000-0000-0000-000000000000",
            CreatedDate = DateTime.Now,
            LastModifiedBy = "00000000-0000-0000-0000-000000000000",
            LastModifiedDate = DateTime.Now
        });


        context.SaveChanges();
    }



}