using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestIndore.Models
{
    public class Product
    {
		public int ProductID { get; set; }
		public string ProductCode {get;set;}
		public string ProductName {get;set;}
		public string ProductImage{get;set;}
		public decimal Unit {get;set;}
		public decimal Rate {get;set;}
		public string Description { get; set;}
		public DateTime CreatedDate { get; set; }
		public int CreatedBy { get; set; }
		public DateTime UpdatedDate { get; set; }
		public int UpdatedBy { get; set; }
		public string FileName { get; set; }
		public byte[] Data { get; set; }
		public string ContentType { get; set; }
		public HttpPostedFileBase File { get; set; }

	}
	public class Image
	{
		public int Id { get; set; }
		public string FileName { get; set; }
		public byte[] Data { get; set; }
		public string ContentType { get; set; }
	}
}