using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QnAFitProject.Repository
{
    interface IAnswerRep<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(object obj);
        void Insert(T obj);
        void Update(T obj);
        void Delete(object Id);
        void Save();
    }
}
