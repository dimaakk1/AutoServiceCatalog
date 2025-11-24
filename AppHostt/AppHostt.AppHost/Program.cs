using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);


var sql = builder.AddSqlServer("sqlserver")
    .WithDataVolume();



var mongo = builder.AddMongoDB("mongo")
    .WithImage("mongo:7")
    .WithDataVolume("mongo");




var catalogService = builder.AddProject<Projects.AutoServiceCatalog_API>("catalog-service")
    .WithReference(sql)
    .WaitFor(sql);

var ordersService = builder.AddProject<Projects.AutoserviceOrders_API>("orders-service")
    .WithReference(sql)
    .WaitFor(sql);

var reviewsService = builder.AddProject<Projects.WebApi>("reviews-service")
    .WithReference(mongo)
    .WaitFor(mongo);

var apiGateway = builder.AddProject<Projects.ApiGateway>("gateway")
    .WithReference(catalogService)
    .WithReference(ordersService)
    .WithReference(reviewsService)
    .WithExternalHttpEndpoints();

var aggregationApi = builder.AddProject<Projects.AggregatorService>("aggregation-service")
    .WithReference(ordersService)
    .WithReference(reviewsService);


builder.Build().Run();