using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);



var sql = builder.AddSqlServer("sqlserver")
    .WithDataVolume()                              
    .WithEnvironment("SA_PASSWORD", "password");



var mongo = builder.AddMongoDB("mongodb")
    .WithDataVolume()                             
    .WithEnvironment("MONGO_INITDB_ROOT_USERNAME", "admin")
    .WithEnvironment("MONGO_INITDB_ROOT_PASSWORD", "password");


var ordersService = builder.AddProject<Projects.AutoServiceCatalog_API>("orders-service")
    .WithReference(sql)
    .WaitFor(sql);    

var reviewsService = builder.AddProject<Projects.WebApi>("reviews-service")
    .WithReference(mongo)
    .WaitFor(mongo);    

var catalogService = builder.AddProject<Projects.AutoServiceCatalog_API>("catalog-service")
    .WithReference(sql)
    .WaitFor(sql);

var aggregator = builder.AddProject<Projects.AggregatorService>("aggregator-service")
    .WithReference(ordersService)
    .WithReference(reviewsService)
    .WithReference(catalogService)
    .WaitFor(ordersService)
    .WaitFor(reviewsService)
    .WaitFor(catalogService);

var gateway = builder.AddProject<Projects.ApiGateway>("api-gateway")
    .WithHttpEndpoint(5000, name: "gateway-http") 
    .WithReference(ordersService)
    .WithReference(reviewsService)
    .WithReference(catalogService)
    .WithReference(aggregator)
    .WaitFor(ordersService)
    .WaitFor(reviewsService)
    .WaitFor(catalogService)
    .WaitFor(aggregator);

builder.Build().Run();
