using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using System.Linq;
using NorthCarolinaTaxRecoveryCalculator.Misc;

namespace NorthCarolinaTaxRecoveryCalculator.Models.Data
{
    public class Smurf : ITable
    {
    }

    public class Hobbit : ITable
    {
    }

    public abstract class ITable
    {
        public Guid ID { get; set; }
    }

    public interface IRepository<T> where T : ITable
    {
        void Save(T t);
        T Get();
        IEnumerable<T> GetAll();
        void Delete(T t);
    }

    public class HobbitThing : IRepository<Hobbit>
    {
        public void Save(Hobbit t)
        {
            
            throw new NotImplementedException();
        }

        public Hobbit Get()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Hobbit> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Delete(Hobbit t)
        {
            throw new NotImplementedException();
        }
    }

    public class SmurfThing : IRepository<Smurf>
    {
        public void Save(Smurf t)
        {
            throw new NotImplementedException();
        }

        public Smurf Get()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Smurf> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Delete(Smurf t)
        {
            throw new NotImplementedException();
        }
    }
}