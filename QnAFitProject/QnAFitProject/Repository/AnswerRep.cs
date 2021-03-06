﻿using QnAFitProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace QnAFitProject.Repository
{
    public class AnswerRep<T> : IAnswerRep<T> where T : class
    {
        private FitnessDbContext db;
        private DbSet<T> dbSet;

        public AnswerRep()
        {
            db = new FitnessDbContext();
            dbSet = db.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }

        public T GetById(object Id)
        {
            return dbSet.Find(Id);
        }

        public void Insert(T obj)
        {
            dbSet.Add(obj);
        }

        public void Update(T obj)
        {
            db.Entry(obj).State = EntityState.Modified;
        }

        public void Delete(object Id)
        {
            T getObjById = dbSet.Find(Id);
            dbSet.Remove(getObjById);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.db != null)
                {
                    this.db.Dispose();
                    this.db = null;
                }
            }
        }
    }

}