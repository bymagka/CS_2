using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 * 
 * 1. Построить три класса (базовый и 2 потомка), описывающих некоторых работников с почасовой оплатой (один из потомков) и фиксированной оплатой (второй потомок).
а) Описать в базовом классе абстрактный метод для расчёта среднемесячной заработной платы. Для «повременщиков» формула для расчета такова: «среднемесячная заработная плата = 20.8 * 8 * почасовая ставка», для работников с фиксированной оплатой «среднемесячная заработная плата = фиксированная месячная оплата».
б) Создать на базе абстрактного класса массив сотрудников и заполнить его.
в) *Реализовать интерфейсы для возможности сортировки массива, используя Array.Sort().
г) *Создать класс, содержащий массив сотрудников, и реализовать возможность вывода данных с использованием foreach.
 * 
 * 
 * 
 */
namespace Employees
{
    class Program
    {
        static void Main(string[] args)
        {

            Employee[] empArray =
            {
                new Manager("Alex","Tinin",10000),
                new Worker("Michael","Tinin",800),
                new Manager("Ivan","Ivanov",20000),
                new Worker("Petr","Petrov",40000)
            };

            Array.Sort(empArray);

            foreach(var item in empArray)
            {
                Console.WriteLine(item);
            }


            EmployeesList empList = new EmployeesList(3);

            empList.Add(new Manager("Alex", "Tinin", 10000));
            empList.Add(new Worker("Michael", "Tinin", 800));
            empList.Add(new Manager("Ivan", "Ivanov", 20000));

            foreach(Employee item in empList)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("-----------");

            EmployeesListYield empList2 = new EmployeesListYield(3);

            empList2.Add(new Manager("Alex", "Tinin", 10000));
            empList2.Add(new Worker("Michael", "Tinin", 800));
            empList2.Add(new Manager("Ivan", "Ivanov", 20000));

            foreach (Employee item in empList2)
            {
                Console.WriteLine(item);
            }

            Console.ReadLine();
        }
    }

    //абстрактный класс сотрудники
    public abstract class Employee : IComparer,IComparable
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public double SalaryRate { get; set; }

        public Employee(string Name,string Surname,double SalaryRate)
        {
            this.Name = Name;
            this.Surname = Surname;
            this.SalaryRate = SalaryRate;
        }

        public int Compare(object x, object y)
        {
            if ((x as Employee).SalaryRate == (y as Employee).SalaryRate) return 0;
            else if((x as Employee).SalaryRate > (y as Employee).SalaryRate) return 1;
            else return -1;
        }

        public  int CompareTo(object obj)
        {
            if (this.SalaryRate == (obj as Employee).SalaryRate) return 0;
            else if (this.SalaryRate > (obj as Employee).SalaryRate) return 1;
            else return -1;
        }

        public abstract double GetSalary();

        public override string ToString()
        {
            return $"{Name} {Surname} {SalaryRate}";
        }

    }

    public class Manager : Employee
    {

        public Manager(string Name, string Surname, double SalaryRate) : base(Name, Surname, SalaryRate)
        {

        }

        public override double GetSalary()
        {
            
            return 20.8 * this.SalaryRate * 8;
        }
    }

    public class Worker : Employee
    {

        public Worker(string Name, string Surname, double SalaryRate) : base(Name, Surname, SalaryRate)
        {

        }

        public override double GetSalary()
        {
            return this.SalaryRate;
        }
    }

    public class EmployeesList : IEnumerable, IEnumerator
    {
        Employee[] empList;

        int iterationNumber = -1;
        int currentPosition = -1;

        public EmployeesList(int length)
        {
            empList = new Employee[length];
        }

        public void Add(Employee emp)
        {
            if (currentPosition > empList.Length - 2) throw new IndexOutOfRangeException();
            currentPosition++;
            empList[currentPosition] = emp;
        }

        public object Current => empList[iterationNumber];

        public IEnumerator GetEnumerator()
        {
            return this;
        }

        public bool MoveNext()
        {
            if(iterationNumber == empList.Length - 1)
            {
                Reset();
                return false;
            }

            iterationNumber++;
            return true;
        }

        public void Reset()
        {
            iterationNumber = -1;
        }
    }


    public class EmployeesListYield : IEnumerable
    {
        Employee[] empList;

        int iterationNumber = -1;
        int currentPosition = -1;

        public EmployeesListYield(int length)
        {
            empList = new Employee[length];
        }

        public void Add(Employee emp)
        {
            if (currentPosition > empList.Length - 2) throw new IndexOutOfRangeException();
            currentPosition++;
            empList[currentPosition] = emp;
        }

        public IEnumerator GetEnumerator()
        {
            for(int i  = 0; i < empList.Length; i++)
            {
                yield return empList[i];
            }
        }

        public bool MoveNext()
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }

}
