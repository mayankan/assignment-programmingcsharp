using System;
using System.Collections.Generic;
using System.Linq;

namespace ComplexAssignmentProblem
{
    internal class Program
    {
        //Enum for Weekdays which helps convert integer to Days in time-table.
        enum WeekDays
        {
            Monday = 0,
            Tuesday = 1,
            Wednesday = 2,
            Thursday = 3,
            Friday = 4,
        }
        //class having className and classValue as members which is needed to store inputs of subjects taught in class and teachers teaching in class at the time of output.
        class ClassSchedule
        {
            public string className;
            public string classValue;
        }
        public static void Main(string[] args)
        {
            
            //noOfteacher,noOfSubject,noOfClasses stores number of inputs for classes, subjects and teacher at the time of input from the user.
            //subjectsInClass stores number of subjects in one class at a time.
            int noOfteachers, noOfSubjects, noOfClasses, subjectsInClass = 0;
            //teacherName stores one professor name at a time.
            //className stores one class name at a time.
            string teacherName = "", className = "";
            bool flag5 = false;
            //teacherDictionary stores teacherName as key and List of subjects being taught by that teacher as the value.
            Dictionary<string, List<string>> teacherDictionary = new Dictionary<string, List<string>>();
            //weekClassDictionary stores integer for specifying weekday as key and List of class ClassSchedule which includes class's name and subjects taught in that class as the value.
            Dictionary<int, List<ClassSchedule>> weekClassDictionary = new Dictionary<int, List<ClassSchedule>>();
            //outputDictionary stores integer for specifying weekday as key and List of class ClassSchedule which includes class's name teaching in that class as the value for the output.
            Dictionary<int, List<ClassSchedule>> outputDictionary = new Dictionary<int, List<ClassSchedule>>();
            //Takes input for number of teachers.
            Console.Write("Enter number of professors ");
            noOfteachers = Convert.ToInt32(Console.ReadLine());
            //Looping for each teacher's subject values.
            for (int i = 0; i < noOfteachers; i++)
            {
                //Taking input for teacher's name as key of each teacherDictionary.
                Console.Write("Enter name of the professor ");
                teacherName = Console.ReadLine();
                //Taking input for number of subjects taught by that teacher.
                Console.Write("Enter number of subjects taught by this professor ");
                noOfSubjects = Convert.ToInt32(Console.ReadLine());
                //subjectsTaught stores List of subjects taught by this teacher in the current loop.
                List<string> subjectsTaught = new List<string>();
                //Looping for storing each subject
                for (int j = 0; j < noOfSubjects; j++)
                {
                    Console.Write("Enter the subject taught by the professor ");
                    subjectsTaught.Add(Console.ReadLine());
                }
                //Adds each teacher's name and List of subjects taught by that teacher into teacherDictionary.
                teacherDictionary.Add(teacherName, subjectsTaught);
            }
            //Takes input for number of classes to store class classSchedule values.
            Console.Write("Enter number of classes ");
            noOfClasses = Convert.ToInt32(Console.ReadLine());
            //Looping for each class's values.
            for (int j = 0; j < noOfClasses; j++)
            {
                //Takes input for class's name as key of each classDictionary.
                Console.Write("Enter Class ");
                className = Console.ReadLine();
                do
                {
                    //weekClasstestDictionary stores a local instance of weekClassDictionary that is specified globally to fetch local values in current loop.
                    Dictionary<int, List<ClassSchedule>> weekClasstestDictionary = new Dictionary<int, List<ClassSchedule>>();
                    //Looping for each week day.
                    for (int i = 0; i < 5; i++)
                    {
                        do
                        {
                            //Taking input for number of lectures taught in that class.
                            Console.Write("Enter number of lectures taught in this class  on {0} ",
                                Enum.GetName(typeof(WeekDays), i));
                            subjectsInClass = Convert.ToInt32(Console.ReadLine());
                        } while (subjectsInClass < 4); //Checks if atleast 4 subjects are being entered and run the statement again if untrue.
                        //flag4 is boolean flag which checks whether 4 unique subjects are entered in a day or not.
                        bool flag4 = false;
                        //classSchedule stores list of subjects taught in current class for the current day.(current->iteration)
                        List<ClassSchedule> classSchedule = new List<ClassSchedule>();
                        do
                        {
                            //Looping for storing each lecture.
                            for (int k = 0; k < subjectsInClass; k++)
                            {
                                //Takes input from user into List of lecture Values.
                                Console.Write("Enter the lecture taught in the class ");
                                classSchedule.Add(new ClassSchedule()
                                {
                                    className = className,
                                    classValue = Console.ReadLine()
                                });
                            }
                            //Checks if there are atleast 4 unique subjects in current class.
                            if ((classSchedule.GroupBy(x => x.classValue).Distinct().Count()) < 4)
                                Console.WriteLine("Please enter atleast 4 unique subjects");
                            else
                                flag4 = true;
                        } while (!flag4); //Reruns the statements which takes input for current class in current day if it doesn't have atleast 4 unique subject values.
                        //Adds each class's name and List of lectures taught in that class with the day(as key) into classDictionary.
                        weekClasstestDictionary.Add(i, classSchedule);
                    }
                    //classSubjects stores List of subjects for current class for all the days.
                    List<string> classSubjects =
                        weekClasstestDictionary.SelectMany(x => x.Value).Where(x => x.className==className).Select(x=>x.classValue).ToList();
                    //subjectsFromInput queries classSubjects and selects unique values of subjects from the list.
                    List<string> subjectsFromInput = classSubjects.GroupBy(x => x).Select(x => x.Key).ToList();
                    //selectedSubjects queries classSubjects and selects subjects that are not exactly occuring 5 times in the week.
                    List<string> selectedSubjects =
                        classSubjects.GroupBy(x => x).Where(x => x.Count() != 5).Select(x => x.Key).ToList();
                    //subjectsInTeacher queries teacherDictionary which includes teacher's name and list of subjects taught by each teacher, for unique values of subjects being taught by teacher.
                    List<string> subjectsInTeacher = teacherDictionary.SelectMany(x => x.Value).Distinct().ToList();
                    //Looping for each value in subjectTeacher
                    foreach (string x in subjectsInTeacher)
                    {
                        //Checks if subjectsFromInput which contains unique values of subjects from the user input, has values for subjects being taught by teacher.
                        if (subjectsFromInput.Contains(x))
                            continue;
                        //selectedSubjects is added the value of subject if teacher is not teaching the subject entered by the user.
                        else
                            selectedSubjects.Add(x); //eg - dm entered instead of discrete maths adds to this statement.
                    }
                    //Checks if there is a abnormality in the subject values entered by the user.
                    if (selectedSubjects.Count > 0)
                        Console.WriteLine("Each subject should have 5 lectures in a week.");
                    else
                    {
                        flag5 = true;
                        //Looping for each local instance of weekClassDictionary specified in the loop.
                        foreach (var x in weekClasstestDictionary)
                        {
                            weekClassDictionary.Add(x.Key,x.Value);
                        }
                    }
                } while (!flag5); //Reruns the statements which takes input for current class for all days of the week if all the subjects are not entered exactly 5 times in the week.
            }
            //count variable is defined to check whether teacher is already alloted to 2 classes.
            int count = 2;
            //
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
            List<string> classesOupList = outputDictionary.SelectMany(x => x.Value).Select(x => x.className).Distinct().ToList();
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
