using System;
using System.Collections.Generic;
using NHibernate.Criterion;

namespace Wise.Framework.DataAccessLayer
{
    public interface IRepository 
    {
        /// <summary>
        ///     Performs Save or Update on object
        /// </summary>
        /// <param name="obj">object to save</param>
        void Save<T>(T obj) where T : EntityBase;

        /// <summary>
        ///     Performs remove action - delete from repository
        /// </summary>
        /// <param name="obj">object to delete</param>
        void Delete<T>(T obj) where T : EntityBase;

        /// <summary>
        ///     used to retrieve all data of specific entity type
        /// </summary>
        /// <returns>collection of entities</returns>
        IEnumerable<T> GetAll<T>() where T : EntityBase;

        /// <summary>
        ///     used to retrieve single entity by they id - which is primary key also.
        /// </summary>
        /// <param name="id">primary key object</param>
        /// <returns>entity</returns>
        T GetById<T>(object id) where T : EntityBase;


        IEnumerable<T> GetBySearchCriteria<T>(ICriterion searchCriteria) where T : EntityBase;
    }
}
