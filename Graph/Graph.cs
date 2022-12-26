using System;
using System.Linq;
using System.Collections.Generic;

namespace Graph
{
    public interface IGraph<T>
    {
        IObservable<IEnumerable<T>> RoutesBetween(T source, T target);
    }

    public class Graph<T> 
    {

        private IEnumerable<ILink<string>> _links;

        public Graph(IEnumerable<ILink<T>> links)
        {
            _links = (IEnumerable<ILink<string>>)links;
        }

        public List<List<string>> RoutesBetween(T source, T target)
        {
            var links = _links.ToArray();

            // inicializa graph
            Dictionary<string, List<string>> graph = new Dictionary<string, List<string>>();
            foreach (var link in links)
            {
                graph[link.Source.ToString()] = new List<string>();
                graph[link.Target.ToString()] = new List<string>();
            }

            foreach (var link in links)
            {
                graph[link.Source.ToString()].Add(link.Target.ToString());
            }

            Dictionary<string, bool> visited = new Dictionary<string, bool>();

            List<List<string>> paths = new List<List<string>>();

            for (int i = 0; i < graph[source.ToString()].Count; i++)
            {
                paths.Add(new List<string>());

                foreach (var key in graph.Keys)
                {
                    visited[key] = false;
                }

                visited[source.ToString()] = true;

                paths[i].Add(source.ToString());
                paths[i].Add(graph[source.ToString()][i]);
                List<string> aux = CreatePath(source.ToString(), graph[source.ToString()][i], target.ToString(), graph, visited, new List<string>());

                foreach (string a in aux)
                {
                    paths[i].Add(a);
                }

            }

            return paths;

        }

        public List<string> CreatePath(string start, string next, string end, Dictionary<string, List<string>> graph,
            Dictionary<string, bool> visited, List<string> path)
        {

            if (String.Compare(next.ToString(), end.ToString()) == 0)
            {
                return path;
            }

            visited[next] = true;

            foreach (var node in graph[next])
            {
                if (!visited[node])
                {
                    path.Add(node);
                    path = CreatePath(start, node, end, graph, visited, path);
                    return path;


                }
            }

            return path;
        }
    }
}
