docker run -p 80:80 -e "ConnectionStrings:SQLDbConnection=Server=172.19.0.2,1433;Initial Catalog=CommandDb;User Id=sa;Password=pa55w0rd!" --network="backend" dotnetminapi:local