using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Sqlite;
using System.Collections;
using Dapper;

namespace nss.Data
{
    public class DatabaseInterface
    {
        public static SqliteConnection Connection
        {
            get
            {
                string env = $"{Environment.GetEnvironmentVariable("NSS_DB")}";
                string _connectionString = $"Data Source={env}";
                return new SqliteConnection(_connectionString);
            }
        }


        public static void CheckCohortTable()
        {
            SqliteConnection db = DatabaseInterface.Connection;

            try
            {
                List<Cohort> toys = db.Query<Cohort>
                    ($"SELECT i.Id FROM Cohort i").ToList();
            }
            catch (System.Exception ex)
            {
                if (ex.Message.Contains("no such table"))
                {
                    db.Execute($@"CREATE TABLE Cohort (
                        `Id`	integer NOT NULL PRIMARY KEY AUTOINCREMENT,
                        `Name`	varchar(80) NOT NULL UNIQUE
                    )");

                    db.Execute($@"INSERT INTO Cohort
                        VALUES (null, 'Evening Cohort 1')");

                    db.Execute($@"INSERT INTO Cohort
                        VALUES (null, 'Day Cohort 10')");

                    db.Execute($@"INSERT INTO Cohort
                        VALUES (null, 'Day Cohort 11')");

                    db.Execute($@"INSERT INTO Cohort
                        VALUES (null, 'Day Cohort 12')");

                    db.Execute($@"INSERT INTO Cohort
                        VALUES (null, 'Day Cohort 13')");

                    db.Execute($@"INSERT INTO Cohort
                        VALUES (null, 'Day Cohort 21')");

                }
            }
        }

        public static void CheckExerciseTable()
        {
            SqliteConnection db = DatabaseInterface.Connection;

            try
            {
                // Select the ids from the table to see if it exists
                List<Exercise> toys = db.Query<Exercise>
                    ($"SELECT e.Id FROM Exercise e").ToList();
            }
            catch (System.Exception ex)
            {
                /*
                    If an exception was thrown with the text "no such table"
                    then the table doesn't exist. Execute a CREATE TABLE
                    statement to create it.
                */
                if (ex.Message.Contains("no such table"))
                {
                    db.Execute($@"CREATE TABLE Exercise (
                        `Id`	integer NOT NULL PRIMARY KEY AUTOINCREMENT,
                        `Name`	varchar(80) NOT NULL,
                        `Language` varchar(80) NOT NULL
                    )");

                    /*
                        Seed the table with some initial entries
                     */
                    db.Execute($@"INSERT INTO Exercise
                        VALUES (null, 'ChickenMonkey', 'JavaScript')");
                    db.Execute($@"INSERT INTO Exercise
                        VALUES (null, 'Overly Excited', 'JavaScript')");
                    db.Execute($@"INSERT INTO Exercise
                        VALUES (null, 'Boy Bands & Vegetables', 'JavaScript')");

                }
            }
        }

        public static void CheckInstructorsTable()
        {
            SqliteConnection db = DatabaseInterface.Connection;

            try
            {
                List<Instructor> toys = db.Query<Instructor>
                    ($"SELECT i.Id FROM Instructor i").ToList();
            }
            catch (System.Exception ex)
            {
                if (ex.Message.Contains("no such table"))
                {
                    db.Execute($@"CREATE TABLE Instructor (
                        `Id`	integer NOT NULL PRIMARY KEY AUTOINCREMENT,
                        `FirstName`	varchar(80) NOT NULL,
                        `LastName`	varchar(80) NOT NULL,
                        `SlackHandle`	varchar(80) NOT NULL,
                        `Specialty` varchar(80),
                        `CohortId`	integer NOT NULL,
                        FOREIGN KEY(`CohortId`) REFERENCES `Cohort`(`Id`)
                    )");

                    db.Execute($@"INSERT INTO Instructor
                        SELECT null,
                              'Steve',
                              'Brownlee',
                              '@coach',
                              'Dad jokes',
                              c.Id
                        FROM Cohort c WHERE c.Name = 'Evening Cohort 1'
                    ");

                    db.Execute($@"INSERT INTO Instructor
                        SELECT null,
                              'Joe',
                              'Shepherd',
                              '@joes',
                              'Analogies',
                              c.Id
                        FROM Cohort c WHERE c.Name = 'Day Cohort 13'
                    ");

                    db.Execute($@"INSERT INTO Instructor
                        SELECT null,
                              'Jisie',
                              'David',
                              '@jisie',
                              'Student success',
                              c.Id
                        FROM Cohort c WHERE c.Name = 'Day Cohort 21'
                    ");
                }
            }
        }

        public static void CheckStudentTable()
        {
            SqliteConnection db = DatabaseInterface.Connection;

            try
            {
                List<Student> toys = db.Query<Student>
                    ($"SELECT i.Id FROM Student i").ToList();
            }
            catch (System.Exception ex)
            {
                if (ex.Message.Contains("no such table"))
                {
                    db.Execute($@"CREATE TABLE Student (
                        `Id`	integer NOT NULL PRIMARY KEY AUTOINCREMENT,
                        `FirstName`	varchar(80) NOT NULL,
                        `LastName`	varchar(80) NOT NULL,
                        `SlackHandle`	varchar(80) NOT NULL,
                        `CohortId`	integer NOT NULL,
                        FOREIGN KEY(`CohortId`) REFERENCES `Cohort`(`Id`)
                    )");

                    db.Execute($@"INSERT INTO Student
                        SELECT null,
                              'Kate',
                              'Williams',
                              '@katerebekah',
                              c.Id
                        FROM Cohort c WHERE c.Name = 'Evening Cohort 1'
                    ");

                    db.Execute($@"INSERT INTO Student
                        SELECT null,
                              'Ryan',
                              'Tanay',
                              '@ryan.tanay',
                              c.Id
                        FROM Cohort c WHERE c.Name = 'Day Cohort 10'
                    ");

                    db.Execute($@"INSERT INTO Student
                        SELECT null,
                              'Juan',
                              'Rodriguez',
                              '@juanrod',
                              c.Id
                        FROM Cohort c WHERE c.Name = 'Day Cohort 11'
                    ");
                }
            }
        }

    }
}