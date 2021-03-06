﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class BookPOSEntities3 : DbContext
    {
        public BookPOSEntities3()
            : base("name=BookPOSEntities3")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<BookRent> BookRents { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<RentBookDetail> RentBookDetails { get; set; }
    
        public virtual ObjectResult<SearchBook_Result> SearchBook(string bookName, string author, Nullable<int> category)
        {
            var bookNameParameter = bookName != null ?
                new ObjectParameter("BookName", bookName) :
                new ObjectParameter("BookName", typeof(string));
    
            var authorParameter = author != null ?
                new ObjectParameter("Author", author) :
                new ObjectParameter("Author", typeof(string));
    
            var categoryParameter = category.HasValue ?
                new ObjectParameter("Category", category) :
                new ObjectParameter("Category", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SearchBook_Result>("SearchBook", bookNameParameter, authorParameter, categoryParameter);
        }
    
        public virtual ObjectResult<SearchRentBookHistory_Result> SearchRentBookHistory(Nullable<long> memberID, string bookName, Nullable<long> category, string startDate, string endDate)
        {
            var memberIDParameter = memberID.HasValue ?
                new ObjectParameter("MemberID", memberID) :
                new ObjectParameter("MemberID", typeof(long));
    
            var bookNameParameter = bookName != null ?
                new ObjectParameter("BookName", bookName) :
                new ObjectParameter("BookName", typeof(string));
    
            var categoryParameter = category.HasValue ?
                new ObjectParameter("Category", category) :
                new ObjectParameter("Category", typeof(long));
    
            var startDateParameter = startDate != null ?
                new ObjectParameter("StartDate", startDate) :
                new ObjectParameter("StartDate", typeof(string));
    
            var endDateParameter = endDate != null ?
                new ObjectParameter("EndDate", endDate) :
                new ObjectParameter("EndDate", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SearchRentBookHistory_Result>("SearchRentBookHistory", memberIDParameter, bookNameParameter, categoryParameter, startDateParameter, endDateParameter);
        }
    }
}
