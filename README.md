Demonstrates Api Versioning using headers in OData Web Api

## Examples

### Use with header: api-version=1.0
- V1 Metadata: http://localhost:1519/odata/$metadata
- V1 Movies EntitySet: http://localhost:1519/odata/Movies
- V1 GetRating Bound: http://localhost:1519/odata/Movies(3)/GetRating()
- V1 GetRating Unbound: http://localhost:1519/odata/GetRatingUnbound(movieId=3)

### Use with header: api-version=2.0
- V2 Metadata: http://localhost:1519/odata/$metadata
- V2 Movies EntitySet: http://localhost:1519/odata/Movies
- V2 GetRevenue Bound: http://localhost:1519/odata/Movies(3)/GetRevenue()
- V2 GetRevenue Unbound: http://localhost:1519/odata/GetRevenueUnbound(movieId=3)