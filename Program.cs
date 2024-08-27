namespace HigherOrderFunction4
{
    internal class Program
    {
        static void Main()
        {
            List<Product> products = new List<Product>
            {
               new Product { Name = "Apple", Price = 1.0, Quantity = 10 },
               new Product { Name = "Banana", Price = 0.5, Quantity = 20 },
               new Product { Name = "Cherry", Price = 2.0, Quantity = 15 },
               new Product { Name = "Date", Price = 3.0, Quantity = 5 },
            };

            Func<List<Product>, List<Product>> filterFunc = x => x.Where(x => x.Price < 2.0).ToList();

            Func<List<Product>, List<Product>> sortFunc = x => x.OrderBy(x => x.Price).ToList();

            Func<List<Product>, List<double>> transformFunc = x => x.Select(x => x.Price * x.Quantity).ToList();

            Func<List<double>, double> aggregateFunc = x => x.Sum();

            var pipeline = CreateQueryPipeline(filterFunc, sortFunc, transformFunc, aggregateFunc);

            double result = pipeline(products);

            Console.WriteLine($"The result of the query pipeline is: {result}");
        }

        static Func<List<Product>, double> CreateQueryPipeline(
                                                                 Func<List<Product>, List<Product>> filterFunc,
                                                                 Func<List<Product>, List<Product>> sortFunc,
                                                                 Func<List<Product>, List<double>> transformFunc,
                                                                 Func<List<double>, double> aggregateFunc
                                                               )
        {
            return input => aggregateFunc(transformFunc(sortFunc(filterFunc(input))));
        }

        class Product
        {
            public string Name { get; set; }
            public double Price { get; set; }
            public int Quantity { get; set; }
        }
    }
}
