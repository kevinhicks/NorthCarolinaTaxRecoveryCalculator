using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NorthCarolinaTaxRecoveryCalculator.Models.Service
{
    /// <summary>
    /// This is used simply for nameing consistancy. Nothing shoudl ever try to pass anything by this type
    /// </summary>
    /// <typeparam name="T">The Type of object we wwant to work with</typeparam>
    /// <typeparam name="Key">The type of the key that is used to identify the object</typeparam>
    public interface IBaseRepository<T, Key>
    {
        T FindByID(Key ID);
        void Update(T obj);
        void Create(T obj);
        void Delete(T obj);
    }
}
