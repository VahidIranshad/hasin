The Cache_aside pattern is utilized in this project. 
To run the program, execute the docker-compose up -d command. 
Since reading and writing data is straightforward, a specialized pattern like Clean Architecture was omitted for these operations.
For caching, a Core library was employed containing the ICache interface and the LayerCache class. Each cache type has a library with the implementation of the ICache interface. 
Within the API, a project was established to retrieve book information via port 5000. 
This project includes a Factory for accessing information from the Cache as a Singleton, and a BookService added as a Scope. 
Modifying the database type is easily achievable by implementing a new cache and adjusting the cachefactory class. 
Additionally, two consumers were developed in this project for cache clearing and data transfer. Another project was created to test these consumers, utilizing two APIs for absorbing and generating information in the cache, operating on port 5002. 
The Unittest project is dedicated to testing caches not integrated into Docker, and running the Redis configuration test necessitates the use of the RedisInitializer class. Lastly, the FunctionTest project is designated for API testing, excluding it from Docker integration.
