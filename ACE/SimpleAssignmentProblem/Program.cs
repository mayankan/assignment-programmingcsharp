using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleAssignmentProblem
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            int noOfteachers,noOfSubjects,noOfClasses,subjectsInClass;
            string teacherName,className;
            Dictionary<string, List<string>> teacherDictionary = new Dictionary<string, List<string>>();
            Dictionary<string, List<string>> classDictionary = new Dictionary<string, List<string>>();
            Dictionary<string, List<string>> outputDictionary = new Dictionary<string, List<string>>();
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
                teacherDictionary.Add(teacherName,subjectsTaught);
            }
            Console.Write("Enter number of classes ");
            noOfClasses = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < noOfClasses; i++)
            {
                Console.Write("Enter Class ");
                className = Console.ReadLine();
                Console.Write("Enter number of lectures taught in this class ");
                subjectsInClass = Convert.ToInt32(Console.ReadLine());
                List<string> classSchedule = new List<string>();
                for (int j = 0; j < subjectsInClass; j++)
                {
                    Console.Write("Enter the name of lecture taught in the class ");
                    classSchedule.Add(Console.ReadLine());
                }
                classDictionary.Add(className,classSchedule);
            }
            int count = 2;
            foreach (var x in classDictionary)
            {
                if (count==0)
                    continue;
                else
                {
                    List<string> subjectsFetch = (from y in classDictionary where y.Key == x.Key select x.Value).FirstOrDefault();
                    List<string> teachersReplacedBySubjects = new List<string>();
                    foreach (string z in subjectsFetch)
                    {
                        string teacherFetched = (from a in teacherDictionary where a.Value.Contains(z) select a.Key)
                            .FirstOrDefault();   
                        if(teachersReplacedBySubjects.Contains(teacherFetched))
                            continue;
                        else if (outputDictionary.SelectMany(b => b.Value).Contains(teacherFetched))
                        {
                            teachersReplacedBySubjects.Add(
                                teacherFetched);
                            count--;
                        }
                        else
                        {
                            teachersReplacedBySubjects.Add(
                                teacherFetched);
                        }
                    }
                    outputDictionary.Add(x.Key,teachersReplacedBySubjects);
                }
            }

            foreach (var outPut in outputDictionary)
            {
                Console.Write("Timetable for Class {0} is ",outPut.Key);
                foreach (string zeta in outPut.Value)
                {
                    if(zeta!=outPut.Value.Last()) 
                        Console.Write(" " + zeta + ", ");
                    else
                        Console.WriteLine(" " + zeta);
                }
            }
        }
        
    }
}