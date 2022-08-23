using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using nhibernateDeneme.Models;

namespace nhibernateDeneme.Mapping;

public  class BookMap : ClassMapping<Book>
{
    public BookMap()
    {
        Id(x=>x.Id);
        Property(x=> x.Title);
        Property(x=> x.Genre);
        Property(x=> x.PageCount);
        Property(x=> x.Author);
        
        Table("book");

        /*
        Id(x => x.Id, x =>
        {
            x.Type(NHibernateUtil.Int32);
            x.Column("Id");
            x.UnsavedValue(0);
            x.Generator(Generators.Increment);
        });  

        Property(b => b.Title, x =>
        {
            x.Length(50);
            x.Type(NHibernateUtil.String);
            x.NotNullable(true);
        });
        Property(b => b.Genre, x =>
        {
            x.Length(50);
            x.Type(NHibernateUtil.String);
            x.NotNullable(true);
        });
        Property(b => b.PageCount, x =>
        {
            x.Length(50);
            x.Type(NHibernateUtil.Int32);
            x.NotNullable(true);
        });          
        Property(b => b.Author, x =>
        {
            x.Length(50);
            x.Type(NHibernateUtil.String);
            x.NotNullable(true);
        });
        */
           
        Table("Book");
    }
}