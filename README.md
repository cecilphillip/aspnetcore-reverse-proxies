## ASP.NET, Reverse Proxies, and Load Balancers
Different sample configurations for running ASP.NET Core behind various load balancers and reverse proxies. Relies on docker-compose files to spin up environments bound to port 80 and 443 on the host machine.

### Configurations
- HAProxy - dynamic load balancing config with docker
- Traefik - dynamic load balancing config with docker
- NGINX - Static config between two servers
