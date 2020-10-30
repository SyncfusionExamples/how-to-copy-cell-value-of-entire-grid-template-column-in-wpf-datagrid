using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selection_Copy
{
    public class DataItem : INotifyPropertyChanged
    {
        private int itemshortname;
        public int ItemShortName
        {
            get
            {
                return itemshortname;
            }
            set
            {
                itemshortname = value;
                OnPropertyChanged("ItemShortName");
            }
        }

        private int itemtype;
        public int ItemType
        {
            get
            {
                return itemtype;
            }
            set
            {
                itemtype = value;
                OnPropertyChanged("ItemType");
            }
        }

        private int stringValue;

        public int StringValue
        {
            get
            {
                return stringValue;
            }
            set
            {
                stringValue = value;
                OnPropertyChanged("StringValue");
            }
        }




        private DateTime dateTimevalue;

        public DateTime DateTimeValue
        {
            get
            {
                return dateTimevalue;
            }
            set
            {
                dateTimevalue = value;
                OnPropertyChanged("DateTimeValue");
            }
        }
        private int? _value;
        public int? Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                OnPropertyChanged("Value");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class DataItemDetails : ObservableCollection<DataItem>
    {
        public DataItemDetails()
        {
            PopulateCollection();
        }

        private void PopulateCollection()
        {
            Clear();

            for (int i = 1; i <= 1000; i++)
            {
                Add(new DataItem { ItemShortName = i, ItemType = 1, StringValue = 2 });
            }
        }
    }
}
