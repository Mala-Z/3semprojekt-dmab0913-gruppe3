using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using GraphLayer;
using System.Linq;
using System.Data.Linq;
using DatabaseLayer;


namespace GraphLayer
{
    class GraphCtr : IAbstractGraph
    {
        private IList<Vertex> _vertices;
        private List<LinkedList<Vertex>> _adjList;
        private int numberOfVertices;

        public GraphCtr()
        {
            _vertices = AddAllVertices();
           this.numberOfVertices = GetNoOfVertices();
           Init();
        }

        private void Init()
        {
            _adjList = new List<LinkedList<Vertex>>(numberOfVertices);
            for (int i = 0; i < numberOfVertices; i++)
                _adjList.Add(new LinkedList<Vertex>());
        }

        public IList<Vertex> AddAllVertices()
        {
            var db = DBConnection.GetInstance().GetConnection();

            var listVertex = new List<Vertex>();

            var list = db.Airports.Select(a => a).ToList();
            foreach (Airport airport in list)
            {
                Vertex v = new Vertex(airport);
                listVertex.Add(v);
            }
            return listVertex;

        }

        public void AddEdge(Vertex startVertex, Vertex endVertex)
        {
            int startIndex = _vertices.IndexOf(startVertex);
            _adjList[startIndex].AddFirst(endVertex);
            //if undirected also:
            //int endIndex = _vertices.IndexOf(endVertex);
            //_adjList[endIndex].AddFirst(startVertex);
        }

        public bool ContainsVertex(Vertex vertex)
        {
            return _vertices.Contains(vertex);
        }

        public bool IsAdjacent(Vertex startVertex, Vertex endVertex)
        {
            int vertexIndex = _vertices.IndexOf(startVertex);
            bool found = false;
            LinkedListNode<Vertex> cur = _adjList[vertexIndex].First;
            while (!found && cur != null)
            {
                if (cur.Value.Equals(endVertex))
                    found = true;
                else
                    cur = cur.Next;
            }
            return found; ;
        }

        //public override IList<Vertex> GetAdjacencies(Vertex vertex)
        //{
        //    int vertexIndex = _vertices.IndexOf(vertex);
        //    return new List<Vertex>(_adjList[vertexIndex]);
        //}

        public ICollection<Vertex> GetAdjacencies(Vertex vertex)
        {
            int vertexIndex = _vertices.IndexOf(vertex);
            return _adjList[vertexIndex];
        }

        public ICollection<Vertex> GetVertices()
        {
            return _vertices;
        }

        public bool IsEmpty()
        {
            return _vertices.Count == 0;
        }

        public int GetNoOfVertices()
        {
            return _vertices.Count;
        }

        public int GetNoOfEdges()
        {
            int count = 0;
            foreach (LinkedList<Vertex> l in _adjList)
                count = count + l.Count;
            return count;//if undirected count/2
        }

        public void Clear()
        {
            Init();
        }

     

  
    
    }
}
