using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentManagementSystem
{
    //student class
    public class Student
    {
        public required string Name { get; set; }
        public int Grade { get; set; }
        public required string Course { get; set; }

        public override string ToString()
        {
            return $"{Name} ({Course}): Grade {Grade}";
        }
    }
    //Manages a group of students, the list holds the students
    // Generic
    public class StudentGroup<T> where T : Student
    {
        private readonly List<T> students = [];

        public void AddStudent(T student)
        {
            students.Add(student);
        }

        public T GetStudent(int index)
        {
            return students[index];
        }

        public List<T> GetAllStudents()
        {
            return students;
        }

        // Calculate average
        public double AveragePerformance => students.Average(s => s.Grade);



        // Operator overloading
        public static StudentGroup<T> operator +(StudentGroup<T> group1, StudentGroup<T> group2)
        {
            var mergedGroup = new StudentGroup<T>();
            mergedGroup.students.AddRange(group1.students);
            mergedGroup.students.AddRange(group2.students);
            return mergedGroup;
        }

        
        public static bool operator >(StudentGroup<T> group1, StudentGroup<T> group2)
        {
            return group1.AveragePerformance > group2.AveragePerformance;
        }

        public static bool operator <(StudentGroup<T> group1, StudentGroup<T> group2)
        {
            return group1.AveragePerformance < group2.AveragePerformance;
        }
    }

    class Program
    {
        unsafe static void Main(string[] args)
        {
            // dummy data
            var student1 = new Student { Name = "Alice", Grade = 65, Course = "IT" };
            var student2 = new Student { Name = "Bob", Grade = 28, Course = "IT" };
            var student3 = new Student { Name = "Charlie", Grade = 92, Course = "Business" };
            var student4 = new Student { Name = "Diana", Grade = 18, Course = "Business" };

            //demo array
            Student[] students = [student1, student2, student3, student4];

            Console.WriteLine("Students:");
            foreach (var student in students)
            {
                Console.WriteLine(student);
               
            }
            Console.WriteLine(  );

            // pointers
            fixed (Student* pStudent = students)
            {
                pStudent[0].Grade = 95; // Modify unsafe
            }

            Console.WriteLine("Modified Students:");
            foreach (var student in students)
            {
                Console.WriteLine(student);
            }

            // 2 groups
            var itGroup = new StudentGroup<Student>();
            itGroup.AddStudent(student1);
            itGroup.AddStudent(student2);

            var businessGroup = new StudentGroup<Student>();
            businessGroup.AddStudent(student3);
            businessGroup.AddStudent(student4);

            // Merge 
            var mergedGroup = itGroup + businessGroup;

            Console.WriteLine("\nMerged Student Group:");
            foreach (var student in mergedGroup.GetAllStudents())
            {
                Console.WriteLine(student);
            }

            // Compare
            if (itGroup > businessGroup)
            {
                Console.WriteLine("\nIT group has better performance.");
            }
            else
            {
                Console.WriteLine("\nBusiness group has better performance.");
            }
        }
    }
}
