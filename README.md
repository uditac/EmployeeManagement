setting up docker :
Follow the steps in https://docs.docker.com/desktop/install/windows-install/
https://learn.microsoft.com/en-us/windows/wsl/install
open windows powershell  and run wsl install then run wsl --install -d Ubuntu then once ubuntu is installed run wsl --update
Run the command   docker run -d -p 80:80 docker/getting-started
Then set up postgres by using pgadmin
Then run the command docker pull postgres
Run the command     docker run -d -e POSTGRES_USER=user -e POSTGRES_PASSWORD=**** --name local-postgres -p 5432:5432  postgres
setup the pgadmin by adding the server name : local-postgres and hostname as localhost
* logging
   https://peterdaugaardrasmussen.com/2023/02/19/how-to-set-up-serilog-logging-in-asp-net/
