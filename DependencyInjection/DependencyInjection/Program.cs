using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjection
{
    class Program
    {
        static void Main(string[] args)
        {
            string personDataManagerTypeString = ConfigurationManager.AppSettings["PersonDataManagerType"];
            Type personDataManagerType = Type.GetType(string.Format("DependencyInjection.{0}", personDataManagerTypeString), true);
            
            IPersonDataManager personDataManager = Activator.CreateInstance(personDataManagerType) as IPersonDataManager;

            //create the person class
            Person person = new Person()
            {
                FirstName = "John",
                LastName = "Doe"
            };

            //constructor Injection
            PersonManager personManager = new PersonManager(person, personDataManager);

            //Property Injection
            //PersonManager personManager = new PersonManager(person);
            //personManager.PersonDataManager = personDataManager;


            //Setter function injection
            //PersonManager personManager = new PersonManager(person);
            //personManager.SetPersonDataManager(personDataManager);

            //Interface injection
            //PersonManager personManager = new PersonManager(person);
            //personManager.SetPersonDataManagerObject(personDataManager);

            //save the person info
            personManager.SavePerson();

            Console.ReadLine();
        }
        static void SavePerson(Person person)
        {

            Console.WriteLine("firstname: {0} and LastName {1}", person.FirstName, person.LastName);
        }
    }
    class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    #region PersonDataManger
    interface IPersonDataManager
    {
        void Save(Person person);
    }

    //Provide func to actually save person data
    class PersonDataManagerTextFile : IPersonDataManager
    {
        public void Save(Person person)
        {
            Console.WriteLine("Person details are saved in text file");
        }
    }
    class PersonDataManagerXmlFile : IPersonDataManager
    {
        public void Save(Person person)
        {
            Console.WriteLine("Person details are saved in xml file");
        }
    }
    class PersonDataManagerSqlDb : IPersonDataManager
    {
        public void Save(Person person)
        {
            Console.WriteLine("Person details are saved in sql db");
        }
    }
    #endregion PersonDataManager

    interface IPersonManagerInjector
    {
        void SetPersonDataManagerObject(IPersonDataManager personDataManager);
    }

    //service to acting as a class to handle person manager
    class PersonManager //: IPersonManagerInjector
    {
        private Person _person;
        private IPersonDataManager _personDataManager;

        #region Constructor Injection
        public PersonManager(Person person, IPersonDataManager personDataManager)
        {
            _person = person;
            _personDataManager = personDataManager;
        }
        #endregion Constructor Injection
        #region Property Injection
        //Property Injection
        //public IPersonDataManager PersonDataManager
        //{
        //    get { return _personDataManager; }
        //    set { _personDataManager = value; }
        //}
        //public PersonManager(Person person)
        //{
        //    _person = person;
        //}
        #endregion Property Injection

        #region SetterFunctionInjection
        //public void SetPersonDataManager(IPersonDataManager personDataManager)
        //{
        //    _personDataManager = personDataManager;
        //}
        //public PersonManager(Person person)
        //{
        //    _person = person;
        //}
        #endregion SetterFunctionInjection

        #region InterfaceInjection
        //public void SetPersonDataManagerObject(IPersonDataManager personDataManager)
        //{
        //    _personDataManager = personDataManager;
        //}
        //public PersonManager(Person person)
        //{
        //    _person = person;
        //}
        #endregion InterfaceInjection
        public void SavePerson()
        {
            _personDataManager.Save(_person);
        }
    }
}
