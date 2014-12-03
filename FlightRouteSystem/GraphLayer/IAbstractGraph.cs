using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text;
using DatabaseLayer;
using System.Runtime.Serialization;

namespace GraphLayer
{	//  FEN 2005.02.16
    //	PQC 2005.04.27 Namespace changed to Noea.Graph
    //  FEN 2006.02.05 Simplified to use in exersice
    //  FEN 2008.10.22 Using Generics
    //  FEN 2012.09.30 Changing return type for GetAdjencies to ICollection
    //                 in order to support adjency list implementation.
    //                 Changing returntype of Vertices() from IEnumerator to
    //                 ICollection in order to allow setting of Mark
    //                 Introducing abstract classes for methods which are common
    //                 to matrix and list implementations
    /// <summary>
    /// basic graph operations on an unweighted graph are defined in this interface
    /// specification omitted when obvious
    /// </summary>
    [ServiceContract]
    public interface IAbstractGraph
    {
        ///<summary>
        ///adds a vertex to the graph
        ///PRE: vertex is not in the graph
        ///</summary>
        [OperationContract]
        void AddAllVertices(string date);

        ///<summary>
        ///adds an edge to the (unweighted) graph
        ///PRE (startVertex,endVertex) is not contained in the graph
        ///</summary>
        [OperationContract]
        void AddAllEdges();

        ///<summary>
        ///determines whether a vertex is in the graph
        ///</summary>
        [OperationContract]
        bool ContainsVertex(Vertex vertex);

        ///<summary>
        ///determines whether two vertices are adjacent
        ///</summary>
        

        ///<summary>
        ///Returns a collection containing all vertices adjancent to vertex.
        ///Must be a collection and not just an enumerator
        ///since it should be possible to remove adjacent vertices
        ///</summary>
 

        ///<summary>
        ///Returns an Enumerator to the collection of vertices
        ///</summary>
        [OperationContract]
        List<Vertex> GetVertices();

        ///<summary>
        ///determines whether the graph is empty
        ///</summary>
       [OperationContract]
        bool IsEmpty();

        ///<summary>
        ///returns the number of vertices
        ///</summary>
       // int GetNoOfVertices();

        ///<summary>
        ///returns the number of edges
        ///</summary>
       // int GetNoOfEdges();


        ///<summary>
        ///makes the graph empty
        ///</summary>
       [OperationContract]
        void Clear();


    }
}
