using System.Collections.Generic;

namespace VelocityDBExtensions.Geo.query
{


  public interface GeoHashQuery
  {

    /// <summary>
    /// check whether a geohash is within the hashes that make up this query.
    /// </summary>
    bool Contains(GeoHash hash);

    /// <summary>
    /// returns whether a point lies within a query.
    /// </summary>
    bool Contains(WGS84Point point);

    /// <summary>
    /// should return the hashes that re required to perform this search.
    /// </summary>
    IList<GeoHash> SearchHashes {get;}

    string WktBox {get;}

  }
}