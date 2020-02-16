using System;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoreCollections;

namespace Common.Collections.Tests
{
    [TestClass]
    public class KnapsackProblemTests
    {
        [TestMethod]
        public void ContinuousKnapsack()
        {
            var items = new[]
            {
                new{Name = "Potatoes", Quantity = 5.6, Value = 9.2},
                new{Name = "Strawberries", Quantity = 1.3, Value = 3.4},
                new{Name = "Liver", Quantity = 1.9, Value = .4},
            };
            var knapsackContents = items.ContinuousKnapsack(5, anon => anon.Quantity, anon => anon.Value).ToList();
            knapsackContents.Count.Should().Be(2);
            knapsackContents[0].Item.Name.Should().Be("Strawberries");
            knapsackContents[0].Quantity.Should().BeApproximately(1.3, Double.Epsilon);
            knapsackContents[1].Item.Name.Should().Be("Potatoes");
            knapsackContents[1].Quantity.Should().BeApproximately(3.7, Double.Epsilon);
        }

        [TestMethod]
        public void Knapsack()
        {
            var items = new[]
            {
                new { Name = "Map", Weight = 9, Value = 150 },
                new { Name = "Water", Weight = 153, Value = 200 },
                new { Name = "Compass", Weight = 13, Value = 35 },
                new { Name = "Sandwitch", Weight = 50, Value = 160 },
                new { Name = "Glucose", Weight = 15, Value = 60 },
                new { Name = "Tin", Weight = 68, Value = 45 },
                new { Name = "Banana", Weight = 27, Value = 60 },
                new { Name = "Apple", Weight = 39, Value = 40 },
                new { Name = "Cheese", Weight = 23, Value = 30 },
                new { Name = "Beer", Weight = 52, Value = 10 },
                new { Name = "Suntan Cream", Weight = 11, Value = 70 },
                new { Name = "Camera", Weight = 32, Value = 30 },
                new { Name = "T-shirt", Weight = 24, Value = 15 },
                new { Name = "Trousers", Weight = 48, Value = 10 },
                new { Name = "Umbrella", Weight = 73, Value = 40 },
                new { Name = "WaterProof Trousers", Weight = 42, Value = 70 },
                new { Name = "Note-Case", Weight = 22, Value = 80 },
                new { Name = "Sunglasses", Weight = 7, Value = 20 },
                new { Name = "Towel", Weight = 18, Value = 12 },
                new { Name = "Socks", Weight = 4, Value = 50 },
                new { Name = "Book", Weight = 30, Value = 10 },
                new { Name = "waterproof overclothes ", Weight = 43, Value = 75 },
            };
            var knapsackContents = items.Knapsack(20, anon => anon.Weight, anon => anon.Value).ToList();
            knapsackContents.Count.Should().Be(2);
            knapsackContents[0].Name.Should().Be("Map");
            knapsackContents[1].Name.Should().Be("Suntan Cream");
        }
    }
}
