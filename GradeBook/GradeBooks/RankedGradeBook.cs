using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using GradeBook.Enums;

namespace GradeBook.GradeBooks
{
	public class RankedGradeBook : BaseGradeBook
	{
		public RankedGradeBook(string name) : base(name)
		{
			Type = GradeBookType.Ranked;
		}

		public override char GetLetterGrade(double averageGrade)
		{
			if (base.Students.Count < 5)
				throw new InvalidOperationException("Ranked grading required 5 or more students");

			var treshhold = (int)Math.Ceiling(Students.Count * 0.2);

			var grades = Students.OrderByDescending(e => e.AverageGrade).Select(e => e.AverageGrade).ToList();

			if (grades[treshhold - 1] <= averageGrade)
				return 'A';
			else if (grades[(treshhold * 2) - 1] <= averageGrade)
				return 'B';
			else if (grades[(treshhold * 3) - 1] <= averageGrade)
				return 'C';
			else if (grades[(treshhold * 4) - 1] <= averageGrade)
				return 'D';
			else
				return 'F';
		}

		public override void CalculateStatistics()
		{
			if (base.Students.Count < 5)
			{
				Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
				return;
			}
			base.CalculateStatistics();
		}

		public override void CalculateStudentStatistics(string name)
		{
			if (base.Students.Count < 5)
			{
				Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
				return;
			}
			base.CalculateStudentStatistics(name);
		}
	}
}
