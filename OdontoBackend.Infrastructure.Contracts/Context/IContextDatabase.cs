using Npgsql;
using OdontoBackend.Infrastructure.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdontoBackend.Infrastructure.Contracts.Context
{
    public interface IContextDatabase
    {
        NpgsqlConnection GetConnection();
        void CloseConnection(NpgsqlConnection conn);
        bool ExecuteNoQuery(Packages.pkg pkg, NpgsqlCommand comando, ref string err);
        IQueryable<T> ExecuteList<T>(/*Packages.pkg pkg,*/ NpgsqlCommand comando, ref string err);
        IQueryable<T> ExecuteListWithOneClass<T>(/*Packages.pkg pkg,*/ NpgsqlCommand comando, ref string err, object classes);
        //public IQueryable<IQueryable<T>> ExecuteLists<T>(Packages.pkg pkg, NpgsqlCommand comando, ref string err);

    }
}
