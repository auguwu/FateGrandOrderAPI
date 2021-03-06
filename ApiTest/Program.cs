﻿using System;
using System.Text;
using System.Diagnostics;
using FateGrandOrderApi.Classes;
using System.Collections.Generic;
using static FateGrandOrderApi.FateGrandOrderParsing;

namespace ApiTest
{
    class Program
    {
        public static List<Servant> ServantsParsed = new List<Servant>();

        static void Main(string[] args)
        {
            Console.WriteLine("Api Tester - AzyIsCool");

            FateGrandOrderApi.Settings.Cache.SaveCachedPartsToDisk = false;
            Stopwatch stopwatch = new Stopwatch();

            var s = GetActiveSkill("Galaxy Meteor Sword");

            foreach (var servant in Servants)
            {
                stopwatch.Reset();
                Console.WriteLine(Line($"Getting {servant} data"));
                Console.WriteLine($"Getting {servant} data");
                stopwatch.Start();
                var persondata = GetServant(servant, PresetsForInformation.AllInformation).ConfigureAwait(true).GetAwaiter().GetResult();
                stopwatch.Stop();
#if !DEBUG
                Console.WriteLine($"It took {stopwatch.Elapsed} to get {servant} data");
#elif DEBUG
                Console.WriteLine($"It took {stopwatch.Elapsed} to get {servant} data (Is cached: {persondata.FromCache})");
#endif
#if DEBUG
                Console.WriteLine(Line($"It took {stopwatch.Elapsed} to get {servant} data (Is cached: {persondata.FromCache})"));
#elif !DEBUG
                Console.WriteLine(Line($"It took {stopwatch.Elapsed} to get {servant} data"));
#endif
                if (persondata.BasicInformation != null)
                {
                    StringBuilder servantInfo = new StringBuilder();
                    servantInfo.AppendLine($"Name: {persondata.BasicInformation.EnglishName}");
                    servantInfo.AppendLine($"Jap name: {persondata.BasicInformation.JapaneseName}");
                    servantInfo.AppendLine($"Gender: {persondata.BasicInformation.Gender}");
                    servantInfo.AppendLine($"ATK: {persondata.BasicInformation.ATK}");
                    servantInfo.AppendLine($"Class: {persondata.BasicInformation.Class}");
                    servantInfo.AppendLine($"Cost: {persondata.BasicInformation.Cost}");
                    Console.Write(servantInfo);
                }
                ServantsParsed.Add(persondata);
            }
            Console.ReadLine();
        }

        static string Line(string person)
        {
            StringBuilder b = new StringBuilder();
            return b.Append('-', person.Length).ToString();
        }

#if !DEBUG
        static readonly string[] Servants = { "Jeanne d'Arc (Alter)", "Lancelot (Saber)", "Sigurd", "Artoria Pendragon (Alter)", "Medb (Saber)", "Diarmuid Ua Duibhne (Saber)", "Jack the Ripper", "Helena Blavatsky", "Atalanta", "Enkidu" };
#elif DEBUG
        static readonly string[] Servants = { "Enkidu" };
#endif
    }
}
