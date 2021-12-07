using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;

namespace AdventOfCode2020 {
    public class Day16 {
        public static void Main(string[] args) {
            var (fields, myTicket, nearByTickets) = ParseInput();
            var allValidNumbers = fields.Values.SelectMany(x => x).ToHashSet();
            var sumErrors = 0;
            var validTickets = new List<List<int>> { myTicket };
            foreach (var nearByTicket in nearByTickets) {
                var nearByTicketHs = nearByTicket.ToHashSet();
                nearByTicketHs.ExceptWith(allValidNumbers);
                if (nearByTicketHs.Count == 0) {
                    validTickets.Add(nearByTicket);
                }
                sumErrors += nearByTicketHs.Sum();
            }
            Console.WriteLine($"Day 16 part 1: {sumErrors}");

            var filteredFieldsOfAllTickets = new List<List<HashSet<string>>>(validTickets.Count);
            foreach (var ticket in validTickets) {
                var filteredTicketFields = new List<HashSet<string>>(ticket.Count);
                for (var i = 0; i < ticket.Count; i++) {
                    var filteredFields = fields.Where(pair => pair.Value.Contains(ticket[i])).Select(f => f.Key).ToHashSet();
                    filteredTicketFields.Add(filteredFields);
                }      
                filteredFieldsOfAllTickets.Add(filteredTicketFields);
            }

            var preResult = new Dictionary<int, HashSet<string>>(fields.Count);
            for (var i = 0; i < fields.Count; i++) {
                preResult[i] = fields.Keys.ToHashSet();
            }
            foreach (var fieldsOfAllTicket in filteredFieldsOfAllTickets) {
                for (var fsetIndex = 0; fsetIndex < fieldsOfAllTicket.Count; fsetIndex++) {
                    preResult[fsetIndex].IntersectWith(fieldsOfAllTicket[fsetIndex]);
                }
            }

            string[] resultPositions = new string[fields.Count];
            while (preResult.Count > 0) {
                var foundFields = preResult.Where(kvp => kvp.Value.Count == 1)
                    .Select(p => (Index: p.Key, FieldName: p.Value.First())).ToList();
                foreach (var field in foundFields) 
                    resultPositions[field.Index] = field.FieldName;
                List<int> toRemove = new List<int>();
                foreach (var preResultField in preResult.Values) {
                    foreach (var foundField in foundFields) {
                        preResultField.Remove(foundField.FieldName);
                    }
                }

                foreach (var foundField in foundFields) {
                    preResult.Remove(foundField.Index);
                }
            }

            var result = 1L;
            for (var index = 0; index < resultPositions.Length; index++) {
                var position = resultPositions[index];
                if (position.Contains("departure")) {
                    result *= myTicket[index];
                }
            }

            Console.WriteLine($"Day 16 part 2: {result}");
        }
        
        private static (Dictionary<string, HashSet<int>> fields, List<int> myTicket, List<List<int>> nearByTickets) ParseInput() {
            var fields = new Dictionary<string, HashSet<int>>();
            var data = File.ReadAllText("Day16_input").Split("\n\n", StringSplitOptions.RemoveEmptyEntries);
            foreach (var s in data[0].Split('\n', StringSplitOptions.RemoveEmptyEntries)) {
                var field = s.Split(':', StringSplitOptions.RemoveEmptyEntries);
                var name = field[0];
                fields[name] = new HashSet<int>();
                var ranges = field[1].TrimStart().Split(" or ");
                foreach (var range in ranges) {
                    var minmax = range.Split('-', StringSplitOptions.RemoveEmptyEntries);
                    fields[name].UnionWith(Enumerable.Range(int.Parse(minmax[0]),
                        int.Parse(minmax[1]) - int.Parse(minmax[0]) + 1));
                }
            }
            var myTicket = data[1].Split('\n')[1].Split(',').Select(int.Parse).ToList();

            var nearByTickets = data[2].Split('\n', StringSplitOptions.RemoveEmptyEntries).Skip(1)
                .Select(t => t.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList()).ToList();;

            return (fields, myTicket, nearByTickets);
        }
    }
}