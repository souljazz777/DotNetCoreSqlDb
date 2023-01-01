﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using DotNetCoreSqlDb.Models;

namespace DotNetCoreSqlDb.Data
{
	public class MyDatabaseContext : DbContext
	{
		public MyDatabaseContext(DbContextOptions<MyDatabaseContext> options) : base(options)
		{

		}
		public DbSet<DotNetCoreSqlDb.Models.Todo> Todo { get; set;}
	}
}

