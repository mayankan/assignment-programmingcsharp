using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace ComplexAssignmentProblem
{
    internal class Program
    {
        enum WeekDays
        {
            Monday = 0,
            Tuesday = 1,
            Wednesday = 2,
            Thursday = 3,
            Friday = 4,
        }

        class ClassSchedule
        {
            public string className;
            public string classValue;
        }

        public static void Main(string[] args)
        {
            int noOfteachers, noOfSubjects, noOfClasses, subjectsInClass = 0;
            string teacherName = "", className = "";
            Dictionary<string, List<string>> teacherDictionary = new Dictionary<string, List<string>>();
            Dictionary<int, List<ClassSchedule>> weekClassDictionary = new Dictionary<int, List<ClassSchedule>>();
            Dictionary<int, List<ClassSchedule>> outputDictionary = new Dictionary<int, List<ClassSchedule>>();

            Console.Write("Enter number of professors ");
            noOfteachers = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < noOfteachers; i++)
            {
                Console.Write("Enter name of the professor ");
                teacherName = Console.ReadLine();
                Console.Write("Enter number of subjects taught by this professor ");
                noOfSubjects = Convert.ToInt32(Console.ReadLine());
                List<string> subjectsTaught = new List<string>();
                for (int j = 0; j < noOfSubjects; j++)
                {
                    Console.Write("Enter the subject taught by the professor ");
                    subjectsTaught.Add(Console.ReadLine());
                }

                teacherDictionary.Add(teacherName, subjectsTaught);
            }

            Console.Write("Enter number of classes ");
            noOfClasses = Convert.ToInt32(Console.ReadLine());
            for (int j = 0; j < noOfClasses; j++)
            {
                Console.Write("Enter Class ");
                className = Console.ReadLine();
                for (int i = 0; i < 5; i++)
                {
                    do
                    {
                        Console.Write("Enter number of lectures taught in this class  on {0} ",
                            Enum.GetName(typeof(WeekDays), i));
                        subjectsInClass = Convert.ToInt32(Console.ReadLine());
                    } while (subjectsInClass < 4);

                    List<ClassSchedule> classSchedule = new List<ClassSchedule>();
                    for (int k = 0; k < subjectsInClass; k++)
                    {
                        if ((classSchedule.GroupBy(x => x.classValue).Distinct().Count()) < 4)
                            Console.WriteLine("Please enter atleast 4 unique subjects");
                        else
                        {
                            Console.Write("Enter the lecture taught in the class ");
                            classSchedule.Add(new ClassSchedule()
                            {
                                className = className,
                                classValue = Console.ReadLine()
                            });
                        }
                    }

                    weekClassDictionary.Add(i, classSchedule);
                }
            }

            List<string> classSubjects =
                weekClassDictionary.SelectMany(x => x.Value).Select(x => x.classValue).ToList();

            /*foreach (ClassSchedule x in classSchedules)
            {
                subjectsCol.Add(x.classSubject);
            }*/

            List<string> selectedSubjects =
                classSubjects.GroupBy(x => x).Where(x => x.Count() != 5).Select(x => x.Key).ToList();
            List<string> subjectsInTeacher = teacherDictionary.SelectMany(x => x.Value).Distinct().ToList();
            foreach (string x in subjectsInTeacher)
            {
                if (!selectedSubjects.Contains(x))
                    selectedSubjects.Add(x);
            }

            if (selectedSubjects.Count > 0)
                Console.WriteLine(selectedSubjects.ToString());
            int count = 2;
            for (int i = 0; i < 5; i++)
            {
                var query1 = weekClassDictionary.Where(x => x.Key == i).SelectMany(x => x.Value);
                List<string> classes = weekClassDictionary.Where(x => x.Key == i).SelectMany(x => x.Value)
                    .Select(x => x.className).Distinct()
                    .ToList();
                foreach (string y in classes)
                {
                    List<string> subjectsFetch =
                        query1.Where(x => x.className == y).Select(x => x.classValue).ToList();
                    List<ClassSchedule> teachersReplacedBySubjects = new List<ClassSchedule>();
                    if (count == 0)
                        teachersReplacedBySubjects.Add(new ClassSchedule()
                        {
                            className = y,
                            classValue = " "
                        });
                    else
                    {
                        foreach (string z in subjectsFetch)
                        {
                            string teacherFetched = (from a in teacherDictionary where a.Value.Contains(z) select a.Key)
                                .FirstOrDefault();
                            if (teachersReplacedBySubjects.Select(x => x.classValue).Contains(teacherFetched))
                                continue;
                            else if (query1.Select(b => b.classValue).Contains(teacherFetched))
                            {
                                teachersReplacedBySubjects.Add(
                                    new ClassSchedule()
                                    {
                                        className = y,
                                        classValue = teacherFetched
                                    });
                                count--;
                            }
                            else
                            {
                                teachersReplacedBySubjects.Add(
                                    new ClassSchedule()
                                    {
                                        className = y,
                                        classValue = teacherFetched
                                    });
                            }
                        }
                        outputDictionary.Add(i, teachersReplacedBySubjects);
                    }
                }
            }

            List<string> classesOupList = outputDictionary.SelectMany(x => x.Value).Select(x => x.className).ToList();
            foreach (string a in classesOupList)
            {
                Console.WriteLine("Timetable for Class {0} :",a);
                for (int i = 0; i < 5; i++)
                {
                    Console.Write(Enum.GetName(typeof(WeekDays), i)+" : ");
                    List<string> outputList = outputDictionary.Where(u => u.Key == i).SelectMany(x => x.Value)
                        .Where(x => x.className == a).Select(x => x.classValue).ToList();
                    foreach (string b in outputList)
                    {
                        if (b != outputList.Last())
                            Console.Write(" " + b + ", ");
                        else
                            Console.WriteLine(" " + b);
                    }
                }
            }
            
        }
    }
}