using System.Collections.Generic;

namespace CollectionUtilities
{
    public class ToggleableCollectionSelector<T>
    {
        private List<T> _selectedElements = new List<T>();
        private List<T> _unnselectedElements;

        public ToggleableCollectionSelector(List<T> initialCollection)
        {
            _unnselectedElements = initialCollection;
        }


        public IEnumerable<T> GetSelectedElements()
        {
            return _selectedElements;
        }
        
        public IEnumerable<T> GetUnselectedElements()
        {
            return _unnselectedElements;
        }
        
        public IEnumerable<T> GetAllElements()
        {
            var allInList = new List<T>();
            allInList.AddRange(_unnselectedElements);
            allInList.AddRange(_selectedElements);
            return allInList;
        }

        public void ToggleElement(T selectElement)
        {
            bool isSelected = false;
            if (_selectedElements.Contains(selectElement))
            {
                _selectedElements.Remove(selectElement);
                _unnselectedElements.Add(selectElement);
            }
            else
            {
                isSelected = true;
                _selectedElements.Add(selectElement);
                _unnselectedElements.Remove(selectElement);
            }
        }
    }
}