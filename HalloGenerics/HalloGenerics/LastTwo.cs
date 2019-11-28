using System.Collections.Generic;

namespace HalloGenerics
{
    public class LastTwo<T>
    {
        private T one;
        private T two;

        public void Add(T ding)
        {
            one = two;
            two = ding;
        }

        public IEnumerable<T> GetAll()
        {
            yield return one;
            yield return two;
        }
    }
}
