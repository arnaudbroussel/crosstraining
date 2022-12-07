using System;
using System.Collections.Generic;
using System.Text;

namespace crosstraining.hierarchy {
    public interface IRepository<T> where T : IEntity {
        T FindById(int id);
        IEnumerable<T> All();
    }
}
