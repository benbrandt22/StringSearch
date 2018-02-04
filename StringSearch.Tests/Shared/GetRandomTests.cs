using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using StringSearch.Core;
using Xunit;
using StringSearch.Shared;

namespace StringSearch.Tests.Shared
{
    public class GetRandomTests
    {

        [Fact]
        public void GetRandomOnEmptySetThrowsException()
        {
            var set = new List<int>();

            Action test = () =>
            {
                var item = set.GetRandom();
            };

            test.ShouldThrow<InvalidOperationException>();
        }

        [Fact]
        public void GetRandomOnValidSetReturnsRandomValues()
        {
            var set = new List<int>() {1, 2, 3, 4, 5, 6, 7, 8, 9};

            int testCount = 100;
            var results = new List<int>();

            for (int i = 0; i < testCount; i++)
            {
                results.Add(set.GetRandom());
            }

            results.Distinct().Count().Should().BeGreaterThan(1);
        }

        [Fact]
        public void GetRandomOnValidSetReturnsAllPossibleValues()
        {
            var sourceSet = new List<int>() { 1, 2, 3, 4 };

            int testCount = 200;
            var results = new List<int>();

            for (int i = 0; i < testCount; i++)
            {
                results.Add(sourceSet.GetRandom());
            }

            foreach (var i in sourceSet)
            {
                results.Should().Contain(i);
            }
        }

        [Fact]
        public void GetRandomOnStringReturnsRandomValues()
        {
            var set = "ABCDEFG";

            int testCount = 100;
            var results = new List<char>();

            for (int i = 0; i < testCount; i++)
            {
                results.Add(set.GetRandom());
            }

            results.Distinct().Count().Should().BeGreaterThan(1);
        }
    }
}
