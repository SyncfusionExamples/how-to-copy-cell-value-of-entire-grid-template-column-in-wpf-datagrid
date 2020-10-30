using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selection_Copy
{
    public class SampleViewModel
    {

        public List<StringNameValuePair> NameValuePair { get; set; }
        public List<StringNameValuePair1> NameValuePair1 { get; set; }
        public SampleViewModel()
        {
            NameValuePair = new List<StringNameValuePair>();
            GridDataSource = new DataItemDetails();
            NameValuePair.Add(new StringNameValuePair(0, "Apple"));
            NameValuePair.Add(new StringNameValuePair(1, "Banana"));
            NameValuePair.Add(new StringNameValuePair(2, "Carrot"));

            NameValuePair1 = new List<StringNameValuePair1>();
            NameValuePair1.Add(new StringNameValuePair1(0, "Peer"));
            NameValuePair1.Add(new StringNameValuePair1(1, "Banana"));
            NameValuePair1.Add(new StringNameValuePair1(2, "Mango"));
        }

        public ObservableCollection<DataItem> GridDataSource { get; set; }
    }

    public class SampleViewModel1
    {

        public List<StringNameValuePair1> NameValuePair1 { get; set; }
        public SampleViewModel1()
        {
            NameValuePair1 = new List<StringNameValuePair1>();
            NameValuePair1.Add(new StringNameValuePair1(0, "Apple"));
            NameValuePair1.Add(new StringNameValuePair1(1, "Banana"));
            NameValuePair1.Add(new StringNameValuePair1(2, "Carrot"));
        }

    }

    public class StringNameValuePair
    {
        private int stringValue;
        private string name;

        public StringNameValuePair(int stringValue, string name)
        {
            this.stringValue = stringValue;
            this.name = name;
        }

        public int StringValue
        {
            get
            {
                return stringValue;
            }

            set
            {
                this.stringValue = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                this.name = value;
            }
        }
    }

    public class StringNameValuePair1
    {
        private int stringValue;
        private string name;

        public StringNameValuePair1(int stringValue, string name)
        {
            this.stringValue = stringValue;
            this.name = name;
        }

        public int StringValue
        {
            get
            {
                return stringValue;
            }

            set
            {
                this.stringValue = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                this.name = value;
            }
        }
    }
}
