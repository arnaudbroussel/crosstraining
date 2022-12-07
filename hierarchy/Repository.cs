using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace crosstraining.hierarchy {
    public class Repository<T> : IRepository<T> where T : IEntity {
        protected IEnumerable<T> _elements;
        public Repository(IEnumerable<T> elements) {
            _elements = elements;
        }
        public T FindById(int id) {
            return _elements.SingleOrDefault(e => e.Id == id);
        }
        public IEnumerable<T> All() {
            return _elements;
        }
    }
}
