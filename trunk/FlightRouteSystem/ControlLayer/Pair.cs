using System;
using System.Collections.Generic;
using DatabaseLayer;

public sealed class Pair<Airport, Flight>
    : IEquatable<Pair<Airport, Flight>>
{
    private readonly Airport airport;
    private readonly Flight flight;

    public Pair(Airport airport, Flight flight)
    {
        this.airport = airport;
        this.flight = flight;
    }

    public Airport GetAirport
    {
        get { return airport; }
    }

    public Flight GetFlight
    {
        get { return flight; }
    }

    public bool Equals(Pair<Airport, Flight> other)
    {
        if (other == null)
        {
            return false;
        }
        return EqualityComparer<Airport>.Default.Equals(this.GetAirport, other.GetAirport) &&
               EqualityComparer<Flight>.Default.Equals(this.GetFlight, other.GetFlight);
    }

    public override bool Equals(object o)
    {
        return Equals(o as Pair<Airport, Flight>);
    }

    public override int GetHashCode()
    {
        return EqualityComparer<Airport>.Default.GetHashCode(airport) * 37 +
               EqualityComparer<Flight>.Default.GetHashCode(flight);
    }
}