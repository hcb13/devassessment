﻿using System;
using System.Collections.Generic;
using Xunit;


namespace Graph.Tests
{
    public class GraphTest
    {
        [Fact]
        public void TestRoutesBetweenTwoPoints()
        {
            var links = new ILink<string>[]
            {
                new Link<string>("a","b"),
                new Link<string>("b","c"),
                new Link<string>("c","b"),
                new Link<string>("b","a"),
                new Link<string>("c","d"),
                new Link<string>("d","e"),
                new Link<string>("d","a"),
                new Link<string>("a","h"),
                new Link<string>("h","g"),
                new Link<string>("g","f"),
                new Link<string>("f","e"),
            };

            var graph = new Graph<string>(links);
            List<List<string>> paths = graph.RoutesBetween("a", "e");

            Assert.Equal(2, paths.Count);

            Assert.Contains(paths, l => String.Join("-", l) == "a-b-c-d-e");
            Assert.Contains(paths, l => String.Join("-", l) == "a-h-g-f-e");
        }
    }
}
