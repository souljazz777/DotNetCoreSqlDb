using System;
using System.ComponentModel.DataAnnotations;

namespace DotNetCoreSqlDb.Models
{
	public class Todo
	{
		public int Id { get; set;}
		public string Description { get; set;}

		[Display(Name = "Created Date")]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public DateTime CreateDate { get; set;}
	}
}

