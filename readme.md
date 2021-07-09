# FIrst Create Redis Cache in Azure / On-Premise

https://redislabs.com/ebook/appendix-a/a-3-installing-on-windows/a-3-2-installing-redis-on-window/

# Read its COnnectio String and Add it in the appsettings.json as value of 'Redis' in 'onnectionStrings'

# Also Must have database in Azure / On-Prmises and use its COnnection String in 'AppConnectionString' of 'ConnectionStrings' in appsettings.json

# USe Dockerfile to BUild Image
# Push image in Hub (Make sure that to use it as Micropservice in Microk8s (or any other Kubernetes, the Database and Redis must be in cloud))
# USe deploy.yml to deploy the app in Kubernetes