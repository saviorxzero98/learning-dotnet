namespace SqlKataSample
{
    class Program
    {
        static void Main(string[] args)
        {
            //var sqlQueryBuilder = new SqlQueryBuilderSample();
            //sqlQueryBuilder.RunDemo();

            var sqlExecution = new SqlExecutionSample();
            sqlExecution.DemoSqlExecutionBySqlServer();
        }
    }
}
