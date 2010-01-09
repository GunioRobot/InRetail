using InRetail.EntityPresentation;
using Tests.InRetail.Procurement.EntityPresentation;

namespace Tests.InRetail.Procurement
{
    public class PersonModel : EntityBase, IModel
    {
        private string _firstName;
        private string _lastName;
        private string _address;
        private string _city;

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value; NotifyPropertyChanged(this.Property(x => x.FirstName));
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                NotifyPropertyChanged(this.Property(x => x.LastName));
            }
        }

        public string Address
        {
            get { return _address; }
            set { _address = value; NotifyPropertyChanged(this.Property(x => x.Address)); }
        }

        public string City
        {
            get { return _city; }
            set { _city = value; NotifyPropertyChanged(this.Property(x => x.City)); }
        }

        private int _age;
        public int Age
        {
            get { return _age; }
            set { _age = value; NotifyPropertyChanged(this.Property(x => x.Age)); }
        }
    }
}