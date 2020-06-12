## ASP.NET, Reverse Proxies, and Load Balancers

Different sample configurations for running ASP.NET Core behind various load balancers and reverse proxies. Relies on docker-compose files to spin up environments bound to port 80 and 443 on the host machine.

Some of these configurations use TLS and expect a `demo.pfx` file to be available in the containers for the web APIs. You can use the following command to create one on your machiine.

```shell
dotnet dev-certs https -ep ${HOME}/.aspnet/https/demo.pfx -p 1111
```
### Configurations

#### HAProxy - dynamic load balancing config with docker
This setup configures HAProxy to use Docker's DNS to discover the available API endpoints. The configuration for HAProxy is in the `haproxy.cfg` file. The load balancer binds to port 80 on localhost. The HAProxy web UI is available on port 81 on localhost. The user name and password for the web UI is set in the `frontend stats` of the `haproxy.cfg` file.

Versions

- HAProxy v2.1.4

Configuration Files

- `docker-compose-haproxy.yml`
- `haproxy.cfg`
- `api/Dockerfile`

Commands

```shell
scale up
> docker-compose -f docker-compose-haproxy.yml up --build --scale simplecoreapi_service=4

shutdown
> docker-compose -f docker-compose-haproxy.yml down

logs
> docker-compose -f docker-compose-haproxy.yml logs
```


#### HAProxy - dynamic load balancing config with consul
This setup configures HAProxy to use Consul's DNS to discover the available API endpoints. The configuration for HAProxy is in the `haproxy4consul.cfg` file. The load balancer binds to port 80 on localhost. The HAProxy web UI is available on port 81 on localhost. The user name and password for the web UI is set in the `frontend stats` of the `haproxy4consul.cfg` file. Consul's web UI is availiable on port 8500 on localhost.

Versions

- HAProxy v2.1.4
- Consul v1.7.3

Configuration Files

- `docker-compose-haproxy-consul.yml`
- `haproxy4consul.cfg`
- `consul.server.json`
- `apiinsights/Dockerfile`

Commands

```shell
scale up
> docker-compose -f docker-compose-haproxy-consul.yml up --build --scale apiinsights=4

shutdown
> docker-compose -f docker-compose-haproxy-consul.yml down

logs
> docker-compose -f docker-compose-haproxy-consul.yml logs
```

#### Traefik - dynamic load balancing config with docker
This setup uses Traefik as a load balancer. Traefik's configuration is provided via labels and command options in the Docker compose file. This configuration uses Docker as a provider for backend services. Traefik binds and accepts traffic on both port 80 and 443 on localhost. It also has a web UI available on port 8080.

Versions

- Traefik v2.2.1

Configuration Files

- `docker-compose-traefik.yml`
- `api/Dockerfile`

Commands

```shell
scale up
> docker-compose -f docker-compose-traefik.yml up --build --scale simplecoreapi_service=4

shutdown
> docker-compose -f docker-compose-traefik.yml down

logs
> docker-compose -f docker-compose-traefik.yml logs
```


#### NGINX - Static config between two servers
This setup uses NGINX as a reverse proxy and load balancer. The NGINX discovers available backends staticly via the `nginx.conf` configuration file. Scaling the service with Docker compose will not make them visible to NGINX. You'll need to update the configuration file for that.

Versions

- NGINX v1.19.0

Configuration Files

- `docker-compose-nginx-static.yml`
- `nginx.conf`
- `api/Dockerfile`
- `prometheus.yml`

Commands

```shell
run
> docker-compose -f docker-compose-nginx-static.yml up --build

shutdown
> docker-compose -f docker-compose-nginx-static.yml down

logs
> docker-compose -f docker-compose-nginx-static.yml logs
```

#### Fabio - dynamic load balancing config with consul
This setup configures Fabio as a load balancing along with consul as the source for dynamically providing backend endpoints. The fabio configuration is provided via environment variables in the Docker compose file. Fabio listens for incoming traffic on port 80 on localhost. Also, Fabio's web ui is available on port 9998 on localhost. Consul's web UI is availiable on port 8500 on localhost.

Versions

- Fabio 1.5.13

Configuration Files

- `docker-compose-fabio-consul.yml`
- `consul.server.json`
- `apiinsights/Dockerfile`

Commands

```shell
run
> docker-compose -f docker-compose-fabio-consul.yml up --build

shutdown
> docker-compose -f docker-compose-fabio-consul.yml down

logs
> docker-compose -f docker-compose-fabio-consul.yml logs
```



### TODO
- Consul Connect
- Envoy
- COREDNS
